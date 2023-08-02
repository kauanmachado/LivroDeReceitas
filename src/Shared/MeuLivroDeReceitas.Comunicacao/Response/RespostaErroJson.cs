namespace MeuLivroDeReceitas.Comunicacao.Response;

public class RespostaErroJson
{
    public List<string> Mensagens { get; set; }

    public RespostaErroJson(string mensagem)
    {
        Mensagens = new List<string>
        {
            mensagem
        };
    }

    public RespostaErroJson(List<String> mensagens) { 
        Mensagens = mensagens;
    }
}
