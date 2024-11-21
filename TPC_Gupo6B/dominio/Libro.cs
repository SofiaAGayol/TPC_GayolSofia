using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;

namespace dominio
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int Ejemplares { get; set; }
        public int Disponibles { get; set; }
        public bool Estado { get; set; }
        public string Imagen { get; set; }
        public Autor Autor { get; set; }
        public Usuario usuario { get; set; }
        public Categoria Categoria { get; set; }
        public Prestamo Prestamo { get; set; }
        public List<Prestamo> Prestamos { get; set; }
    }
}

