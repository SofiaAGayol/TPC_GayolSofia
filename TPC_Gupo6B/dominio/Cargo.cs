using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_GayolSofia.dominio
{
    public class Cargo
    {
        public int IdCargo { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}