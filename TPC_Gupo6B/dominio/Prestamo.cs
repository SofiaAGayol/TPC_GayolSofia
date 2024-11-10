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
        public Libro Libro { get; set; }     
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public MetodoDeEnvio MetodoEnvio { get; set; } 
        public MetodoDeRetiro MetodoRetiro { get; set; } 
        public decimal CostoEnvio { get; set; }
        public string Estado { get; set; }
        public bool Devuelto { get; set; }

    }
}
