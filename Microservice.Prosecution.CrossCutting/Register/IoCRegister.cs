namespace Microservice.Prosecution.CrossCutting.Register
{
    using Hangfire;
    using Hangfire.Dashboard;
    using Hangfire.Logging.LogProviders;
    using Hangfire.Mongo;
    using Microservice.Prosecution.Application.Contracts.IServices;
    using Microservice.Prosecution.Application.Services;
    using Microservice.Prosecution.Business;
    using Microservice.Prosecution.Common.Constants;
    using Microservice.Prosecution.CrossCutting.Filters;
    using Microservice.Prosecution.DataAccess.Configuration;
    using Microservice.Prosecution.DataAccess.Contracts.IRepositories;
    using Microservice.Prosecution.DataAccess.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class IoCRegister
    {
        /// <summary>
        /// Add generic register
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            services.AddRegisterServices();
            services.AddRegisterRepositories();
            services.AddRegisterOthers();
            return services;
        }
        /// <summary>
        /// Add Register Services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddRegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IProcessInformationService, ProcessInformationService>();
            services.AddSingleton<IAppConfigService, AppConfigService>();
            return services;
        }
        /// <summary>
        /// Add Register Repositories
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddRegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMongoContextRepository, MongoContextRepository>();
            return services;
        }
        /// <summary>
        /// Add Register Others
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddRegisterOthers(this IServiceCollection services)
        {
            services.AddTransient<QueuingProcess>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IConfiguration _configuration = serviceProvider.GetService<IConfiguration>();
            services.AddHangfire(config =>
            {
                var migrationOptions = new MongoStorageOptions
                {
                    MigrationOptions = new MongoMigrationOptions
                    {
                        Strategy = MongoMigrationStrategy.Migrate,
                        BackupStrategy = MongoBackupStrategy.Collections
                    }
                };
                config.UseColouredConsoleLogProvider();
                config.UseLogProvider(new ColouredConsoleLogProvider());
                config.UseMongoStorage(_configuration.GetSection("MongoConnection:ConnectionString").Value, _configuration.GetSection("MongoConnection:Database").Value, migrationOptions);
                config.UseDashboardMetric(DashboardMetrics.FailedCount);
                config.UseDashboardMetric(DashboardMetrics.EnqueuedAndQueueCount);
                config.UseDashboardMetric(DashboardMetrics.AwaitingCount);
                config.UseDashboardMetric(DashboardMetrics.DeletedCount);
                config.UseDashboardMetric(DashboardMetrics.ProcessingCount);
                config.UseDashboardMetric(DashboardMetrics.ScheduledCount);
                config.UseDashboardMetric(DashboardMetrics.SucceededCount);
                config.UseDashboardMetric(DashboardMetrics.ServerCount);
            });

            services.Configure<Settings>(options =>
            {
                options.ConnectionString = _configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = _configuration.GetSection("MongoConnection:Database").Value;
            });
            services.AddMvc();
            return services;
        }
        /// <summary>
        /// Add configuration to swagger in app
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddRegistration(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.AddHangFireRegistration(configuration);
            return app;
        }
        public static IApplicationBuilder AddHangFireRegistration(this IApplicationBuilder app, IConfiguration configuration)
        {
            var options = new BackgroundJobServerOptions
            {
                Queues = new[]
                {
                     Queues.QueueTransactions
                },
                WorkerCount = Convert.ToInt32(configuration.GetSection("GeneralConfiguration:WorkersNumber").Value),
                ServerName = $"Servidor Envío de Transacciones [{Environment.MachineName}]"

            };
            app.UseHangfireServer(options);
            string hangfireName = configuration.GetSection("ProcessMongoDB:HangfireName").Value;
            app.UseHangfireDashboard($"/{hangfireName}", new DashboardOptions
            {
                Authorization = new[] { new AuthorizationFilters() }
            });
            #region Hangfire custom retry
            object AutomaticRetriesAttribute = null;
            foreach (var filtro in GlobalJobFilters.Filters)
                if (filtro.Instance is AutomaticRetryAttribute)
                    AutomaticRetriesAttribute = filtro.Instance;

            if (AutomaticRetriesAttribute != null)
            {
                GlobalJobFilters.Filters.Remove(AutomaticRetriesAttribute);
                GlobalJobFilters.Filters.Add(new AutomaticRetries());
            }
            #endregion
            return app;
        }
    }
}
