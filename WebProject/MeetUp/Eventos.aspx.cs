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
    public partial class Eventos : System.Web.UI.Page
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
            List<Componente> iComponentes = objUser.Perfiles.SelectMany(p => p.Componentes).Distinct().ToList();
            List<WebControl> iControls = new List<WebControl>() { pnlEventos };
            foreach (WebControl control in iControls)
            {
                control.Visible = (iComponentes.Where(c => c.Detalle == control.ID).ToList().Count > 0);
            }
        }

        private void Inicializar()
        {
            CargarGrillaEvento();
            CargarCoboSalas();
        }
        private void CargarCoboSalas()
        {
            try
            {
                SalaDA objDA = new SalaDA();
                cboSala.DataSource = objDA.Cargar(new Sala());
                cboSala.DataValueField = "intIdSala";
                cboSala.DataTextField = "strSala";
                cboSala.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        #region Evento
        private void LimpiarCamposEvento()
        {
            try
            {
                List<Control> iControl = new List<Control>() { txtNombre, txtCupo, txtFecha, HFintIdEvento };
                Funciones.LimpiarCampos(iControl);
                btnGuardarEvento.Visible = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void CargarGrillaEvento()
        {
            try
            {
                EventoDA objDA = new EventoDA();
                List<Evento> obj = objDA.Cargar(new Evento());
                List<ApiTemperature> iTemp = Funciones.ObtenerListadoTemperaturas();
                foreach (Evento evento in obj)
                {
                    List<ApiTemperature> ExisteFecha = iTemp.Where(t => t.Fecha.Date == evento.DiaSeleccionado.Fecha.Date).ToList();
                    if (ExisteFecha.Count > 0)
                    {
                        evento.DiaSeleccionado.Fecha = ExisteFecha.First().Fecha;
                        evento.DiaSeleccionado.Temperatura = ExisteFecha.First().Temperatura;
                    }
                }
                gvEvento.DataSource = obj;
                gvEvento.DataBind();
                upGridEvento.Update();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnNuevoEvento_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposEvento();
                int intIdSala = Convert.ToInt32(cboSala.SelectedValue);
                CargarCalendario(intIdSala, "calendar-reservation");
                pnlEvento.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnGuardarEvento_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposEvento())
                {
                    SalaDA objSalaDA = new SalaDA();
                    Sala objSala = new Sala();
                    objSala.Id = Convert.ToInt32(cboSala.SelectedValue);
                    objSala = objSalaDA.Cargar(objSala).First();
                    if (objSala.Cupo >= Convert.ToInt32(txtCupo.Text))
                    {

                        int r = 0;
                        int intId = Convert.ToInt32(HFintIdEvento.Value);
                        Evento obj = new Evento();
                        obj.Id = Convert.ToInt32(HFintIdEvento.Value);
                        obj.TotalAsistentes = Convert.ToInt32(txtCupo.Text);
                        obj.Nombre = txtNombre.Text;
                        obj.DiaSeleccionado.Fecha = Convert.ToDateTime(txtFecha.Text);
                        obj.Salon.Id = Convert.ToInt32(cboSala.SelectedValue);
                        obj.Salon.Nombre = Convert.ToString(cboSala.SelectedItem);
                        obj.UsuarioAnfitrion = (Usuario)Session["objUser"];

                        EventoDA objDA = new EventoDA();
                        if (obj.Id == 0)
                            r = objDA.Guardar(obj);
                        else
                            r = objDA.Actualizar(obj);

                        if (r > 0)
                        {
                            pnlEvento.Visible = false;
                            CargarGrillaEvento();
                            Funciones.LimpiarCampos(new List<Control>() { txtNombre, txtCupo, HFintIdEvento });
                            Mensaje("Se registró el Evento correctamente.", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                        }
                        else
                        {
                            Mensaje("No fue posible guardar el Evento, intente nuevamente", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                            int intIdSala = Convert.ToInt32(cboSala.SelectedValue);
                            CargarCalendario(intIdSala, "calendar-reservation");
                        }
                    }
                    else
                    {
                        Mensaje("La cantidad de invitados no debe superar el cupo maximo de personas de la sala.", "Cantidad de personas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                        int intIdSala = Convert.ToInt32(cboSala.SelectedValue);
                        CargarCalendario(intIdSala, "calendar-reservation");
                    }

                }
            }
            catch (Exception ex)
            {
                Mensaje("Ocurrió un inconveniente al registrar el Evento", "Problemas!", AlertIcon.Icon.Ban, CssClasses.CssClass.Danger);
            }
        }
        private bool ValidarCamposEvento()
        {
            bool r = true;
            try
            {
                List<WebControl> iControl = new List<WebControl>() { txtNombre, txtCupo };
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
        protected void gvEvento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView _gv = (GridView)sender;
            _gv.PageIndex = e.NewPageIndex;
            CargarGrillaEvento();
        }
        protected void gvEvento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] _argument = e.CommandArgument.ToString().Split('-');
            int intIdEvento = Convert.ToInt32(_argument[0]);
            int intIdSala = Convert.ToInt32(_argument[1]);
            HFintIdEvento.Value = Convert.ToString(intIdEvento);
            EventoDA objDA = new EventoDA();
            Evento obj = new Evento();
            switch (e.CommandName)
            {
                case "editItem":
                    obj.Id = intIdEvento;
                    obj = objDA.Cargar(obj).FirstOrDefault();
                    CargarCamposEvento(obj);
                    pnlEvento.Visible = true;
                    break;
                case "deleteItem":
                    EventoDA evtDA = new EventoDA();
                    obj.Id = intIdEvento;
                    int r = objDA.Eliminar(obj);
                    if (r > 0)
                    {
                        Mensaje("Se eliminó el Evento correctamente", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                        CargarGrillaEvento();
                    }
                    else
                        Mensaje("No fue posible eliminar el Tipo de Documento indicado", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                    break;
            }
        }

        private void CargarCalendario(int intIdSala, string strCalendario)
        {
            try
            {
                List<Evento> obj = new List<Evento>();
                EventoDA objDA = new EventoDA();
                Evento objEvento = new Evento();
                objEvento.Salon.Id = intIdSala;
                obj = objDA.Cargar(objEvento);

                string strEventosJson = string.Empty;
                for (int i = 0; i < obj.Count; i++)
                {
                    if (obj[i].DiaSeleccionado.Fecha.Date >= DateTime.Now.Date)
                    {
                        string strEvento = obj[i].ToEventoCalendarioString();

                        if (i != obj.Count - 1)
                            strEvento += ",";

                        strEventosJson += strEvento;
                    }
                }
                strEventosJson = string.Format("[{0}]", strEventosJson);
                string _js = string.Format("CargarCalendario({0}, '{1}');", strEventosJson, strCalendario);
                Main main = (Main)this.Master;
                main.EjecutarScript(_js, "jsEventosCalendario");
            }
            catch (Exception ex)
            {
            }
        }

        private void CargarCamposEvento(Evento obj)
        {
            try
            {
                LimpiarCamposEvento();

                txtNombre.Text = obj.Nombre;
                txtCupo.Text = Convert.ToString(obj.TotalAsistentes);
                txtFecha.Text = obj.DiaSeleccionado.Fecha.ToString("yyyy/MM/aa");
                cboSala.SelectedValue = obj.Salon.Id.ToString();
                HFintIdEvento.Value = Convert.ToString(obj.Id);

                CargarCalendario(obj.Salon.Id, "calendar-reservation");

                pnlEvento.Visible = true;
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

        protected void cboSala_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList _cbo = (DropDownList)sender;
                int intIdSala = Convert.ToInt32(_cbo.SelectedValue);
                CargarCalendario(intIdSala, "calendar-reservation");

                Sala objSala = new Sala();
                objSala.Id = intIdSala;
                SalaDA objSalaDA = new SalaDA();
                objSala = objSalaDA.Cargar(objSala).First();
                smCantidadMaxima.InnerText = string.Format("{0} como máximo", Convert.ToString(objSala.Cupo));
            }
            catch (Exception ex)
            {
            }
        }

        protected void gvEvento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridViewRow row = (GridViewRow)e.Row;
                    Evento obj = (Evento)row.DataItem;
                    Label lblFecha = (Label)row.FindControl("lblFecha");
                    Label lblCervezas = (Label)row.FindControl("lblCervezas");

                    if (obj.DiaSeleccionado.Temperatura > 0)
                        lblFecha.Text = string.Format("{0:dd/MM/yyyy} ({1}°)", obj.DiaSeleccionado.Fecha, obj.DiaSeleccionado.Temperatura);
                    else
                        lblFecha.Text = string.Format("{0:dd/MM/yyyy}", obj.DiaSeleccionado.Fecha);

                    double cervezas = 0;
                    if (obj.DiaSeleccionado.Temperatura < 20)//toman 0.75
                        cervezas = obj.TotalReservas * 0.75;
                    else if(obj.DiaSeleccionado.Temperatura >= 20 && obj.DiaSeleccionado.Temperatura <= 24)//toman 1
                        cervezas = obj.TotalReservas;
                    else if(obj.DiaSeleccionado.Temperatura > 24)//toman 3                        
                        cervezas = obj.TotalReservas * 3;

                    lblCervezas.Text = string.Format("{0} cajas de cerveza", Convert.ToInt32(Math.Ceiling(cervezas/6)));

                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}