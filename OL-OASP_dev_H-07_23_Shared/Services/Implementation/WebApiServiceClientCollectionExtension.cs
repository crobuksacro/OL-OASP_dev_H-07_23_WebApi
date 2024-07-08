using Microsoft.Extensions.DependencyInjection;
using OL_OASP_dev_H_07_23_Shared.Services.Interfaces;

namespace OL_OASP_dev_H_07_23_Shared.Services.Implementation
{
    public static class WebApiServiceClientCollectionExtension
    {
        /// <summary>
        /// Primjer, ne poziva se nigdje!!
        /// </summary>
        /// <param name="services"></param>
        /// <param name="webApiServiceBaseUrl"></param>
        /// <param name="unsuccessfulResponseAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebApiMovieServiceClient(this IServiceCollection services, string webApiServiceBaseUrl, Action<HttpResponseMessage> unsuccessfulResponseAction = null)
        {
            //Potrebno je instalirat pckg Microsoft.Extensions.Http
            services.AddHttpClient(nameof(WebApiMovieServiceClient)).AddTypedClient<IWebApiMovieServiceClient>(c => new WebApiMovieServiceClient(c, unsuccessfulResponseAction))
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(webApiServiceBaseUrl);
                });

            return services;
        }
    }
}
