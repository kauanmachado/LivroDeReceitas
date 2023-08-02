using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Domain.Repositorios;
using MeuLivroDeReceitas.Infraestrutura.AcessoRepositorio.Repositorio;
using MeuLivroDeReceitas.Infraestrutura.AcessoRepositorio;
using Microsoft.EntityFrameworkCore;
using FluentMigrator.Runner.Initialization;

namespace MeuLivroDeReceitas.Infraestrutura;

public static class Bootstrapper
{
    public static void AddRepositorio(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMigrator(services, configurationManager);
        AddContexto(services, configurationManager);
        AddUnidadeDeTrabalho(services);
        AddRepositorios(services);
    }
   

    private static void AddContexto(IServiceCollection services, IConfiguration configurationManager)
    {

        var versaoServidor = new MySqlServerVersion(new Version(8,0,26));
        var connectionString = configurationManager.GetConexaoCompleta();
        services.AddDbContext<MeuLivroDeReceitasContext>(dbContextOpcoes =>
        {
            dbContextOpcoes.UseMySql(connectionString, versaoServidor);
        });
    }
    private static void AddUnidadeDeTrabalho(IServiceCollection services)
    {
        services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
    }

    public static void AddRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUsuarioWriteOnlyRepositorio, UsuarioRepositorio>()
                .AddScoped<IUsuarioReadOnlyRepositorio, UsuarioRepositorio>();

    }
    

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager)
    {

      

        services.AddFluentMigratorCore().ConfigureRunner(
            c => c.AddMySql5()
            .WithGlobalConnectionString(configurationManager.GetConexaoCompleta()).ScanIn(Assembly.Load("MeuLivroDeReceitas.Infraestrutura")).For.All());
    }

   
}
