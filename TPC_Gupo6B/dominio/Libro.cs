using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dominio
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public Autor Autor { get; set; }
        public Categoria Categoria { get; set; }
        public int AñoPublicacion { get; set; }
        public int Ejemplares { get; set; }        
        public int Disponibles { get; set; } 
        public bool Estado { get; set; }
        public string Imagen { get; set; }

        // Constructor
        public Libro(string titulo, Autor autor, Categoria categoria, int añoPublicacion, int ejemplares, string imagen)
        {
            Titulo = !string.IsNullOrEmpty(titulo) ? titulo : throw new ArgumentException("El título no puede estar vacío.");
            Autor = autor ?? throw new ArgumentNullException(nameof(autor));
            Categoria = categoria ?? throw new ArgumentNullException(nameof(categoria));
            AñoPublicacion = añoPublicacion > 0 ? añoPublicacion : throw new ArgumentException("Año de publicación no válido.");
            Ejemplares = ejemplares >= 0 ? ejemplares : throw new ArgumentException("El número de ejemplares no puede ser negativo.");
            Disponibles = Ejemplares;
            Estado = true; // Inicialmente, el libro está disponible
            Imagen = imagen;
        }

        public Libro() { }

        public bool HayEjemplaresDisponibles()
        {
            return Disponibles > 0;
        }

        public override string ToString()
        {
            return Titulo;
        }
    }
}
