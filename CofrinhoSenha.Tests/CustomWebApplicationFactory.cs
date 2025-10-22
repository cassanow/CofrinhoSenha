using System.Data.Common;
using CofrinhoSenha.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CofrinhoSenha.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType ==
                                                                    typeof(IDbContextOptionsConfiguration<
                                                                        AppDbContext>));

            services.Remove(dbContextDescriptor);
            
            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType == 
                     typeof(DbConnection));
            
            services.Remove(dbConnectionDescriptor);
            
            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();  
                
                return connection;
            });

            services.AddDbContextFactory<AppDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });
        });
        
        builder.UseEnvironment("Development");
    }
}