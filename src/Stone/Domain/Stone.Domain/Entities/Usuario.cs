using System;
using Domain.Core.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stone.Domain.Entities
{
    public class Usuario : Entity
    {
        public Usuario(string login, string senha, string clienteId)
        {
            Login = login;
            Senha = senha;
            ClienteId = clienteId;
        }

		public string ClienteId { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; set; }

        public void AlterarSenha(string senha)
        {
            Senha = senha;
        }

        public override bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}