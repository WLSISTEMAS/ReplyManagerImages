using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reply.Manager.Images.TmdbAdapter;
using Otc.AspNetCore.ApiBoot;
using Otc.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using Reply.Manager.Images.Application;
using Reply.Manager.Images.PictureAdapter;
using Reply.Manager.Images.PictureAdapter.Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Reply.Manager.Images.WebApi
{
    /// <summary>
    /// Este eh o Startup da API. 
    /// <para>
    /// A base <see cref="ApiBootStartup"/> implementa uma serie de requisitos que consideramos
    /// necessarios para qualquer API, como Log, Swagger, Authorizacao, Versionamento e mais.
    /// Veja https://github.com/OleConsignado/otc-aspnetcore-apiboot para maiores detalhes (talvez a documentacao ainda esteja em construcao).
    /// </para>
    /// </summary>
    public class Startup : ApiBootStartup
    {
        protected override ApiMetadata ApiMetadata => new ApiMetadata()
        {
            Name = "Reply.Manager.Images",
            Description = "Reply WebApi",
            DefaultApiVersion = "1.0"
        };

        static Startup()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<TmdbMapperProfile>();
                config.AddProfile<WebApiMapperProfile>();
            });
        }


        public Startup(IConfiguration configuration)
            : base(configuration)
        {

        }

        /// <summary>
        /// Registra os servicos especificos da API.
        /// </summary>
        /// <param name="services"></param>
        [ExcludeFromCodeCoverage]
        protected override void ConfigureApiServices(IServiceCollection services)
        {
            services.AddTmdbAdapter(Configuration.SafeGet<TmdbAdapterConfiguration>());
            services.AddPictureAdapter(Configuration.SafeGet<PictureAdapterConfiguration>());
            services.AddApplication(Configuration.SafeGet<ApplicationConfiguration>());

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_0);

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });
        }
    }
}
