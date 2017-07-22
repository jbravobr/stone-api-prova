using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Stone.Domain.Entities;
using Stone.Data.Interfaces;


namespace Stone.API.ApplicationServices
{
    public class ClienteApplicationService : IClienteApplicationService
    {
        IClienteRepository _repository {get;}
        
        public ClienteApplicationService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task Adicionar(Cliente obj)
        {
            await _repository.Adicionar(obj);
        }

        public async Task Atualizar(Cliente obj)
        {
            await _repository.Atualizar(obj);
        }

        public async Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> predicate)
        {
            return await _repository.Buscar(predicate);
        }

        public async Task<Cliente> ObterPorId(string id)
        {
            return await _repository.ObterPorId(id);
        }

        public async Task Remover(string id)
        {
            await _repository.Remover(id);
        }

        public async Task<Cliente> ObterMaisRecente()
        {
            return await _repository.ObterMaisRecente();
        }

        public async Task<IEnumerable<Cliente>> BuscarTodos()
        {
            return await _repository.BuscarTodos();
        }
    }
}