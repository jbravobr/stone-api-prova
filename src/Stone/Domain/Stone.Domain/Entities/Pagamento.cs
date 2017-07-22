using System;
using Domain.Core.Models;

namespace Stone.Domain.Entities
{
    public class Pagamento : Entity
    {
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public string ClienteID { get; set; }
        public string EstabelecimentoID { get; set; }
        public bool IsCanceled { get; set; }

        public override bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}