using System;
using Utilidades;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MeetUp
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EjecutarScript("bindScripts();", "jsBindScripts");
        }

        public void EjecutarScript(string _script, string _tipo)
        {
            try
            {
                if (ScriptManager.GetCurrent(this.Page) != null)
                {
                    if (ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), _tipo, _script, true);
                    else
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), _tipo, _script, true);
                }
                else
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), _tipo, _script, true);
            }
            catch (Exception ex)
            {
            }
        }
        public void MostrarMensaje(AlertIcon.Icon Icon, CssClasses.CssClass CssClass, string strTitle, string strMessage)
        {
            string _alert = "message";
            string _class = CssClasses.Default;
            string _iconSelected = AlertIcon.Info;

            switch (CssClass)
            {
                case CssClasses.CssClass.Info:
                    _class = CssClasses.Info;
                    break;
                case CssClasses.CssClass.Success:
                    _class = CssClasses.Success;
                    break;
                case CssClasses.CssClass.Warning:
                    _class = CssClasses.Warning;
                    break;
                case CssClasses.CssClass.Danger:
                    _class = CssClasses.Danger;
                    break;
            }

            switch (Icon)
            {
                case AlertIcon.Icon.Check:
                    _iconSelected = AlertIcon.Check;
                    break;
                case AlertIcon.Icon.Info:
                    _iconSelected = AlertIcon.Info;
                    break;
                case AlertIcon.Icon.Ban:
                    _iconSelected = AlertIcon.Ban;
                    break;
                case AlertIcon.Icon.ExclamationTriangle:
                    _iconSelected = AlertIcon.ExclamationTriangle;
                    break;
            }

            string _js = string.Format("showMessage('{0}', '{1}', '{2}', '{3}');", _iconSelected, _class, strTitle, strMessage);
            EjecutarScript(_js, "jsMessage");
        }

    }
}