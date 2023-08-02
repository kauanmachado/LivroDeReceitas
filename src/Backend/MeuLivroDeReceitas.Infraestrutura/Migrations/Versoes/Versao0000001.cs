﻿using FluentMigrator;

namespace MeuLivroDeReceitas.Infraestrutura.Migrations.Versoes;

[Migration((long)NumeroVersoes.CriarTabelaUsuario, "Cria tabela usuário")]
public class Versao0000001 : Migration
{
    public override void Down()
    {
        
    }

    public override void Up()
    {
        var tabela = VersaoBase.InserirColunasPadrao(Create.Table("Usuarios"));

        tabela
            .WithColumn("Nome").AsString(100).NotNullable()
            .WithColumn("Email").AsString(100).NotNullable().Unique()
            .WithColumn("Senha").AsString(2000).NotNullable()
            .WithColumn("Telefone").AsString(14).NotNullable()
            ;
    }
}
