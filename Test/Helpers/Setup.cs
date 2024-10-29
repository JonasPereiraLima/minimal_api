using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Minimal_Api.Dominio.Interfaces;
using Test.Mocks;

namespace Test.Helpers;

public class Setup
{
    public const string PORT = "5101";
    public static TestContext testContext = default!;
    public static WebApplicationFactory<Startup> http = default!;
    public static HttpClient client = default!;

    public static void ClassInit(TestContext testContext)
    {
        Setup.testContext = testContext;
        Setup.http = new WebApplicationFactory<Startup>();
        Setup.http = Setup.http.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("https_port", Setup.PORT).UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IAdministradorServico, AdministradoresServicoMock>();

                /*
                //== Caso queira deixar o teste com conexão diferente ==
                var conexao = "Server=localhost;Database=desafio21dias_dotnet7_test;Uid=root;Pwd=root";
                services.AddContext<DbContexto>(options => 
                {
                    options.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
                });


                */
            });

        });

        Setup.client = Setup.http.CreateClient();
    }

    internal static void ClassCleanup()
    {
        Setup.http.Dispose();
    }
}