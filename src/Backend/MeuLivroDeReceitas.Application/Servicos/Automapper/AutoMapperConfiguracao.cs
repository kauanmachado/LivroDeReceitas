using AutoMapper;
using MeuLivroDeReceitas.Comunicacao.Request;

namespace MeuLivroDeReceitas.Application.Servicos.Automapper;

public class AutoMapperConfiguracao : Profile
{
    public AutoMapperConfiguracao()
    {
        CreateMap<RequestRegistrarUsuarioJson, Domain.Entities.Usuario>()
            .ForMember(destino => destino.Senha, config => config.Ignore());

    }
}
