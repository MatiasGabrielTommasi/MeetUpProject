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
    public partial class Salas : System.Web.UI.Page
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
            List<WebControl> iControls = new List<WebControl>() { pnlSalas };
            foreach (WebControl control in iControls)
            {
                control.Visible = (iComponentes.Where(c => c.strDetalleComponente == control.ID).ToList().Count > 0);
            }
        }

        private void Inicializar()
        {
            CargarGrillaSala();
        }
        #region Sala
        private void LimpiarCamposSala()
        {
            try
            {
                List<Control> iControl = new List<Control>() { txtNombre, txtCupo, txtUbicacion, HFintIdSala };
                Funciones.LimpiarCampos(iControl);
                btnGuardarSala.Visible = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void CargarGrillaSala()
        {
            try
            {
                SalaDA objDA = new SalaDA();
                gvSala.DataSource = objDA.Cargar(new Sala());
                gvSala.DataBind();
                upGridSala.Update();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnNuevoSala_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposSala();
                Sala obj = new Sala();
                pnlSala.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnGuardarSala_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposSala())
                {
                    int r = 0;
                    int intId = Convert.ToInt32(HFintIdSala.Value);
                    Sala obj = new Sala();
                    obj.intIdSala = Convert.ToInt32(HFintIdSala.Value);
                    obj.intCupo = Convert.ToInt32(txtCupo.Text);
                    obj.strSala = txtNombre.Text;
                    obj.strUbicacion = txtUbicacion.Text;

                    SalaDA objDA = new SalaDA();
                    if (obj.intIdSala == 0)
                        r = objDA.Guardar(obj);
                    else
                        r = objDA.Actualizar(obj);

                    if (r > 0)
                    {
                        pnlSala.Visible = false;
                        CargarGrillaSala();
                        Funciones.LimpiarCampos(new List<Control>() { txtNombre, txtCupo, txtUbicacion, HFintIdSala });
                        Mensaje("Se registró el Sala correctamente.", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                    }
                    else
                        Mensaje("No fue posible guardar el Sala, intente nuevamente", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);

                }
            }
            catch (Exception ex)
            {
                Mensaje("Ocurrió un inconveniente al registrar el Sala", "Problemas!", AlertIcon.Icon.Ban, CssClasses.CssClass.Danger);
            }
        }
        private bool ValidarCamposSala()
        {
            bool r = true;
            try
            {
                List<WebControl> iControl = new List<WebControl>() { txtNombre, txtCupo, txtUbicacion };
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
        protected void gvSala_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView _gv = (GridView)sender;
            _gv.PageIndex = e.NewPageIndex;
            CargarGrillaSala();
        }
        protected void gvSala_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int intId = Convert.ToInt32(e.CommandArgument);
            HFintIdSala.Value = Convert.ToString(intId);
            SalaDA objDA = new SalaDA();
            Sala obj = new Sala();
            obj.intIdSala = intId;
            switch (e.CommandName)
            {
                case "editItem":
                    obj = objDA.Cargar(obj).FirstOrDefault();
                    CargarCamposSala(obj);
                    pnlSala.Visible = true;
                    break;
                case "eventosItem":
                    EventoDA evtDA = new EventoDA();
                    obj = objDA.Cargar(obj).FirstOrDefault();
                    Evento evt = new Evento();
                    evt.oSala = obj;
                    obj.iEventos = evtDA.Cargar(evt);
                    //cargargrillaeventos(o mejor el calendario)
                    CargarCalendario(obj);
                    break;
            }
        }

        private void CargarCalendario(Sala obj)
        {
            try
            {
                string strEventosJson = string.Empty;
                for (int i = 0; i < obj.iEventos.Count; i++)
                {
                    string strEvento = obj.iEventos[i].ToSalasCalendarioString();

                    if (i != obj.iEventos.Count - 1)
                        strEvento += ",";

                    strEventosJson += strEvento;
                }
                strEventosJson = string.Format("[{0}]", strEventosJson);
                string _js = string.Format("CargarCalendario({0});", strEventosJson);
                Main main = (Main)this.Master;
                main.EjecutarScript(_js, "jsSalasCalendario");
            }
            catch (Exception ex)
            {
            }
        }

        private void CargarCamposSala(Sala obj)
        {
            try
            {
                LimpiarCamposSala();

                txtNombre.Text = obj.strSala;
                txtCupo.Text = Convert.ToString(obj.intCupo);
                txtUbicacion.Text = obj.strUbicacion;
                HFintIdSala.Value = Convert.ToString(obj.intIdSala);
                pnlSala.Visible = true;
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