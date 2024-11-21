using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_GayolSofia.dominio
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public Rol Rol { get; set; }
        public bool Estado { get; set; }
        public override string ToString()
        {
            string nombreCompleto = Nombre + " " + Apellido;
            return nombreCompleto;
        }
        public string NombreCompleto => $"{Nombre} {Apellido}";
        public Usuario() { }
        public Usuario(int idUsuario, string nombreUsuario, string clave, string nombre, string apellido, string dni, string email, string telefono, Rol rol, bool estado)
        {
            IdUsuario = idUsuario;
            NombreUsuario = nombreUsuario;
            Clave = clave;
            Nombre = nombre;
            Apellido = apellido;
            DNI = dni;
            Email = email;
            Telefono = telefono;
            Rol = rol;
            Estado = estado;

        }
    }
}