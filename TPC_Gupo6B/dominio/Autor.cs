using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Autor
    {
        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Nacionalidad { get; set; }
        public string BestSeller { get; set; }

        public override string ToString()
        {
            string nombreCompleto = Nombre+" " + Apellido;
            return nombreCompleto;
        }

        // Propiedad calculada que devuelve el nombre completo
        public string NombreCompleto => $"{Nombre} {Apellido}";

        public Autor() { }
        public Autor(int idAutor, string nombre, string apellido)
        {
            IdAutor = idAutor;
            Nombre = nombre;
            Apellido = apellido;
        }

    }
}
