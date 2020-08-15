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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposLogin())
                {
                    UsuarioDA objDa = new UsuarioDA();
                    string strUsuario = txtUsuario.Text;
                    Usuario obj = objDa.Login(strUsuario);
                    if (obj != null)
                    {
                        string strPass = Funciones.Decrypt(obj.Contrasenia);
                        if (txtContrasenia.Text.Equals(strPass) && txtUsuario.Text.Equals(obj.Username))
                        {
                            Session["objUser"] = obj;
                            Response.Redirect("~/Default.aspx");
                        }
                        else
                            Mensaje("Indique usuario y contraseña válidos", "Reintente", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                    }
                    else
                        Mensaje("Indique usuario y contraseña válidos", "Reintente", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                }
            }
            catch (Exception ex)
            {
                Mensaje("No fue posible iniciar sesión", "Problemas", AlertIcon.Icon.Ban, CssClasses.CssClass.Danger);
            }
        }

        private bool ValidarCamposLogin()
        {
            bool r = true;
            try
            {
                List<WebControl> iControl = new List<WebControl>() { txtUsuario, txtContrasenia };
                string strMessage = string.Empty;
                r = Funciones.ValidarCamposConRetorno(iControl, ref strMessage);
                if (!r)
                    Mensaje(strMessage, "Revise los campos", AlertIcon.Icon.Ban, CssClasses.CssClass.Warning);
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