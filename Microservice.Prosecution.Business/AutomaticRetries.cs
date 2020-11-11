

namespace Microservice.Prosecution.Business
{
    using Hangfire;
    using Hangfire.Common;
    using Hangfire.Logging;
    using Hangfire.States;
    using Hangfire.Storage;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Clase para realizar reintentos sobre las peticiones realizadas en el Hangfire, esto es en caso de fallar la solicitud.
    /// </summary>
    public class AutomaticRetries : JobFilterAttribute, IElectStateFilter, IApplyStateFilter
    {
        #region Propiedades
        /// <summary>
        /// Variable con la configuracion obtenida del archivo app.settings
        /// </summary>
       IConfigurationSection configuracion = new ConfigurationBuilder().AddJsonFile($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\appsettings.json", optional: true, reloadOnChange: true).Build().GetSection("Configuracion_General");
        /// <summary>
        /// tiempo para encolar nuevamente.
        /// </summary>
        public int timeToRetryAgain;
        /// <summary>
        /// propiedad que configura el numero de reintentos que se tienen por defecto.
        /// </summary>
        private const int _retryNumberDefault = 10;
        /// <summary>
        /// Manejador de logs
        /// </summary>
        private static readonly ILog _logReintento = LogProvider.For<AutomaticRetries>();
        /// <summary>
        /// Object lock(_lockObject)
        /// </summary>
        private readonly object _objetoBloqueo = new object();
        /// <summary>
        /// Intentos realizados para la peticion
        /// </summary>
        private int _intentos;
        /// <summary>
        /// intentos superados.
        /// </summary>
        public AttemptsExceededAction _intentosSuperados { get; set; }
        /// <summary>
        /// Indica si se encuentra activo los eventos de log
        /// </summary>
        private bool _eventosLog;
        /// <summary>
        /// Propiedad para asignar el maximo numero de intentos.
        /// </summary>
        public int Retries
        {
            get { lock (_objetoBloqueo) { return _intentos; } }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Attempts value must be equal or greater than zero.");
                }
                lock (_objetoBloqueo)
                {
                    _intentos = value;
                }
            }
        }
        /// <summary>
        /// estado candidato para un trabajo en segundo plano que se elegirá cuando se exceda el número de intentos de reintento.
        /// </summary>
        public AttemptsExceededAction IntentosSuperados
        {
            get { lock (_objetoBloqueo) { return _intentosSuperados; } }
            set { lock (_objetoBloqueo) { _intentosSuperados = value; } }
        }
        /// <summary>
        /// Indica si se producen mensajes de registro en los intentos de reintento.
        /// </summary>
        public bool LogEventos
        {
            get { lock (_objetoBloqueo) { return _eventosLog; } }
            set { lock (_objetoBloqueo) { _eventosLog = value; } }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public AutomaticRetries()
        {
            int processRetriesNumber = Convert.ToInt32(configuracion.GetValue<string>("ProcessRetriesNumber"));
            Retries = processRetriesNumber > 0 ? processRetriesNumber : _retryNumberDefault;
            LogEventos = true;
            IntentosSuperados = AttemptsExceededAction.Fail;
            Order = 20;
            timeToRetryAgain =Convert.ToInt32(configuracion.GetValue<string>("TimeToQueue"));
        }
        #endregion
        #region Metodos
        /// <summary>
        /// Metodo para obtener el estado final de la peticion.
        /// </summary>
        /// <param name="context">contexto de la peticion</param>
        public void OnStateElection(ElectStateContext context)
        {
            FailedState estadoFallido = context.CandidateState as FailedState;
            // Este filtro acepta solo el estado de trabajo fallido.
            if (estadoFallido == null) return;

            int retryCount = context.GetJobParameter<int>("RetryCount") + 1;

            if (retryCount <= Retries)
                ScheduleAgainLater(context, retryCount, estadoFallido);
            else if (retryCount > Retries && IntentosSuperados == AttemptsExceededAction.Delete)
                TransitionToDeleted(context, estadoFallido);
            else
            {
                if (LogEventos)
                {
                    _logReintento.ErrorException(
                        $"Failed to process the job '{context.BackgroundJob.Id}': an exception occurred.",
                        estadoFallido.Exception);
                }
            }
        }
        /// <summary>
        /// Metodo para saber el estado a aplicar a una peticion
        /// </summary>
        /// <param name="context">contexto de la transaccion</param>
        /// <param name="transaction">transaccion</param>
        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            if (context.NewState is ScheduledState &&
                context.NewState.Reason != null &&
                context.NewState.Reason.StartsWith("Retry attempt"))
            {
                transaction.AddToSet("retries", context.BackgroundJob.Id);
            }
        }
        /// <summary>
        /// Metodo para asignar estado de no aplicado.
        /// </summary>
        /// <param name="context">contexto de la transaccion</param>
        /// <param name="transaction">transaccion</param>
        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            if (context.OldStateName == ScheduledState.StateName)
                transaction.RemoveFromSet("retries", context.BackgroundJob.Id);
        }
        /// <summary>
        /// Schedules the job to run again later. See <see cref="SecondsToDelay"/>.
        /// </summary>
        /// <param name="context">The state context.</param>
        /// <param name="retryAttempt">The count of retry attempts made so far.</param>
        /// <param name="failedState">Object which contains details about the current failed state.</param>
        private void ScheduleAgainLater(ElectStateContext context, int retryAttempt, FailedState failedState)
        {
            context.SetJobParameter("RetryCount", retryAttempt);

            TimeSpan delay = TimeSpan.FromSeconds(timeToRetryAgain);//TimeSpan.FromSeconds(SecondsToDelay(retryAttempt));

            const int maxMessageLength = 50;
            var exceptionMessage = failedState.Exception.Message.Length > maxMessageLength
                ? failedState.Exception.Message.Substring(0, maxMessageLength - 1) + "…"
                : failedState.Exception.Message;

            // If attempt number is less than max attempts, we should
            // schedule the job to run again later.
            context.CandidateState = new ScheduledState(delay)
            {
                Reason = $"Retry attempt {retryAttempt} of {Retries}: {exceptionMessage}"
            };

            if (LogEventos)
            {
                _logReintento.WarnException(
                    $"Failed to process the job '{context.BackgroundJob.Id}': an exception occurred. Retry attempt {retryAttempt} of {Retries} will be performed in {delay}.",
                    failedState.Exception);
            }
        }
        /// <summary>
        /// Transition the candidate state to the deleted state.
        /// </summary>
        /// <param name="context">The state context.</param>
        /// <param name="failedState">Object which contains details about the current failed state.</param>
        private void TransitionToDeleted(ElectStateContext context, FailedState failedState)
        {
            context.CandidateState = new DeletedState
            {
                Reason = Retries > 0
                    ? "Exceeded the maximum number of retry attempts."
                    : "Retries were disabled for this job."
            };

            if (LogEventos)
            {
                _logReintento.WarnException(
                    $"Failed to process the job '{context.BackgroundJob.Id}': an exception occured. Job was automatically deleted because the retry attempt count exceeded {Retries}.",
                    failedState.Exception);
            }
        }
        /// <summary>
        /// Metodo para obtener el tiempo para realizar un reintento.
        /// </summary>
        /// <param name="retryCount">numero de reintentos</param>
        /// <returns></returns>
        private static int SecondsToDelay(long retryCount)
        {
            var random = new Random();
            double pow = 4;
            if (retryCount > 5)
            {
                switch (retryCount)
                {
                    case 6:
                        retryCount = 8;
                        break;
                    case 7:
                        retryCount = 9;
                        break;
                    case 8:
                        retryCount = 11;
                        break;
                    case 9:
                        retryCount = 12; // 
                        break;
                    case 10:
                        retryCount = 19; // 36 hrs
                        break;
                }
                retryCount = retryCount + retryCount;
            }
            return (int)Math.Round(
            Math.Pow(retryCount - 1, pow) + 15 + (random.Next(30) * (retryCount)));
        }
        #endregion
    }
}
