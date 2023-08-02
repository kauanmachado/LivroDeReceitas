using FluentValidation;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Exceptions;
using System.Text.RegularExpressions;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioValidator : AbstractValidator<RequestRegistrarUsuarioJson>
{
    public RegistrarUsuarioValidator()
    {
        RuleFor(c => c.Nome).NotEmpty().WithMessage(ResourceMensagensDeErro.NOME_VAZIO);

        RuleFor(c => c.Email).NotEmpty().WithMessage(ResourceMensagensDeErro.EMAIL_VAZIO);
        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceMensagensDeErro.EMAIL_INVALIDO);
        });
       

        RuleFor(c => c.Senha).NotEmpty().WithMessage(ResourceMensagensDeErro.SENHA_VAZIA);
        When(c => !string.IsNullOrWhiteSpace(c.Senha), () =>
        {
           RuleFor(c => c.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMensagensDeErro.SENHA_MINIMO_SEIS_CARACTERES);
        });
       

        RuleFor(c => c.Telefone).NotEmpty().WithMessage(ResourceMensagensDeErro.TELEFONE_VAZIO);
        When(c => !string.IsNullOrWhiteSpace(c.Telefone), () =>
        {
            RuleFor(c => c.Telefone).Custom((telefone, context) =>
            {
                string padraoTelefone = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var IsMatch = Regex.IsMatch(telefone, padraoTelefone);
                if (!IsMatch)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(telefone), ResourceMensagensDeErro.TELEFONE_INVALIDO));
                }
            });
        });
        
        
    }
}
