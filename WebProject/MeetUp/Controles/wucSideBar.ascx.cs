using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MeetUp.Controles
{
    public partial class wucSideBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();
        }

        private void Inicializar()
        {
            if (Session["objUser"] != null)
            {
                Usuario obj = (Usuario)Session["objUser"];
                CargarMenuVertical(obj);
            }
            else
                litMenuVertical.Text = "<li class=\"active\"><a href=\"Login.aspx\" ><i class=\"now-ui-icons objects_key-25\"></i><p>Iniciar Sesión</p></a></li>";
        }

        private void CargarMenuVertical(Usuario obj)
        {
            try
            {
                List<Componente> iComponentes = obj.iPerfiles.SelectMany(p => p.iComponentes).Distinct().ToList();
                Componente ComponenteMenu = iComponentes.Where(c => c.strDetalleComponente == "V").ToList().FirstOrDefault();
                List<Componente> iMenuVertical = iComponentes.Where(c => c.intIdComponentePadre == ComponenteMenu.intIdComponente).ToList();

                liCerrarSesion.Visible = true;
                string strMenuVertical = string.Empty;
                foreach (Componente boton in iMenuVertical)
                {
                    strMenuVertical += string.Format("<li>" +
                                                        "<a href = \"{0}\">" +
                                                            "<i class=\"now-ui-icons {1}\"></i>" +
                                                            "<p>{2}</p>" +
                                                        "</a>" +
                                                    "</li>", boton.strUrlComponente, boton.strIconoComponente, boton.strComponente);

                }

                litMenuVertical.Text = Server.HtmlDecode(strMenuVertical);
            }
            catch (Exception ex)
            {
                litMenuVertical.Text = "<li class=\"active\"><a href=\"./icons.html\" ><i class=\"now-ui-icons objects_key-25\"></i><p>Iniciar Sesión</p></a></li>";
            }
        }
        protected void btnCerrarSesión_Click(object sender, EventArgs e)
        {
            try
            {
                Session["objUser"] = null;
                Response.Redirect("~/Login.aspx");
            }
            catch (Exception ex)
            {
            }
        }
    }
}