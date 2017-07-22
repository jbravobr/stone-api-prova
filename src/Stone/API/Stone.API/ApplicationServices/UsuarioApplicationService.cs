using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Stone.Data.Interfaces;
using Stone.Domain.Entities;

namespace Stone.API.ApplicationServices
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        IUsuarioRepository _repository { get; }
        public UsuarioApplicationService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task Adicionar(Usuario obj)
        {
            obj.AlterarSenha(CalculateHash(obj.Senha));

            await _repository.Adicionar(obj);
        }

        public async Task<Usuario> ObterPorId(string id)
        {
            return await _repository.ObterPorId(id);
        }

        public async Task Atualizar(Usuario obj)
        {
            obj.AlterarSenha(CalculateHash(obj.Senha));
            await _repository.Atualizar(obj);
        }

        public async Task Remover(string id)
        {
            await _repository.Remover(id);
        }

        public async Task<IEnumerable<Usuario>> Buscar(Expression<Func<Usuario, bool>> predicate)
        {
            return await _repository.Buscar(predicate);
        }

        public async Task<IEnumerable<Usuario>> BuscarTodos()
        {
            return await _repository.BuscarTodos();
        }

        public async Task<Usuario> ObterMaisRecente()
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> Login(Usuario login)
        {
            var user = await _repository.BuscarPorLogin(login.Login);
            if (user != null && user.Senha == CalculateHash(login.Senha))
                return user;
            return null;
        }

        public string CalculateHash(string input)
        {
            using (var algorithm = SHA512.Create()) //or MD5 SHA256 etc.
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}