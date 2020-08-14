using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilidades;

namespace MeetUp
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objUser"] == null)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                Inicializar();
                ValidarAcceso();
            }

        }

        private void ValidarAcceso()
        {
            Usuario objUser = (Usuario)Session["objUser"];
            List<Componente> iComponentes = objUser.iPerfiles.SelectMany(p => p.iComponentes).Distinct().ToList();
            List<WebControl> iControls = new List<WebControl>() { pnlUsuarios };
            foreach (WebControl control in iControls)
            {
                control.Visible = (iComponentes.Where(c => c.strDetalleComponente == control.ID).ToList().Count > 0);
            }
        }

        private void Inicializar()
        {
            CargarGrillaUsuario();
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
        #region Usuario
        private void LimpiarCamposUsuario()
        {
            try
            {
                List<Control> iControl = new List<Control>() { txtUsuario, txtNombre, txtApellido, txtCorreo, txtDocumento, HFintIdUsuario };
                Funciones.LimpiarCampos(iControl);
                btnGuardarUsuario.Visible = true;
                tvPerfiles.Nodes.Clear();
                upTvPerfiles.Update();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void CargarGrillaUsuario()
        {
            try
            {
                UsuarioDA objDA = new UsuarioDA();
                gvUsuario.DataSource = objDA.Cargar(new Usuario());
                gvUsuario.DataBind();
                upGridUsuario.Update();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void CargarTreeViewPerfil(Usuario obj)
        {
            try
            {
                tvPerfiles.Nodes.Clear();
                PerfilDA objN = new PerfilDA();
                List<Perfil> iPerfil = new List<Perfil>();
                PerfilDA oPerfilDA = new PerfilDA();
                iPerfil = oPerfilDA.Cargar(new Perfil());
                ViewState["iPerfil"] = iPerfil;

                foreach (Perfil perfil in iPerfil)
                {
                    TreeNode nodo = new TreeNode();

                    if (obj.iPerfiles.Where(p => p.intId == perfil.intId).ToList().Count > 0)
                        nodo.Checked = true;

                    nodo.ShowCheckBox = true;
                    nodo.Value = new JavaScriptSerializer().Serialize(perfil);
                    nodo.Text = string.Format("<i class=\"now-ui-icons  location_bookmark\"></i> <span>{0}</span>", perfil.strDescrip);

                    tvPerfiles.Nodes.Add(nodo);
                }
                upTvPerfiles.Update();
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposUsuario();
                Usuario obj = new Usuario();
                CargarTreeViewPerfil(obj);
                pnlUsuario.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
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

                    TreeNodeCollection iTreePerfiles = tvPerfiles.CheckedNodes;
                    foreach (TreeNode nodo in iTreePerfiles)
                    {
                        Perfil objPerfil = new JavaScriptSerializer().Deserialize<Perfil>(nodo.Value);
                        obj.iPerfiles.Add(objPerfil);
                    }

                    UsuarioDA objDA = new UsuarioDA();
                    if (obj.intIdUsuario == 0)
                    {
                        obj.strContrasenia = Funciones.Encrypt("123456");
                        r = objDA.Guardar(obj);
                    }
                    else
                        r = objDA.Actualizar(obj);

                    if (r > 0)
                    {
                        pnlUsuario.Visible = false;
                        CargarGrillaUsuario();
                        Funciones.LimpiarCampos(new List<Control>() { txtUsuario });
                        Mensaje("Se registró el Usuario correctamente.", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                    }
                    else
                        Mensaje("No fue posible guardar el Usuario, intente nuevamente", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);

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
        protected void gvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView _gv = (GridView)sender;
            _gv.PageIndex = e.NewPageIndex;
            CargarGrillaUsuario();
        }
        protected void gvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int intId = Convert.ToInt32(e.CommandArgument);
            HFintIdUsuario.Value = Convert.ToString(intId);
            UsuarioDA objDA = new UsuarioDA();
            Usuario obj = new Usuario();
            obj.intIdUsuario = intId;
            switch (e.CommandName)
            {
                case "editItem":
                    obj = objDA.Cargar(obj).FirstOrDefault();
                    CargarCamposUsuario(obj);
                    pnlUsuario.Visible = true;
                    break;
                case "reestablecerContrasenia":
                    obj.strContrasenia = Funciones.Encrypt("123456");
                    int r = objDA.ReestablecerContrasenia(obj);
                    if (r > 0)
                    {
                        Mensaje("Se reestablecio la contraseña correctamente", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                        CargarGrillaUsuario();
                    }
                    else
                        Mensaje("No fue posible reestablecer la contraseña", "Problemas", AlertIcon.Icon.Ban, CssClasses.CssClass.Warning);
                    break;
            }
        }
        private void CargarCamposUsuario(Usuario obj)
        {
            try
            {
                LimpiarCamposUsuario();

                txtUsuario.Text = obj.strUsuario;
                txtNombre.Text = obj.strNombre;
                txtApellido.Text = obj.strApellido;
                txtCorreo.Text = obj.strCorreoUsuario;
                txtDocumento.Text = obj.strNumeroDocumento;
                btnGuardarUsuario.Visible = true;
                HFintIdUsuario.Value = Convert.ToString(obj.intIdUsuario);
                CargarTreeViewPerfil(obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        private void Mensaje(string strMessage, string strTitle, AlertIcon.Icon Icon, CssClasses.CssClass Class)
        {
            Main main = (Main)this.Master;
            main.MostrarMensaje(Icon, Class, strTitle, strMessage);
        }
    }
}