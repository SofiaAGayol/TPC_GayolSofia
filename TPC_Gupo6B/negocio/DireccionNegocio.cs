using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class DireccionNegocio
    {
        public List<Direccion> ListarPorUsuario(int idUsuario)
        {
            // Lógica para obtener las direcciones de un usuario específico desde la base de datos
            return new List<Direccion>();
        }

        public void Agregar(Direccion direccion)
        {
            // Lógica para agregar una nueva dirección a la base de datos
        }
    }
}
