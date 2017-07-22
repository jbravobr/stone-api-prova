using System.Threading.Tasks;
using Domain.Core.Interfaces;
using Stone.Domain.Entities;

namespace Stone.Data.Interfaces
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        Task<Pagamento> ObterMaisRecente();
    }
}