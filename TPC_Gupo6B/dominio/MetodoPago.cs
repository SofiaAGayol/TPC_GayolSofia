using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_GayolSofia.dominio
{
    public class MetodoPago
    {
        public int IdMetodoPago { get; set; }
        public Usuario Usuario { get; set; }
        public string TipoTarjeta { get; set; }
        public string NroTarjeta { get; set; }
        public DateTime Vencimiento { get; set; }
        public string Cod { get; set; }

    }
}