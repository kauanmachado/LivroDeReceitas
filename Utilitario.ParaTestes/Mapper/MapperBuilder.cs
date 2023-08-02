using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.Automapper;

namespace Utilitario.ParaTestes.Mapper;

public class MapperBuilder
{
    public static IMapper Instancia()
    {
        var configuracao = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperConfiguracao>();
        });

        return configuracao.CreateMapper();
    }
}
