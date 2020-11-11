namespace Microservice.Prosecution.CrossCutting.Register
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using System;
    using System.IO;
    /// <summary>
    /// Configure Swagge
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Add configuration to swagger in service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRegistrationSc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Microservice.Prosecution API V1",
                    Description = "Prosecution API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    //Contact = new OpenApiContact
                    //{
                    //    Name = @"cbermedina",
                    //    Email = "cbermedina@gmail.com",
                    //    Url = new Uri("https://github.com/cbermedina")
                    //}
                });
                var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Microservice.Prosecution.xml");
                c.IncludeXmlComments(xmlPath, true);
            });
            return services;
        }
        /// <summary>
        /// Add configuration to swagger in app
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddRegistrationSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.DocumentTitle = "Microservice.Prosecution API V1";
                c.DefaultModelsExpandDepth(0);
            });
            return app;
        }
    }
}
