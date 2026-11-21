using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Testudines.Data.Itis.Context;
using Testudines.Data.Itis.Identities;

namespace Testudines.Data.Identity.TestConsole;

internal static class Program
{
    private static async Task<int> Main()
    {
        try
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("ItisConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Console.Error.WriteLine("No se encontró ConnectionStrings:ItisConnection.");
                return 1;
            }

            var options = new DbContextOptionsBuilder<ItisDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;

            await using var db = new ItisDbContext(options);

            await db.Database.OpenConnectionAsync();
            var activeConnection = db.Database.GetDbConnection();
            Console.WriteLine($"Servidor conectado: {activeConnection.DataSource}");
            Console.WriteLine($"Base de datos conectada: {activeConnection.Database}");

              
            var listTaxonomicNames = await db.TaxonomicUnits
                .AsNoTracking()
                .Where(u => !string.IsNullOrEmpty(u.CompleteName))
                .Select(u => u.CompleteName)
                .OrderBy(name => name)
                .Take(20) // Límite preventivo por el gran tamaño de ITIS
                .ToListAsync();

            if (listTaxonomicNames != null)
            {
                foreach (var name in listTaxonomicNames)
                {
                    Console.WriteLine(name);
                }
            }

            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("La prueba de integración falló:");
            Console.WriteLine(exception.Message);
            Console.Error.WriteLine(exception.Message);
            return 1;
        }
        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey(); // Detiene la ejecución hasta que pulses una tecla
    }

    
}
