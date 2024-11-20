using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using TPC_GayolSofia.dominio;


namespace dominio
{
    public class Prestamo
    {
        public int IDPrestamo { get; set; }
        public Usuario Usuario { get; set; }
        public List<Libro> Libros { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public MetodoDeEnvio MetodoEnvio { get; set; }
        public MetodoDeRetiro MetodoRetiro { get; set; }
        public decimal CostoEnvio { get; set; }
        public string Estado { get; set; }
        public bool Devuelto { get; set; }
        public Direccion Direccion { get; set; }
        public Prestamo(int idPrestamo, Usuario usuario, List<Libro> libros, DateTime fechaInicio, DateTime fechaFin,
        bool devuelto, string estado, MetodoDeEnvio metodoEnvio, MetodoDeRetiro metodoRetiro, Direccion direccion, decimal costoEnvio)
        {
            IDPrestamo = idPrestamo;
            Usuario = usuario;
            Libros = libros;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Devuelto = devuelto;
            Estado = estado;
            MetodoEnvio = metodoEnvio;
            MetodoRetiro = metodoRetiro;
            Direccion = direccion;
            CostoEnvio = costoEnvio;
        }
        public Prestamo() { }
    }
}
