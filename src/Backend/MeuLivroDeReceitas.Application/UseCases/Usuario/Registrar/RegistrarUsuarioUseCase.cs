using AutoMapper;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Domain.Repositorios;
using MeuLivroDeReceitas.Exceptions.ExceptionBase;
using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Exceptions;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
{
    private readonly IUsuarioWriteOnlyRepositorio _repositorio;
    private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public RegistrarUsuarioUseCase(IUsuarioWriteOnlyRepositorio repositorio, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController, IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio)
    {
        _repositorio = repositorio;
        _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;

    }
    public async Task<RespostaUsuarioRegistradoJson> Executar(RequestRegistrarUsuarioJson request)
    {
        await Validar(request);

        var entidade = _mapper.Map<Domain.Entities.Usuario>(request);
        entidade.Senha = _encriptadorDeSenha.Criptografar(request.Senha);
        await _repositorio.Adicionar(entidade);
        await _unidadeDeTrabalho.Commit();

        var token = _tokenController.GerarToken(entidade.Email);

        return new RespostaUsuarioRegistradoJson
        {
            Token = token
        };
    }

    private async Task Validar(RequestRegistrarUsuarioJson request)
    {
        var validator = new RegistrarUsuarioValidator();
        var resultado = validator.Validate(request);

        var existeUsuarioComEmail = await _usuarioReadOnlyRepositorio.ExisteUsuarioComEmail(request.Email);
        if (existeUsuarioComEmail)
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMensagensDeErro.EMAIL_JA_REGISTRADO));
        }

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
