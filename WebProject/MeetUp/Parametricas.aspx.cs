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
    public partial class Parametricas : System.Web.UI.Page
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

        private void Inicializar()
        {
            CargarGrillaPerfiles();
            CargarGrillaTipoComponente();
            CargarComboTipoComponente();
            CargarGrillaTipoDocumento();
            CargarTreeViewComponentes();
        }
        private void ValidarAcceso()
        {

        }
        #region Common
        private void Mensaje(string strMessage, string strTitle, AlertIcon.Icon Icon, CssClasses.CssClass Class)
        {
            Main main = (Main)this.Master;
            main.MostrarMensaje(Icon, Class, strTitle, strMessage);
        }
        #endregion
        #region Perfiles
        private void LimpiarCamposPerfil()
        {
            try
            {
                string _empty = string.Empty;
                string _cero = "0";
                txtPerfil.Text = _empty;
                btnGuardarPerfil.Visible = true;
                HFintIdPerfil.Value = _cero;
                tvComponentesPerfil.Nodes.Clear();
                upTvComponentesPerfil.Update();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void CargarGrillaPerfiles()
        {
            try
            {
                PerfilDA objDA = new PerfilDA();
                gvPerfil.DataSource = objDA.Cargar(new Perfil());
                gvPerfil.DataBind();
                upGridPerfil.Update();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void CargarTreeViewComponentes(Perfil oProfile)
        {
            try
            {
                tvComponentesPerfil.Nodes.Clear();
                ComponenteDA objN = new ComponenteDA();
                List<Componente> iComponentes = new List<Componente>();
                ComponenteDA oComponenteDA = new ComponenteDA();
                iComponentes = oComponenteDA.Cargar(new Componente());
                ViewState["iComponentes"] = iComponentes;

                List<Componente> parents = iComponentes.Where(p => p.intIdComponentePadre == 0).ToList();

                foreach (Componente parentComponent in parents)
                {
                    TreeNode parent = new TreeNode();

                    List<Componente> hijos = iComponentes.Where(p => p.intIdComponentePadre == parentComponent.intIdComponente).ToList();

                    if (oProfile.iComponentes.Where(c => c.intIdComponente == parentComponent.intIdComponente).ToList().Count > 0)
                        parent.Checked = true;

                    parent.ShowCheckBox = true;
                    parent.Value = new JavaScriptSerializer().Serialize(parentComponent);
                    parent.Text = string.Format("<i class=\"now-ui-icons  {0}\"></i> <span oncontextmenu=\"treeViewContextMenu(event, {2});\">{1}</span>", parentComponent.strIconoComponente, parentComponent.strComponente, parentComponent.intIdComponente);
                    if (hijos.Count > 0)
                    {
                        parent.Text = string.Format("<i class=\"fa fa-caret-right\"></i> {0}", parent.Text);
                        foreach (Componente componente in hijos)
                        {
                            parent.ChildNodes.Add(BuscarHijosTreeNode(componente, iComponentes, oProfile));
                        }
                    }

                    tvComponentesPerfil.Nodes.Add(parent);
                }
                tvComponentesPerfil.CollapseAll();
                upTvComponentesPerfil.Update();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private TreeNode BuscarHijosTreeNode(Componente obj, List<Componente> iComponentes, Perfil oProfile)
        {
            TreeNode result = new TreeNode();

            if (oProfile.iComponentes.Where(c => c.intIdComponente == obj.intIdComponente).ToList().Count > 0)
                result.Checked = true;

            result.ShowCheckBox = true;
            result.Value = new JavaScriptSerializer().Serialize(obj);
            result.Text = string.Format("<i class=\"now-ui-icons  {0}\"></i> <span oncontextmenu=\"treeViewContextMenu(event, {2});\">{1}</span>", obj.strIconoComponente, obj.strComponente, obj.intIdComponente);
            try
            {
                List<Componente> iHijos = iComponentes.Where(p => p.intIdComponentePadre == obj.intIdComponente).ToList();
                if (iHijos.Count > 0)//tiene hijos
                {
                    result.Text = string.Format("<i class=\"fa fa-caret-right\"></i> {0}", result.Text);
                    foreach (Componente component in iHijos)
                    {
                        result.ChildNodes.Add(BuscarHijosTreeNode(component, iComponentes, oProfile));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        protected void btnNuevoPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposPerfil();
                CargarTreeViewComponentes(new Perfil(1, string.Empty));
                pnlPerfil.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnGuardarPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposPerfil())
                {
                    int r = 0; 
                    int intId = Convert.ToInt32(HFintIdPerfil.Value);
                    string strProfile = txtPerfil.Text;
                    Perfil obj = new Perfil(intId, strProfile);

                    TreeNodeCollection iTreeComponents = tvComponentesPerfil.CheckedNodes;
                    foreach (TreeNode component in iTreeComponents)
                    {
                        Componente oComponent = new JavaScriptSerializer().Deserialize<Componente>(component.Value);
                        obj.iComponentes.Add(oComponent);
                    }

                    PerfilDA objDA = new PerfilDA();
                    if (obj.intId == 0)
                        r = objDA.Guardar(obj);
                    else
                        r = objDA.Actualizar(obj);
                    
                    if (r > 0)
                    {
                        pnlPerfil.Visible = false;
                        CargarGrillaPerfiles();
                        Funciones.LimpiarCampos(new List<Control>() { txtPerfil });
                        Mensaje("Se registró el Perfil correctamente.", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                    }
                    else
                        Mensaje("No fue posible guardar el Perfil, intente nuevamente", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);

                }
            }
            catch (Exception ex)
            {
                Mensaje("Ocurrió un inconveniente al registrar el perfil", "Problemas!", AlertIcon.Icon.Ban, CssClasses.CssClass.Danger);
            }
        }
        private bool ValidarCamposPerfil()
        {
            bool r = true;
            try
            {
                List<WebControl> iControl = new List<WebControl>() { txtPerfil };
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
        protected void tvComponentesPerfil_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                TreeView _tv = (TreeView)sender;
                TreeNode selectedNode = (TreeNode)_tv.SelectedNode;
                _tv.CollapseAll();
                if (selectedNode.Parent != null)
                    selectedNode.Parent.Expand();
                selectedNode.Expand();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void tvComponentesPerfil_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
        {
            try
            {
                TreeView _tv = (TreeView)sender;
                TreeNode collapsedNode = e.Node;
                collapsedNode.Text = collapsedNode.Text.Replace("caret-down", "caret-right");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void tvComponentesPerfil_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            try
            {
                TreeView _tv = (TreeView)sender;
                TreeNode expandedNode = e.Node;
                expandedNode.Text = expandedNode.Text.Replace("caret-right", "caret-down");
                if (expandedNode.Parent != null)
                    expandedNode.Parent.Expand();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void gvPerfil_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView _gv = (GridView)sender;
            _gv.PageIndex = e.NewPageIndex;
            CargarGrillaPerfiles();
        }
        protected void gvPerfil_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int intId = Convert.ToInt32(e.CommandArgument);
            HFintIdPerfil.Value = Convert.ToString(intId);
            PerfilDA objDA = new PerfilDA();
            switch (e.CommandName)
            {
                case "editItem":
                    Perfil obj = new Perfil();
                    obj.intId = intId;
                    obj = objDA.Cargar(obj).FirstOrDefault();
                    CargarCamposPerfil(obj);
                    pnlPerfil.Visible = true;
                    break;
            }
        }
        private void CargarCamposPerfil(Perfil obj)
        {
            try
            {
                LimpiarCamposPerfil();

                txtPerfil.Text = obj.strDescrip;
                HFintIdPerfil.Value = Convert.ToString(obj.intId);
                CargarTreeViewComponentes(obj);

                string _js = "showPanel('pnlProfileFields', true);";
                Main oMaster = (Main)this.Master;
                oMaster.EjecutarScript(_js, "jsShow");
                //upProfileFields.Update();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #region Componente
        private void CargarComboTipoComponente()
        {
            try
            {
                TipoComponenteDA objDA = new TipoComponenteDA();
                cboTipoComponente.DataSource = objDA.Cargar(new TipoComponente());
                cboTipoComponente.DataTextField = "strDescrip";
                cboTipoComponente.DataValueField = "intId";
                cboTipoComponente.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        private void CargarCamposComponentes(Componente obj)
        {
            try
            {
                txtComponente.Text = obj.strComponente;
                txtComponenteDetalle.Text = obj.strDetalleComponente;
                txtComponenteIcono.Text = obj.strIconoComponente;
                txtComponenteUrl.Text = obj.strUrlComponente;
                chkHabilitar.Checked = obj.bitHabilitarComponente;
                chkMostrar.Checked = obj.bitMostrarComponente;
                cboTipoComponente.SelectedValue = Convert.ToString(obj.oTipoComponente.intId);
                HFintIdComponente.Value = Convert.ToString(obj.intIdComponente);
                HFintIdComponentePadre.Value = Convert.ToString(obj.intIdComponentePadre);
                pnlComponente.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
        private void LimpiarCamposComponente()
        {
            try
            {
                List<Control> iControl = new List<Control>() { txtComponente, txtComponenteDetalle, txtComponenteIcono, txtComponenteUrl, cboTipoComponente, HFintIdComponente, HFintIdComponentePadre };
                Funciones.LimpiarCampos(iControl);
            }
            catch (Exception ex)
            {
            }
        }
        private bool ValidarCamposComponente()
        {
            bool r = true;
            try
            {
                List<WebControl> iControl = new List<WebControl>() { txtComponente, txtComponenteDetalle, cboTipoComponente };
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
        protected void btnNuevoComponente_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposComponente();
                pnlComponente.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnGuardarComponente_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposComponente())
                {
                    Componente obj = new Componente();
                    obj.intIdComponente = Convert.ToInt32(HFintIdComponente.Value);
                    obj.intIdComponentePadre = Convert.ToInt32(HFintIdComponentePadre.Value);
                    obj.oTipoComponente = new TipoComponente(Convert.ToInt32(cboTipoComponente.SelectedValue), Convert.ToString(cboTipoComponente.SelectedItem));
                    obj.strComponente = txtComponente.Text;
                    obj.strDetalleComponente = txtComponenteDetalle.Text;
                    obj.strIconoComponente = txtComponenteIcono.Text;
                    obj.strUrlComponente = txtComponenteUrl.Text;
                    obj.bitHabilitarComponente = chkHabilitar.Checked;
                    obj.bitMostrarComponente = chkMostrar.Checked;

                    ComponenteDA objDA = new ComponenteDA();
                    int r = 0;
                    if (obj.intIdComponente == 0)
                        r = objDA.Guardar(obj);
                    else
                        r = objDA.Actualizar(obj);

                    if (r > 0)
                    {
                        Mensaje("Se guardo el Componente Correcatmente", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                        LimpiarCamposComponente();
                        pnlComponente.Visible = false;
                        CargarTreeViewComponentes();
                    }
                    else
                        Mensaje("Ocurrio un inconveniente al guarar, intente en unos minutos", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void tvComponentes_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                TreeView _tv = (TreeView)sender;
                TreeNode selectedNode = (TreeNode)_tv.SelectedNode;
                _tv.CollapseAll();
                if (selectedNode.Parent != null)
                    selectedNode.Parent.Expand();
                selectedNode.Expand();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnContextEdit_Click(object sender, EventArgs e)
        {
            try
            {
                List<Componente> iComponentes = (List<Componente>)ViewState["iComponentes"];
                Componente obj = iComponentes.Where(c => c.intIdComponente == Convert.ToInt32(HFintIdComponente.Value)).ToList().First();

                CargarCamposComponentes(obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnContextAddChild_Click(object sender, EventArgs e)
        {
            try
            {
                string intIdParent = HFintIdComponente.Value;
                LimpiarCamposComponente();
                HFintIdComponentePadre.Value = intIdParent;
                pnlComponente.Visible = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnContextDelete_Click(object sender, EventArgs e)
        {
            Main oMaster = (Main)this.Master;
            try
            {
                int intId = Convert.ToInt32(HFintIdComponente.Value);
                ComponenteDA objDA = new ComponenteDA();
                Componente obj = new Componente();
                obj.intIdComponente = intId;
                int r = objDA.Eliminar(obj);
                if (r > 0)
                {
                    Mensaje("Se eliminí el Tipod e Documento correctamente", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                    CargarGrillaTipoComponente();
                }
                else
                    Mensaje("No fue posible eliminar el Tipo de Documento indicado", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
            }
            catch (Exception ex)
            {
                Mensaje("Ocurrio un imprevisto, volve a intentar mas tarde", "Error!", AlertIcon.Icon.Ban, CssClasses.CssClass.Danger);
            }
        }
        protected void tvComponentes_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
        {
            try
            {
                TreeView _tv = (TreeView)sender;
                TreeNode collapsedNode = e.Node;
                collapsedNode.Text = collapsedNode.Text.Replace("caret-down", "caret-right");
                foreach (TreeNode item in collapsedNode.ChildNodes)
                {
                    if (Convert.ToBoolean(item.Expanded))
                    {
                        item.Text = item.Text.Replace("caret-right", "caret-down");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void tvComponentes_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            try
            {
                TreeView _tv = (TreeView)sender;
                TreeNode expandedNode = e.Node;
                if (expandedNode.Parent != null)
                    if (!Convert.ToBoolean(expandedNode.Parent.Expanded))
                        expandedNode.Parent.Expand();
                expandedNode.Text = expandedNode.Text.Replace("caret-right", "caret-down");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void CargarTreeViewComponentes()
        {
            try
            {
                tvComponentes.Nodes.Clear();
                ComponenteDA objDA = new ComponenteDA();
                List<Componente> iComponentes = new List<Componente>();
                iComponentes = objDA.Cargar(new Componente());
                ViewState["iComponentes"] = iComponentes;

                List<Componente> parents = iComponentes.Where(p => p.intIdComponentePadre == 0).ToList();

                foreach (Componente parentComponent in parents)
                {
                    List<Componente> hijos = iComponentes.Where(p => p.intIdComponentePadre == parentComponent.intIdComponente).ToList();
                    TreeNode parent = new TreeNode();
                    parent.Value = new JavaScriptSerializer().Serialize(parentComponent);
                    parent.Text = string.Format("<i class=\"now-ui-icons  {0}\"></i> <span oncontextmenu=\"treeViewContextMenu(event, {2});\">{1}</span>", parentComponent.strIconoComponente, parentComponent.strComponente, parentComponent.intIdComponente);
                    if (hijos.Count > 0)//tiene hijosS
                    {
                        parent.Text = string.Format("<i class=\"fas fa-caret-right\"></i> {0}", parent.Text);
                        foreach (Componente componente in hijos)
                        {
                            parent.ChildNodes.Add(BuscarHijosTreeNode(componente, iComponentes, string.Empty, 0));
                        }
                    }

                    tvComponentes.Nodes.Add(parent);
                }
                tvComponentes.CollapseAll();
                upTvComponentes.Update();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private TreeNode BuscarHijosTreeNode(Componente obj, List<Componente> iComponents, string strParentAccordion = "accComponents", int intMargin = -5)
        {
            TreeNode result = new TreeNode();
            result.Value = new JavaScriptSerializer().Serialize(obj);
            result.Text = string.Format("<i class=\"now-ui-icons  {0}\"></i> <span oncontextmenu=\"treeViewContextMenu(event, {2});\">{1}</span>", obj.strIconoComponente, obj.strComponente, obj.intIdComponente);
            try
            {
                List<Componente> iHijos = iComponents.Where(p => p.intIdComponentePadre == obj.intIdComponente).ToList();
                if (iHijos.Count > 0)//tiene hijos
                {
                    result.Text = string.Format("<i class=\"fas fa-caret-right\"></i> {0}", result.Text);
                    foreach (Componente component in iHijos)
                    {
                        result.ChildNodes.Add(BuscarHijosTreeNode(component, iComponents, "collapse" + obj.intIdComponente, (intMargin + 8)));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        #endregion
        #region TipoDocumento
        private void CargarGrillaTipoDocumento()
        {
            try
            {
                TipoDocumentoDA objDa = new TipoDocumentoDA();
                gvTipoDocumento.DataSource = objDa.Cargar(new TipoDocumento());
                gvTipoDocumento.DataBind();
                upGridTipoDocumento.Update();
            }
            catch (Exception ex)
            {
            }
        }
        private void CargarCamposTipoDocumento(TipoDocumento obj)
        {
            try
            {
                txtTipoDocumento.Text = obj.strDescrip;
                HFintIdTipoDocumento.Value = Convert.ToString(obj.intId);
                pnlTipoDocumento.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
        private void LimpiarCamposTipoDocumento()
        {
            try
            {
                List<Control> iControl = new List<Control>() { txtTipoDocumento, HFintIdTipoDocumento };
                Funciones.LimpiarCampos(iControl);
            }
            catch (Exception ex)
            {
            }
        }
        private bool ValidarCamposTipoDocumento()
        {
            bool r = true;
            try
            {
                List<WebControl> iControl = new List<WebControl>() { txtTipoDocumento };
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
        protected void gvTipoDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView _gv = (GridView)sender;
            _gv.PageIndex = e.NewPageIndex;
            CargarGrillaTipoDocumento();
        }
        protected void gvTipoDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intId = Convert.ToInt32(e.CommandArgument);
                HFintIdTipoDocumento.Value = Convert.ToString(intId);
                TipoDocumentoDA objDA = new TipoDocumentoDA();
                switch (e.CommandName)
                {
                    case "editItem":
                        TipoDocumento obj = objDA.Cargar(new TipoDocumento(intId, string.Empty)).FirstOrDefault();
                        CargarCamposTipoDocumento(obj);
                        break;
                    case "deleteItem":
                        int r = objDA.Eliminar(new TipoDocumento(intId, string.Empty));
                        if (r > 0)
                        {
                            Mensaje("Se eliminí el Tipod e Documento correctamente", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                            CargarGrillaTipoDocumento();
                        }
                        else
                            Mensaje("No fue posible eliminar el Tipo de Documento indicado", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnGuardarTipoDocumento_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposTipoDocumento())
                {
                    TipoDocumento obj = new TipoDocumento();
                    obj.intId = Convert.ToInt32(HFintIdTipoDocumento.Value);
                    obj.strDescrip = txtTipoDocumento.Text;

                    TipoDocumentoDA objDA = new TipoDocumentoDA();
                    int r = 0;
                    if(obj.intId == 0)
                        r = objDA.Guardar(obj);
                    else
                        r = objDA.Actualizar(obj);

                    if (r > 0)
                    {
                        Mensaje("Se guardo el Tipo de Documento Correcatmente", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                        LimpiarCamposTipoDocumento();
                        pnlTipoDocumento.Visible = false;
                        CargarGrillaTipoDocumento();
                    }
                    else
                        Mensaje("Ocurrio un inconveniente al guarar, intente en unos minutos", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Ocurrio un imprevisto, volve a intentar mas tarde", "Error!", AlertIcon.Icon.Ban, CssClasses.CssClass.Danger);
            }
        }
        protected void btnNuevoTipoDocumento_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposTipoDocumento();
                pnlTipoDocumento.Visible = true;
            }
            catch (Exception ex)
            {
                Mensaje("Ocurrio un imprevisto, volve a intentar mas tarde", "Error!", AlertIcon.Icon.Ban, CssClasses.CssClass.Danger);
            }
        }
        #endregion
        #region TipoComponente
        private void CargarGrillaTipoComponente()
        {
            try
            {
                TipoComponenteDA objDa = new TipoComponenteDA();
                gvTipoComponente.DataSource = objDa.Cargar(new TipoComponente());
                gvTipoComponente.DataBind();
                upGridTipoComponente.Update();
            }
            catch (Exception ex)
            {
            }
        }
        private void CargarCamposTipoComponente(TipoComponente obj)
        {
            try
            {
                txtTipoComponente.Text = obj.strDescrip;
                HFintIdTipoComponente.Value = Convert.ToString(obj.intId);
                pnlTipoComponente.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
        private void LimpiarCamposTipoComponente()
        {
            try
            {
                List<Control> iControl = new List<Control>() { txtTipoComponente, HFintIdTipoComponente };
                Funciones.LimpiarCampos(iControl);
            }
            catch (Exception ex)
            {
            }
        }
        private bool ValidarCamposTipoComponente()
        {
            bool r = true;
            try
            {
                List<WebControl> iControl = new List<WebControl>() { txtTipoComponente };
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
        protected void gvTipoComponente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView _gv = (GridView)sender;
            _gv.PageIndex = e.NewPageIndex;
            CargarGrillaTipoComponente();
        }
        protected void gvTipoComponente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intId = Convert.ToInt32(e.CommandArgument);
                HFintIdTipoComponente.Value = Convert.ToString(intId);
                TipoComponenteDA objDA = new TipoComponenteDA();
                switch (e.CommandName)
                {
                    case "editItem":
                        TipoComponente obj = objDA.Cargar(new TipoComponente(intId, string.Empty)).FirstOrDefault();
                        CargarCamposTipoComponente(obj);
                        break;
                    case "deleteItem":
                        int r = objDA.Eliminar(new TipoComponente(intId, string.Empty));
                        if (r > 0)
                        {
                            Mensaje("Se eliminí el Tipod e Documento correctamente", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                            CargarGrillaTipoComponente();
                        }
                        else
                            Mensaje("No fue posible eliminar el Tipo de Documento indicado", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnGuardarTipoComponente_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposTipoComponente())
                {
                    TipoComponente obj = new TipoComponente();
                    obj.intId = Convert.ToInt32(HFintIdTipoComponente.Value);
                    obj.strDescrip = txtTipoComponente.Text;

                    TipoComponenteDA objDA = new TipoComponenteDA();
                    int r = 0;
                    if (obj.intId == 0)
                        r = objDA.Guardar(obj);
                    else
                        r = objDA.Actualizar(obj);

                    if (r > 0)
                    {
                        Mensaje("Se guardo el Tipo de Documento Correcatmente", "Correcto!", AlertIcon.Icon.Check, CssClasses.CssClass.Success);
                        LimpiarCamposTipoComponente();
                        pnlTipoComponente.Visible = false;
                        CargarGrillaTipoComponente();
                    }
                    else
                        Mensaje("Ocurrio un inconveniente al guarar, intente en unos minutos", "Problemas!", AlertIcon.Icon.ExclamationTriangle, CssClasses.CssClass.Warning);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Ocurrio un imprevisto, volve a intentar mas tarde", "Error!", AlertIcon.Icon.Ban, CssClasses.CssClass.Danger);
            }
        }
        protected void btnNuevoTipoComponente_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposTipoComponente();
                pnlTipoComponente.Visible = true;
            }
            catch (Exception ex)
            {
                Mensaje("Ocurrio un imprevisto, volve a intentar mas tarde", "Error!", AlertIcon.Icon.Ban, CssClasses.CssClass.Danger);
            }
        }
        #endregion



    }
}