namespace Microservice.Prosecution.Application.Services
{
    using Microservice.Prosecution.Application.Contracts.IServices;
    using Microsoft.Extensions.Configuration;
    public class AppConfigService : IAppConfigService
    {
        private readonly IConfiguration _configuration;

        public AppConfigService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Cache
        /// <summary>
        /// Obtiene el máximo numero de reintentos
        /// </summary>
        public int GetMaxTrys => int.Parse(_configuration.GetSection("Polly:MaxTrys").Value);
        /// <summary>
        /// Obtiene la cantidad de segundos a esperar para reintentar una transacción
        /// </summary>
        public int GetSecondsToWait => int.Parse(_configuration.GetSection("Polly:TimeDelay").Value);
        /// <summary>
        /// Obtiene los minutos en los que expira la cache
        /// </summary>
        public int GetCacheExpireInMinutes => int.Parse(_configuration.GetSection("Cache:CacheExpireInMinutes").Value);
        #endregion

        #region General Configuration
        /// <summary>
        /// Nombre Servidor
        /// </summary>
        public string GetServerName => _configuration.GetSection("GeneralConfiguration:ServerName").Value;
        /// <summary>
        /// Numero Trabajadores
        /// </summary>
        public int GetWorkersNumber => int.Parse(_configuration.GetSection("GeneralConfiguration:WorkersNumber").Value);
        /// <summary>
        /// intentos Transaccion
        /// </summary>
        public int GetProcessRetriesNumber => int.Parse(_configuration.GetSection("GeneralConfiguration:ProcessRetriesNumber").Value);
        /// <summary>
        /// tiempo Encolamiento
        /// </summary>
        public int GetTimeToQueue => int.Parse(_configuration.GetSection("GeneralConfiguration:TimeToQueue").Value);
        /// <summary>
        /// ruta Log
        /// </summary>
        public string GetLogRoute => _configuration.GetSection("GeneralConfiguration:LogRoute").Value;

        #endregion
    }
}
