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
    public partial class Reservas : System.Web.UI.Page
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
            List<WebControl> iControls = new List<WebControl>() { pnlReservas };
            foreach (WebControl control in iControls)
            {
                control.Visible = (iComponentes.Where(c => c.Detalle == control.ID).ToList().Count > 0);
            }
        }

        private void Inicializar()
        {
            CargarGrillaReserva();
        }
        #region Reserva
        private void CargarGrillaReserva()
        {
            try
            {
                ReservaDA objDA = new ReservaDA();
                gvReserva.DataSource = objDA.Cargar(new Reserva());
                gvReserva.DataBind();
                upGridReserva.Update();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnNuevoReserva_Click(object sender, EventArgs e)
        {
            try
            {
                CargarCalendario("calendar");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnGuardarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                int r = 0;

                EventoDA objEventoDA = new EventoDA();
                Evento objEvento = new Evento();
                objEvento.Id = Convert.ToInt32(HFintIdEvento.Value);
                objEvento = objEventoDA.Cargar(objEvento).First();

                Reserva obj = new Reserva();
                obj.EventoSeleccionado = objEvento;
                obj.Asistente = (Usuario)Session["objUser"];

                ReservaDA objDA = new ReservaDA();
                r = objDA.Guardar(obj);

                if (r > 0)
                {
                    CargarGrillaReserva();
                    Mensaje("Se registró el Reserva correctamente.", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                }
                else
                {
                    Mensaje("No fue posible guardar el Reserva, intente nuevamente", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                    CargarCalendario("calendar");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Ocurrió un inconveniente al registrar el Reserva", "Problemas!", AlertIcon.Icon.Ban, CssClasses.CssClass.Danger);
            }
        }
        protected void gvReserva_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView _gv = (GridView)sender;
            _gv.PageIndex = e.NewPageIndex;
            CargarGrillaReserva();
        }
        protected void gvReserva_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int intIdReserva = Convert.ToInt32(e.CommandArgument.ToString());
            ReservaDA objDA = new ReservaDA();
            Reserva obj = new Reserva();
            switch (e.CommandName)
            {
                case "qrItem"://mostrar modal qr
                    obj.Id = intIdReserva;
                    obj = objDA.Cargar(obj).FirstOrDefault();
                    MostrarComprobanteReserva(obj);
                    break;
                case "deleteItem":
                    ReservaDA evtDA = new ReservaDA();
                    obj.Id = intIdReserva;
                    int r = objDA.Eliminar(obj);
                    if (r > 0)
                    {
                        Mensaje("Se eliminó el Reserva correctamente", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                        CargarGrillaReserva();
                    }
                    else
                        Mensaje("No fue posible eliminar el Tipo de Documento indicado", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                    break;
            }
        }

        private void CargarCalendario(string strCalendario)
        {
            try
            {
                List<Evento> obj = new List<Evento>();
                EventoDA objDA = new EventoDA();
                obj = objDA.Cargar(new Evento());

                string strReservasJson = string.Empty;
                for (int i = 0; i < obj.Count; i++)
                {
                    if (obj[i].DiaSeleccionado.Fecha.Date >= DateTime.Now.Date)
                    {
                        string strReserva = obj[i].ToReservaCalendarioString();

                        if (i != obj.Count - 1)
                            strReserva += ",";

                        strReservasJson += strReserva;
                    }
                }
                strReservasJson = string.Format("[{0}]", strReservasJson);
                string _js = string.Format("CargarCalendario({0}, '{1}');", strReservasJson, strCalendario);
                Main main = (Main)this.Master;
                main.EjecutarScript(_js, "jsReservasCalendario");
            }
            catch (Exception ex)
            {
            }
        }

        private void MostrarComprobanteReserva(Reserva obj)
        {
            try
            {
                string strEvento = string.Format("Evento: {0} <br/> Fecha: {1} <br/> Sala: {2} ({3}) <br/> Anfitrión: {4}, {5} <br/> Reservado el día: {6}",
                                                    obj.EventoSeleccionado.Nombre, obj.EventoSeleccionado.DiaSeleccionado.Fecha.ToString("dd/MM/yyyy"),
                                                    obj.EventoSeleccionado.Salon.Nombre, obj.EventoSeleccionado.Salon.Ubicacion
                                                    , obj.EventoSeleccionado.UsuarioAnfitrion.Apellido, obj.EventoSeleccionado.UsuarioAnfitrion.Nombre,
                                                    obj.Fecha.ToString("dd/MM/yyyy"));
                lblEventoQR.InnerHtml = strEvento;
                imgQR.ImageUrl = string.Format("data:image/jpeg;base64,{0}", obj.Qr);

                string _js = "$('#modalQR').modal('show');";
                Main main = (Main)this.Master;
                main.EjecutarScript(_js, "jsModalQR");
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