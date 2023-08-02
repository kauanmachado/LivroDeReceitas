using MeuLivroDeReceitas.Infraestrutura.Migrations;
using MeuLivroDeReceitas.Infraestrutura;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Api.Filtros;
using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.Automapper;
using MeuLivroDeReceitas.Application;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(option => option.LowercaseUrls = true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositorio(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddMvc(options => options.Filters.Add(typeof(FiltroDasExceptions)));
builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguracao());
}).CreateMapper());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

AtualizarBaseDeDados();

app.Run();

void AtualizarBaseDeDados()
{
    var conexao = builder.Configuration.GetConexao();
    var nomeDatabase = builder.Configuration.GetNomeDatabase();

    Database.CriarDatabase(conexao, nomeDatabase);

    app.MigrateBancoDeDados();
}
