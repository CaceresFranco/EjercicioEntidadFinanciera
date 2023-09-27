using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
    public class TarjetaCredito
    {
        public int Id { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal SaldoDisponible { get; set; }
        public string Estado { get; set; }
        public int ClienteId { get; set; }
        public decimal MontoDeuda { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
