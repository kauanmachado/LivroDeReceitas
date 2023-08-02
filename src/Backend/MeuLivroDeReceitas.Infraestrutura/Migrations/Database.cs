using Dapper;
using MySqlConnector;

namespace MeuLivroDeReceitas.Infraestrutura.Migrations;

public static class Database
{
    public static void CriarDatabase(string conexaoComBancoDeDados, string nomeDatabase)
    {
        using var conexao = new MySqlConnection(conexaoComBancoDeDados);
        var parametros = new DynamicParameters();
        parametros.Add("nome", nomeDatabase);

        var registros = conexao.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @nome", parametros);

        if (!registros.Any()) 
        {
            conexao.Execute($"CREATE DATABASE {nomeDatabase}");
        }
    }
}
