using System;
using Domain.Core.Models;

namespace Stone.Domain.Entities
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public  string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCartao { get; set; }

        public override bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}