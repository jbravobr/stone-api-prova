using System.Threading.Tasks;
using Stone.Domain.Entities;
using Domain.Core.Interfaces;

namespace Stone.Data.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterMaisRecente();
    }
}