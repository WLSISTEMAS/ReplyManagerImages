﻿using Reply.Manager.Images.Domain.Adapters;
using Reply.Manager.Images.TmdbAdapter;
using Reply.Manager.Images.TmdbAdapter.Clients;
using Otc.Networking.Http.Client.Abstractions;
using Refit;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TmdbAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddTmdbAdapter(this IServiceCollection services, TmdbAdapterConfiguration tmdbAdapterConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (tmdbAdapterConfiguration == null)
                throw new ArgumentNullException(nameof(tmdbAdapterConfiguration));

            // Registra a instancia do objeto de configuracoes desta camanda.
            services.AddSingleton(tmdbAdapterConfiguration);

            // Configura os parametros para chamada na TMDb API e registra a interface ITmdbApi.
            services.AddScoped(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateHttpClient();
                httpClient.BaseAddress = new Uri(tmdbAdapterConfiguration.TmdbApiUrlBase);

                return RestService.For<ITmdbApi>(httpClient);
            });

            // Registra a implementacao do ITmdbAdapter para ser utilizado na camada de aplicacao.
            services.AddScoped<ITmdbAdapter, TmdbAdapter>();

            return services;
        }
    }
}
