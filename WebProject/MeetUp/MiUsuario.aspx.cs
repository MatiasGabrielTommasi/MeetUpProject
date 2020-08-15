using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilidades;

namespace MeetUp
{
    public partial class MiUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objUser"] == null)
                Response.Redirect("~/Login.aspx");

        }

        protected void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            if (ValidarcamposContrasenia())
            {
                Usuario obj = (Usuario)Session["objUser"];
                obj.Contrasenia = Funciones.Encrypt(txtContrasenia.Text);

                UsuarioDA objDA = new UsuarioDA();
                int r = objDA.CambiarContrasenia(obj);
                if(r > 0)
                {
                    Session["objUser"] = obj;
                    Response.Redirect("~/Reservas.aspx");
                }
                else
                    Mensaje("Error al actualizar su contraseña, intente nuevamente", "Inconvenientes", AlertIcon.Icon.Ban, CssClasses.CssClass.Warning);
            }
        }

        private bool ValidarcamposContrasenia()
        {
            bool r = true;
            try
            {
                List<WebControl> iControl = new List<WebControl>() { txtContrasenia, txtContrasenia2 };
                string strMessage = string.Empty;
                r = Funciones.ValidarCamposConRetorno(iControl, ref strMessage);
                if (!r)
                    Mensaje(strMessage, "Revise los campos", AlertIcon.Icon.Ban, CssClasses.CssClass.Warning);
                else if(txtContrasenia.Text != txtContrasenia2.Text)
                {
                    r = false;
                    Mensaje("Las contraseñas deben ser iguales!", "Contraseña errónea!" , AlertIcon.Icon.Ban, CssClasses.CssClass.Warning);
                }
            }
            catch (Exception ex)
            {
                r = false;
                Mensaje("Error al validar los campos, intente nuevamente", "Inconvenientes", AlertIcon.Icon.Ban, CssClasses.CssClass.Warning);
            }
            return r;
        }

        private void Mensaje(string strMessage, string strTitle, AlertIcon.Icon Icon, CssClasses.CssClass Class)
        {
            Main main = (Main)this.Master;
            main.MostrarMensaje(Icon, Class, strTitle, strMessage);
        }
    }
}