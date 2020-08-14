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
    public partial class Registrarse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                CargarComboTipoDocumento();
        }
        private void CargarComboTipoDocumento()
        {
            try
            {
                TipoDocumentoDA objDA = new TipoDocumentoDA();
                cboTipoDocumento.DataSource = objDA.Cargar(new TipoDocumento());
                cboTipoDocumento.DataValueField = "intId";
                cboTipoDocumento.DataTextField = "strDescrip";
                cboTipoDocumento.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposUsuario())
                {
                    int r = 0;
                    int intId = Convert.ToInt32(HFintIdUsuario.Value);
                    string strProfile = txtUsuario.Text;
                    Usuario obj = new Usuario();
                    obj.intIdUsuario = Convert.ToInt32(HFintIdUsuario.Value);
                    obj.oTipoDocumento = new TipoDocumento(Convert.ToInt32(cboTipoDocumento.SelectedValue), Convert.ToString(cboTipoDocumento.SelectedItem));
                    obj.strApellido = txtApellido.Text;
                    obj.strCorreoUsuario = txtCorreo.Text;
                    obj.strNombre = txtNombre.Text;
                    obj.strNumeroDocumento = txtDocumento.Text;
                    obj.strUsuario = txtUsuario.Text;

                    obj.iPerfiles.Add(new Perfil(4, string.Empty));

                    UsuarioDA objDA = new UsuarioDA();
                    if (obj.intIdUsuario == 0)
                    {
                        obj.strContrasenia = Funciones.Encrypt("123456");
                        r = objDA.Guardar(obj);
                    }

                    if (r > 0)
                    {
                        obj = objDA.Login(obj.strUsuario);
                        Session["objUser"] = obj;
                        Response.Redirect("~/Default.aspx");
                    }
                    else
                        Mensaje("No fue posible registrarse, intente nuevamente", "Registro!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);

                }
            }
            catch (Exception ex)
            {
                Mensaje("Ocurrió un inconveniente al registrar el Usuario", "Problemas!", AlertIcon.Icon.Ban, CssClasses.CssClass.Danger);
            }
        }
        private bool ValidarCamposUsuario()
        {
            bool r = true;
            try
            {
                List<WebControl> iControl = new List<WebControl>() { txtUsuario, txtApellido, txtNombre, txtCorreo, txtDocumento };
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