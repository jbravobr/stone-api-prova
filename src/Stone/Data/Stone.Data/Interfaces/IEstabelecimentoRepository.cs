using System.Threading.Tasks;
using Domain.Core.Interfaces;
using Stone.Domain.Entities;

namespace Stone.Data.Interfaces
{
    public interface IEstabelecimentoRepository : IRepository<Estabelecimento>
    {
         Task<Estabelecimento> ObterMaisRecente();
    }
}