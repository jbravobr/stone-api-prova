using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Stone.Domain.Entities;

namespace Stone.API.ApplicationServices
{
    public interface IPagamentoApplicationService
    {
        Task Adicionar(Pagamento obj);
        Task<Pagamento> ObterPorId(string id);
        Task Atualizar(Pagamento obj);
        Task Remover(string id);
        Task<IEnumerable<Pagamento>> Buscar(Expression<Func<Pagamento, bool>> predicate);
        Task<Pagamento> ObterMaisRecente();
        Task<IEnumerable<Pagamento>> BuscarTodos();
    }
}