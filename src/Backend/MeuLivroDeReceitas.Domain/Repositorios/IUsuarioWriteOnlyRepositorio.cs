using MeuLivroDeReceitas.Domain.Entities;

namespace MeuLivroDeReceitas.Domain.Repositorios;

    public interface IUsuarioWriteOnlyRepositorio
    {
    Task Adicionar(Usuario usuario);

    }

