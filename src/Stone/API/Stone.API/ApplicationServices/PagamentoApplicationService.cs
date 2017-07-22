using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Stone.API.ApplicationServices;
using Stone.Data.Interfaces;
using Stone.Domain.Entities;

namespace Stone.API.Services
{
    public class PagamentoApplicationService : IPagamentoApplicationService
    {
        IPagamentoRepository _repository {get;}
        
        public PagamentoApplicationService(IPagamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task Adicionar(Pagamento obj)
        {
            await _repository.Adicionar(obj);
        }

        public async Task Atualizar(Pagamento obj)
        {
            await _repository.Atualizar(obj);
        }

        public async Task<IEnumerable<Pagamento>> Buscar(Expression<Func<Pagamento, bool>> predicate)
        {
            return await _repository.Buscar(predicate);
        }

        public async Task<Pagamento> ObterPorId(string id)
        {
            return await _repository.ObterPorId(id);
        }

        public async Task Remover(string id)
        {
            await _repository.Remover(id);
        }

        public async Task<Pagamento> ObterMaisRecente()
        {
            return await _repository.ObterMaisRecente();
        }

        public async Task<IEnumerable<Pagamento>> BuscarTodos()
        {
            return await _repository.BuscarTodos();
        }
    }
}