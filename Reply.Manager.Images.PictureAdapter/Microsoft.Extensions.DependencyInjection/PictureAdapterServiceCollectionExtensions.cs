using Microsoft.Extensions.DependencyInjection;
using Reply.Manager.Images.Domain.Adapters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reply.Manager.Images.PictureAdapter.Microsoft.Extensions.DependencyInjection
{
    public static class PictureAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddPictureAdapter(this IServiceCollection services, PictureAdapterConfiguration pictureAdapterConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (pictureAdapterConfiguration == null)
                throw new ArgumentNullException(nameof(pictureAdapterConfiguration));

            // Registra a instancia do objeto de configuracoes desta camanda.
            services.AddSingleton(pictureAdapterConfiguration);

            // Registra a implementacao do ITmdbAdapter para ser utilizado na camada de aplicacao.
            services.AddScoped<IPictureAdapter, PictureAdapter>();

            return services;
        }
    }
}
