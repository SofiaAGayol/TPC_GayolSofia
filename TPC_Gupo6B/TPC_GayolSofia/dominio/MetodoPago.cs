using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_GayolSofia.dominio
{
    public class MetodoPago
    {
        private int IdMetodoPago { get; set; }
        private Cliente Cliente { get; set; }
        private string TipoTarjeta { get; set; }
        private int NumeroTarjeta { get; set; }
        private DateTime? Vencimiento { get; set; }
        private int CodigoSeguridad { get; set; }
        
    }
}