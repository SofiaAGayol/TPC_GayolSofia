using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;

namespace dominio
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        public Usuario Usuario { get; set; }
        public string Calle { get; set; }
        public int Altura { get; set; }
        public string CodigoPostal { get; set; }
        public string Aclaracion { get; set; }
        public bool Predeterminada { get; set; }
    }
}
