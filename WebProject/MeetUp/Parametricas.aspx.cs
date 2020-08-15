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
            Usuario objUser = (Usuario)Session["objUser"];
            List<Componente> iComponentes = objUser.Perfiles.SelectMany(p => p.Componentes).Distinct().ToList();
            List<WebControl> iControls = new List<WebControl>() { pnlComponentes, pnlPerfiles, pnlTiposComponentes, pnlTiposDocumentos };
            foreach (WebControl control in iControls)
            {
                control.Visible = (iComponentes.Where(c => c.Detalle == control.ID).ToList().Count > 0);
            }
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
                List<Control> iControl = new List<Control>() { txtPerfil, HFIdPerfil };
                Funciones.LimpiarCampos(iControl);
                btnGuardarPerfil.Visible = true;
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

                List<Componente> parents = iComponentes.Where(p => p.IdPadre == 0).ToList();

                foreach (Componente parentComponent in parents)
                {
                    TreeNode parent = new TreeNode();

                    List<Componente> hijos = iComponentes.Where(p => p.IdPadre == parentComponent.Id).ToList();

                    if (oProfile.Componentes.Where(c => c.Id == parentComponent.Id).ToList().Count > 0)
                        parent.Checked = true;

                    parent.ShowCheckBox = true;
                    parent.Value = new JavaScriptSerializer().Serialize(parentComponent);
                    parent.Text = string.Format("<i class=\"now-ui-icons  {0}\"></i> <span oncontextmenu=\"treeViewContextMenu(event, {2});\">{1}</span>", parentComponent.Icono, parentComponent.Nombre, parentComponent.Id);
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

            if (oProfile.Componentes.Where(c => c.Id == obj.Id).ToList().Count > 0)
                result.Checked = true;

            result.ShowCheckBox = true;
            result.Value = new JavaScriptSerializer().Serialize(obj);
            result.Text = string.Format("<i class=\"now-ui-icons  {0}\"></i> <span oncontextmenu=\"treeViewContextMenu(event, {2});\">{1}</span>", obj.Icono, obj.Nombre, obj.Id);
            try
            {
                List<Componente> iHijos = iComponentes.Where(p => p.IdPadre == obj.Id).ToList();
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
                    int Id = Convert.ToInt32(HFIdPerfil.Value);
                    string strProfile = txtPerfil.Text;
                    Perfil obj = new Perfil(Id, strProfile);

                    TreeNodeCollection iTreeComponents = tvComponentesPerfil.CheckedNodes;
                    foreach (TreeNode component in iTreeComponents)
                    {
                        Componente oComponent = new JavaScriptSerializer().Deserialize<Componente>(component.Value);
                        obj.Componentes.Add(oComponent);
                    }

                    PerfilDA objDA = new PerfilDA();
                    if (obj.Id == 0)
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
            int Id = Convert.ToInt32(e.CommandArgument);
            HFIdPerfil.Value = Convert.ToString(Id);
            PerfilDA objDA = new PerfilDA();
            switch (e.CommandName)
            {
                case "editItem":
                    Perfil obj = new Perfil();
                    obj.Id = Id;
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

                txtPerfil.Text = obj.Descrip;
                HFIdPerfil.Value = Convert.ToString(obj.Id);
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
                cboTipoComponente.DataTextField = "Descrip";
                cboTipoComponente.DataValueField = "Id";
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
                txtComponente.Text = obj.Nombre;
                txtComponenteDetalle.Text = obj.Detalle;
                txtComponenteIcono.Text = obj.Icono;
                txtComponenteUrl.Text = obj.Url;
                chkHabilitar.Checked = obj.Habilitado;
                chkMostrar.Checked = obj.Mostrado;
                cboTipoComponente.SelectedValue = Convert.ToString(obj.Tipo.Id);
                HFIdComponente.Value = Convert.ToString(obj.Id);
                HFIdComponentePadre.Value = Convert.ToString(obj.IdPadre);
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
                List<Control> iControl = new List<Control>() { txtComponente, txtComponenteDetalle, txtComponenteIcono, txtComponenteUrl, cboTipoComponente, HFIdComponente, HFIdComponentePadre };
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
                    obj.Id = Convert.ToInt32(HFIdComponente.Value);
                    obj.IdPadre = Convert.ToInt32(HFIdComponentePadre.Value);
                    obj.Tipo = new TipoComponente(Convert.ToInt32(cboTipoComponente.SelectedValue), Convert.ToString(cboTipoComponente.SelectedItem));
                    obj.Nombre = txtComponente.Text;
                    obj.Detalle = txtComponenteDetalle.Text;
                    obj.Icono = txtComponenteIcono.Text;
                    obj.Url = txtComponenteUrl.Text;
                    obj.Habilitado = chkHabilitar.Checked;
                    obj.Mostrado = chkMostrar.Checked;

                    ComponenteDA objDA = new ComponenteDA();
                    int r = 0;
                    if (obj.Id == 0)
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
                Componente obj = iComponentes.Where(c => c.Id == Convert.ToInt32(HFIdComponente.Value)).ToList().First();

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
                string IdParent = HFIdComponente.Value;
                LimpiarCamposComponente();
                HFIdComponentePadre.Value = IdParent;
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
                int Id = Convert.ToInt32(HFIdComponente.Value);
                ComponenteDA objDA = new ComponenteDA();
                Componente obj = new Componente();
                obj.Id = Id;
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

                List<Componente> parents = iComponentes.Where(p => p.IdPadre == 0).ToList();

                foreach (Componente parentComponent in parents)
                {
                    List<Componente> hijos = iComponentes.Where(p => p.IdPadre == parentComponent.Id).ToList();
                    TreeNode parent = new TreeNode();
                    parent.Value = new JavaScriptSerializer().Serialize(parentComponent);
                    parent.Text = string.Format("<i class=\"now-ui-icons  {0}\"></i> <span oncontextmenu=\"treeViewContextMenu(event, {2});\">{1}</span>", parentComponent.Icono, parentComponent.Nombre, parentComponent.Id);
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
            result.Text = string.Format("<i class=\"now-ui-icons  {0}\"></i> <span oncontextmenu=\"treeViewContextMenu(event, {2});\">{1}</span>", obj.Icono, obj.Nombre, obj.Id);
            try
            {
                List<Componente> iHijos = iComponents.Where(p => p.IdPadre == obj.Id).ToList();
                if (iHijos.Count > 0)//tiene hijos
                {
                    result.Text = string.Format("<i class=\"fas fa-caret-right\"></i> {0}", result.Text);
                    foreach (Componente component in iHijos)
                    {
                        result.ChildNodes.Add(BuscarHijosTreeNode(component, iComponents, "collapse" + obj.Id, (intMargin + 8)));
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
                txtTipoDocumento.Text = obj.Descrip;
                HFIdTipoDocumento.Value = Convert.ToString(obj.Id);
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
                List<Control> iControl = new List<Control>() { txtTipoDocumento, HFIdTipoDocumento };
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
                int Id = Convert.ToInt32(e.CommandArgument);
                HFIdTipoDocumento.Value = Convert.ToString(Id);
                TipoDocumentoDA objDA = new TipoDocumentoDA();
                switch (e.CommandName)
                {
                    case "editItem":
                        TipoDocumento obj = objDA.Cargar(new TipoDocumento(Id, string.Empty)).FirstOrDefault();
                        CargarCamposTipoDocumento(obj);
                        break;
                    case "deleteItem":
                        int r = objDA.Eliminar(new TipoDocumento(Id, string.Empty));
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
                    obj.Id = Convert.ToInt32(HFIdTipoDocumento.Value);
                    obj.Descrip = txtTipoDocumento.Text;

                    TipoDocumentoDA objDA = new TipoDocumentoDA();
                    int r = 0;
                    if(obj.Id == 0)
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
                txtTipoComponente.Text = obj.Descrip;
                HFIdTipoComponente.Value = Convert.ToString(obj.Id);
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
                List<Control> iControl = new List<Control>() { txtTipoComponente, HFIdTipoComponente };
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
                int Id = Convert.ToInt32(e.CommandArgument);
                HFIdTipoComponente.Value = Convert.ToString(Id);
                TipoComponenteDA objDA = new TipoComponenteDA();
                switch (e.CommandName)
                {
                    case "editItem":
                        TipoComponente obj = objDA.Cargar(new TipoComponente(Id, string.Empty)).FirstOrDefault();
                        CargarCamposTipoComponente(obj);
                        break;
                    case "deleteItem":
                        int r = objDA.Eliminar(new TipoComponente(Id, string.Empty));
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
                    obj.Id = Convert.ToInt32(HFIdTipoComponente.Value);
                    obj.Descrip = txtTipoComponente.Text;

                    TipoComponenteDA objDA = new TipoComponenteDA();
                    int r = 0;
                    if (obj.Id == 0)
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