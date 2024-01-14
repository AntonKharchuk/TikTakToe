
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TikTakToe.ConsoleDemo
{
    public static class DI
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add the implementation types and specify the interfaces
           return  services.AddSingleton(configuration)
                    .AddScoped<IPrintGame, ConsoleGamePrinterWithArrowSelection>()
                    .AddScoped<IApiClient>(provider =>
                    {
                        var baseAddress = configuration["ApiSettings:LocalHostAddress"];
                        return new ApiClient(baseAddress);
                    })
                    .AddScoped<Game, Game>();
        }
    }
}
