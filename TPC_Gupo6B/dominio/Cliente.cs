using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;

namespace dominio
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public Usuario Usuario { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public Membresia Membresia { get; set; }
        public Cliente(int id, int documento, Usuario usuario, string nombre, string apellido, string telefono, string email, Membresia membresia)
        {
            IdCliente = id;
            Usuario = usuario;
            DNI = documento;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Telefono = telefono;
            Membresia = membresia;
        }

        public Cliente() { }
    }
}
