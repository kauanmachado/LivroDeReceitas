using FluentMigrator.Builders.Create.Table;

namespace MeuLivroDeReceitas.Infraestrutura.Migrations
{
    public static class VersaoBase
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax InserirColunasPadrao(ICreateTableWithColumnOrSchemaOrDescriptionSyntax tabela)
        {
            return tabela
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("DataCriacao").AsDateTime().NotNullable();
        }
    }
}
