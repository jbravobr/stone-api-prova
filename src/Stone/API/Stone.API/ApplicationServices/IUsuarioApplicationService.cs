using System.Threading.Tasks;
using Domain.Core;
using Stone.Domain.Entities;

namespace Stone.API.ApplicationServices
{
    public interface IUsuarioApplicationService : IBaseApplicationService<Usuario>
    {
        Task<Usuario> Login(Usuario usuario);
    }
}