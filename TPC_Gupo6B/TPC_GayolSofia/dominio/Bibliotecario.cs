using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_GayolSofia.dominio
{
    public class Bibliotecario
    {
        public int IdBibliotecario { get; set; }
        public Usuario Usuario { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public Cargo cargo { get; set; }

    }
}