using System.Net;
using IPO.Address.API.HealthChecks;
using IPO.Address.Gateways;
using IPO.Address.Interfaces;
using IPO.Address.Models.Configuration;
using IPO.Address.Services;
using IPO.Common.API;
using IPO.CTC.HealthChecks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

namespace IPO.Address.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Helper = new IPOStartupHelper("IPO.Address.API", "version");
        }

        public IConfiguration Configuration { get; }
        public IPOStartupHelper Helper { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Helper.AddIPOServicesConfiguration(services);

            services.AddHealthChecks().AddCheck<AddressServiceHealthCheck>(
                "melissa-address-service-check",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { HealthTags.Ready }
            );

            services.AddIPOErrorAwareScoped<IAddressService, AddressService>("E003");

            services.AddSingleton<GetMelissaQueryString>((c, s) =>
            {
                var queriBuilder = new QueryBuilder
                {
                    { "id", Configuration.GetSection("MelissaGateway")["MelissaId"]!.ToString() },
                    { "country", c },
                    { "ff", s },
                    { "nativecharset", "false" },
                    { "maxrecords", Configuration.GetSection("MelissaGateway")["MelissaMaxRecords"]! },
                    { "format", "json" },
                    { "suitecompression", "false" }
                };

                return queriBuilder.ToString();
            });

            services.AddSwaggerGen(config =>
            {

                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Address Microservice", Version = "v1" });
                config.EnableAnnotations();

            });

            AddManagementServices(services);

            services.Configure<Settings>(Configuration);

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.UseRewriter(new RewriteOptions().Add(RewriteRules.RewriteAlwaysOn));

			Helper.UseIPOConfigurations(app, env);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
        protected virtual void AddManagementServices(IServiceCollection services)
        {
#if DEBUG
            services.AddHttpClient<IAddressGateway, MelissaAddressGateway>((config) =>
            {
                config.BaseAddress = new Uri(Configuration.GetSection("MelissaGateway")["MelissaConnectionString"]!.ToString());
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    UseDefaultCredentials = true,
                    DefaultProxyCredentials = CredentialCache.DefaultNetworkCredentials
                };
            });
#endif
            services.AddIPOErrorAwareHttpClient<IAddressGateway, MelissaAddressGateway>(
                                            x =>
                                            {
                                                x.BaseAddress = new Uri(Configuration.GetSection("MelissaGateway")["MelissaConnectionString"]!.ToString());
                                                x.DefaultRequestHeaders.Add("Accept-Version", Configuration.GetSection("MelissaGateway")["WrapperVersion"]!.ToString());
                                                x.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Configuration.GetSection("MelissaGateway")["SubscriptionKey"]!.ToString());
                                            }, "E002");

        }
    }
}