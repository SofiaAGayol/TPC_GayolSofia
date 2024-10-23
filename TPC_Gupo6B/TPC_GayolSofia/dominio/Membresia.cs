using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_GayolSofia.dominio
{
    public class Membresia
    {
        private int IdMembresia {  get; set; }
        private Cliente Cliente { get; set; }
        private TipoMembresia TipoMembresia { get; set; }
        private DateTime? FechaInicio { get; set; }
        private DateTime? FechaFin { get; set; }
        private Pago Pago { get; set; }
        private bool Estado {  get; set; }
        
    }
}