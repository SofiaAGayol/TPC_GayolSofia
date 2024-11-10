using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;
using dominio;

namespace negocio
{
    public class MetodoPagoNegocio
    {
        public List<MetodoPago> ListarPorUsuario(int idUsuario)
        {
            // Lógica para obtener los métodos de pago de un usuario específico desde la base de datos
            return new List<MetodoPago>();
        }

        public void Agregar(MetodoPago metodoDePago)
        {
            // Lógica para agregar un nuevo método de pago a la base de datos
        }
    }
}
