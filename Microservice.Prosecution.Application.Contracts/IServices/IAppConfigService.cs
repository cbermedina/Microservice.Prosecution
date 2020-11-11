namespace Microservice.Prosecution.Application.Contracts.IServices
{
    public interface IAppConfigService
    {
        #region Cache
        /// <summary>
        /// Obtiene el máximo numero de reintentos
        /// </summary>
        int GetMaxTrys{ get; }
        /// <summary>
        /// Obtiene la cantidad de segundos a esperar para reintentar una transacción
        /// </summary>
        int GetSecondsToWait{ get; }
        /// <summary>
        /// Obtiene los minutos en los que expira la cache
        /// </summary>
        int GetCacheExpireInMinutes{ get; }
        #endregion

        #region General configuration
        /// <summary>
        /// Nombre Servidor
        /// </summary>
        string GetServerName { get; }
        /// <summary>
        /// Numero Trabajadores
        /// </summary>
        int GetWorkersNumber { get; }
        /// <summary>
        /// intentos proceso
        /// </summary>
        int GetProcessRetriesNumber { get; }
        /// <summary>
        /// tiempo Encolamiento
        /// </summary>
        int GetTimeToQueue { get; }
        /// <summary>
        /// ruta Log
        /// </summary>
        string GetLogRoute{ get; }
        #endregion
    }
}
