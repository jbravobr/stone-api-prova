using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Stone.Data.Interfaces;
using Stone.Domain.Entities;

namespace Stone.API.ApplicationServices
{
    public class EstabelecimentoApplicationService : IEstabelecimentoApplicationService
    {
        IEstabelecimentoRepository _repository { get; }

        public EstabelecimentoApplicationService(IEstabelecimentoRepository repository)
        {
            _repository = repository;
        }

        public async Task Adicionar(Estabelecimento obj)
        {
            await _repository.Adicionar(obj);
        }

        public async Task Atualizar(Estabelecimento obj)
        {
            await _repository.Atualizar(obj);
        }

        public async Task<IEnumerable<Estabelecimento>> Buscar(Expression<Func<Estabelecimento, bool>> predicate)
        {
            return await _repository.Buscar(predicate);
        }

        public async Task<Estabelecimento> ObterPorId(string id)
        {
            return await _repository.ObterPorId(id);
        }

        public async Task Remover(string id)
        {
            await _repository.Remover(id);
        }

        public async Task<Estabelecimento> ObterMaisRecente()
        {
            return await _repository.ObterMaisRecente();
        }

        public async Task<IEnumerable<Estabelecimento>> BuscarTodos()
        {
            return await _repository.BuscarTodos();
        }
    }
}