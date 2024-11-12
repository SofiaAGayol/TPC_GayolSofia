﻿using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_GayolSofia.dominio;

namespace TPC_GayolSofia
{
    public partial class CrearUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Registro_Click(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            string usuario = Tb_Usuario.Text;
            string clave = Tb_Clave.Text;
            string nombre = Tb_Nombre.Text;
            string apellido = Tb_Apellido.Text;
            string dni = Tb_DNI.Text;
            string email = Tb_Email.Text;
            string telefono = Tb_Telefono.Text;

            string mensajeError = string.Empty;

            if (string.IsNullOrEmpty(usuario))
            {
                mensajeError = "Usuario no válido.";
            }
            else if (usuarioNegocio.ExisteUsuario(usuario))
            {
                mensajeError = "Usuario ya registrado.";
            }
            else if (string.IsNullOrEmpty(clave))
            {
                mensajeError = "Clave no válida.";
            }
            else if (string.IsNullOrEmpty(nombre))
            {
                mensajeError = "Nombre no válido.";
            }
            else if (string.IsNullOrEmpty(apellido))
            {
                mensajeError = "Apellido no válido.";
            }
            else if (string.IsNullOrEmpty(dni))
            {
                mensajeError = "DNI no válido.";
            }
            else if (usuarioNegocio.ExisteDNI(dni))
            {
                mensajeError = "DNI ya registrado.";
            }
            else if (string.IsNullOrEmpty(email))
            {
                mensajeError = "Email no válido.";
            }
            else if (usuarioNegocio.ExisteEmail(email))
            {
                mensajeError = "Email ya registrado.";
            }
            else if (string.IsNullOrEmpty(telefono))
            {
                mensajeError = "Telefono no válido.";
            }

            if (!string.IsNullOrEmpty(mensajeError))
            {
                alertMessage.InnerText = mensajeError;
                divAlert.Style["display"] = "block";
                return;
            }
            //YA LE DEJAMOS PRECARGADO COMO ID ROL = 4 QUE ES EL ROL "CLIENTE"
            usuarioNegocio.Agregar(usuario, clave, nombre, apellido, dni, email, telefono, 4);

            pnlMensaje.Style["display"] = "block";
            divAlert.Style["display"] = "none";
        }

        protected void btnCerrarMensaje_Click(object sender, EventArgs e)
        {
            pnlMensaje.Style["display"] = "none";
            Response.Redirect("Login.aspx");
        }
    }
}