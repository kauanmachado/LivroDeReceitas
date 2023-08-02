using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionBase;
using Utilitario.ParaTestes.Mapper;
using Utilitario.ParaTestes.Criptografia;
using Utilitario.ParaTestes.Repositorios;
using Utilitario.ParaTestes.Requests;
using Utilitario.ParaTestes.Token;
using Xunit;

namespace UseCases.Test.Usuario.Registrar;

public class RegistrarUsuarioUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        var request = RequestRegistrarUsuarioBuilder.Construir();

        var useCase = CriarUseCase();

        var resposta = await useCase.Executar(request);

        resposta.Should().NotBeNull();
        resposta.Token.Should().NotBeNullOrWhiteSpace();
        
    }

    [Fact]
    public async Task Validar_Erro_Email_Ja_Registrado()
    {
        var request = RequestRegistrarUsuarioBuilder.Construir();

        var useCase = CriarUseCase(request.Email);

        Func<Task> acao = async () => { await useCase.Executar(request); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 && exception.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_JA_REGISTRADO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Vazio()
    {
        var request = RequestRegistrarUsuarioBuilder.Construir();
        request.Email = string.Empty;

        var useCase = CriarUseCase();

        Func<Task> acao = async () => { await useCase.Executar(request); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 && exception.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_VAZIO));
    }

    private RegistrarUsuarioUseCase CriarUseCase(string email = "")
    {
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var mapper = MapperBuilder.Instancia();
        var repositorio = UsuarioWriteOnlyRepositorioBuilder.Instancia().Construir();
        var encriptadorDeSenha = EncriptadorDeSenhaBuilder.Instancia();
        var tokenController = TokenControllerBuilder.Instancia();
        var usuarioReadOnlyRepositorio = UsuarioReadOnlyRepositorioBuilder.Instancia().ExisteUsuarioComEmail(email).Construir();

        return new RegistrarUsuarioUseCase(repositorio, mapper, unidadeDeTrabalho, encriptadorDeSenha, tokenController, usuarioReadOnlyRepositorio);
    }
}
