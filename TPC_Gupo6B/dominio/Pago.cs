using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_GayolSofia.dominio
{
    public class Pago
    {
        public int IdPago { get; set; }
        public Usuario Usuario { get; set; }
        public TipoMembresia TipoMembresia { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal Importe { get; set; }
        public bool Estado { get; set; }
    }
}