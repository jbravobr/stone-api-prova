using System.Threading.Tasks;
using RestEase;
using Stone.Domain.Entities;

namespace Stone.API.Helpers
{
    [Header("User-Agent", "RestEase")]
    public interface IReceitaWS
    {
        [Get("cnpj/{cnpj}")]
        Task<Estabelecimento> GetEstabelecimentoAsync([Path] string cnpj);
    }
}
