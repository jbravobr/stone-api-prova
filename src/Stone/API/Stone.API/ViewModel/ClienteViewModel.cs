using System;

namespace Stone.API
{
    public class ClienteViewModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCartao { get; set; }
    }
}