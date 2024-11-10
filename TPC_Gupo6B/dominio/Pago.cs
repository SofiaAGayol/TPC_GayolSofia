using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_GayolSofia.dominio
{
    public class Pago
    {
        private int IdPago { get; set; }
        public Usuario Usuario { get; set; }
        private TipoMembresia TipoMembresia { get; set; }
        private MetodoPago MetodoPago { get; set; }
        private DateTime? FechaPago { get; set; }
        private float Importe { get; set; }
        private bool Estado { get; set; }
    }
}