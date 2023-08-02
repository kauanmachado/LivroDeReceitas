﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MeuLivroDeReceitas.Domain.Entities;

    public class Usuario : EntidadeBase
    {   
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set;}
        public string Telefone { get; set; }

    }

