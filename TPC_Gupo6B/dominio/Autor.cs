using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;

namespace dominio
{
    public class Autor
    {
        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Nacionalidad Nacionalidad { get; set; }
        public string BestSeller { get; set; }
        public bool Estado { get; set; }

        public override string ToString()
        {
            string nombreCompleto = Nombre+" " + Apellido;
            return nombreCompleto;
        }
        public string NombreCompleto => $"{Nombre} {Apellido}";

        public Autor() { }
        public Autor(int idAutor, string nombre, string apellido, Nacionalidad nacionalidad, string bestSeller)
        {
            IdAutor = idAutor;
            Nombre = nombre;
            Apellido = apellido;
            Nacionalidad = nacionalidad;
            BestSeller = bestSeller;
        }

    }
}
