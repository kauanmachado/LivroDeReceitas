namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar
{
    public interface IEncriptadorDeSenha
    {
        string Criptografar(string senha);
    }
}