using System.Threading.Tasks;
using Domain.Core.Interfaces;
using Stone.Domain.Entities;

namespace Stone.Data.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> BuscarPorLogin(string login);
    }
}