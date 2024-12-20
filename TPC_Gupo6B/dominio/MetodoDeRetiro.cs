﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class MetodoDeRetiro
    {
        public int IdMetodoRetiro { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoAMBA { get; set; }
        public decimal CostoExterior { get; set; }
        public override string ToString()
        {
            return Descripcion;
        }
        public MetodoDeRetiro() { }
        public MetodoDeRetiro(int idMetodoRetiro, string descripcion)
        {
            IdMetodoRetiro = idMetodoRetiro;
            Descripcion = descripcion;
        }
    }
}
