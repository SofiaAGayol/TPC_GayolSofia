using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;

namespace dominio
{
    public class MetodoDeEnvio
    {
        public int IdMetodoEnvio { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoAMBA { get; set; }
        public decimal CostoExterior { get; set; }
        public override string ToString()
        {
            return Descripcion;
        }
        public MetodoDeEnvio() { }
        public MetodoDeEnvio(int idMetodoEnvio, string descripcion)
        {
            IdMetodoEnvio = idMetodoEnvio;
            Descripcion = descripcion;
        }

    }
}
