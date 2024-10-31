using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_GayolSofia.dominio
{
    public class TipoMembresia
    {
        private int IdTipoMembresia { get; set; }
        private string Descripcion { get; set; }
        private int LibrosALaVez { get; set; }
        private int LibrosXMes { get; set; }
        private float Valor { get; set; }
        private int Duracion { get; set; }
        private bool Estado { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}