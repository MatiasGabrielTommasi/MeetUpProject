<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Parametricas.aspx.cs" Inherits="MeetUp.Parametricas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="HFintIdPerfil" Value="0" />
            <asp:HiddenField runat="server" ID="HFintIdComponente" Value="0" />
            <asp:HiddenField runat="server" ID="HFintIdComponentePadre" Value="0" />
            <asp:HiddenField runat="server" ID="HFintIdTipoComponente" Value="0" />
            <asp:HiddenField runat="server" ID="HFintIdTipoDocumento" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="main-panel" id="main-panel">
      <!-- Navbar -->
      <nav class="navbar navbar-expand-lg navbar-transparent  bg-primary  navbar-absolute">
        <div class="container-fluid">
          <div class="navbar-wrapper">
            <div class="navbar-toggle">
              <button type="button" class="navbar-toggler">
                <span class="navbar-toggler-bar bar1"></span>
                <span class="navbar-toggler-bar bar2"></span>
                <span class="navbar-toggler-bar bar3"></span>
              </button>
            </div>
            <a class="navbar-brand" href="#pablo">Paramétricas</a>
          </div>
          <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigation" aria-controls="navigation-index" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-bar navbar-kebab"></span>
            <span class="navbar-toggler-bar navbar-kebab"></span>
            <span class="navbar-toggler-bar navbar-kebab"></span>
          </button>
        </div>
      </nav>
      <!-- End Navbar -->
      <div class="panel-header panel-header-lg"></div>
      <div class="content">
        <div class="row">

            <!-- PERFILES -->
          <div class="col-12">
            <div class="card card-chart">
              <div class="card-header">
                <h5 class="card-category">Configuraciones</h5>
                <h4 class="card-title">Perfil</h4>
                  <asp:UpdatePanel runat="server" class="float-right new-button p-0">
                      <ContentTemplate>
                          <asp:LinkButton runat="server" ID="btnNuevoPerfil" OnClick="btnNuevoPerfil_Click" CssClass="btn btn-round btn-outline-info btn-simple no-caret m-0">
                            <i class="now-ui-icons design-2_ruler-pencil"></i> Nuevo
                          </asp:LinkButton>
                      </ContentTemplate>
                  </asp:UpdatePanel>
              </div>
              <div class="card-body">
                  <div class="row">
                      <asp:UpdatePanel class="col-12 col-md-6" runat="server" ID="upGridPerfil" UpdateMode="Conditional">
                          <ContentTemplate>
                              <asp:GridView CssClass="table table-hover" ID="gvPerfil" runat="server" AllowPaging="true" AutoGenerateColumns="false" EmptyDataText="No Profile found." GridLines="None"
                                   OnPageIndexChanging="gvPerfil_PageIndexChanging" OnRowCommand="gvPerfil_RowCommand" PageSize="10" ShowHeader="false">
                                  <Columns>
                                      <asp:BoundField HeaderText="Description" DataField="strDescrip" />
                                      <asp:TemplateField ItemStyle-Width="100">
                                          <ItemTemplate>    
                                              <asp:LinkButton ID="btnEditar" CommandName="editItem" CommandArgument='<%# Eval("intId") %>' runat="server" CssClass="btn btn-warning btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons design-2_ruler-pencil"></i></asp:LinkButton>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                  </Columns>
                                <pagerstyle horizontalalign="Right" cssclass="pagination-ys"/>
                              </asp:GridView>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                      <asp:UpdatePanel class="col-12 col-md-6" runat="server" UpdateMode="Always" >
                          <ContentTemplate>
                              <asp:Panel runat="server" ID="pnlPerfil" Visible="false">
                                  <div class="form-group col-12"> 
                                      Nombre del Perfil
                                      <asp:TextBox runat="server" ID="txtPerfil" data-name="Nombre del Perfil" CssClass="form-control" placeholder="Perfil" />
                                  </div>
                                  <asp:UpdatePanel class="col-12" style="overflow-y:scroll;max-height:300px!important;" runat="server" ID="upTvComponentesPerfil" UpdateMode="Conditional">
                                      <ContentTemplate>
                                          <asp:TreeView ID="tvComponentesPerfil" OnSelectedNodeChanged="tvComponentesPerfil_SelectedNodeChanged" OnTreeNodeExpanded="tvComponentesPerfil_TreeNodeExpanded" OnTreeNodeCollapsed="tvComponentesPerfil_TreeNodeCollapsed" CssClass="tree-view-asp" runat="server"></asp:TreeView>
                                      </ContentTemplate>
                                  </asp:UpdatePanel>
                                  <asp:LinkButton runat="server" ID="btnGuardarPerfil" OnClick="btnGuardarPerfil_Click" CssClass="btn btn-round btn-outline-success float-right no-caret">
                                    <i class="now-ui-icons design-2_ruler-pencil"></i> Guardar
                                  </asp:LinkButton>
                              </asp:Panel>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
            </div>
          </div>
        </div>
            <!-- PERFILES FIN -->
            
            <!-- COMPONENTES -->
          <div class="col-12 col-md-6 col-lg-6 col-xl-4">
            <div class="card card-chart">
              <div class="card-header">
                <h5 class="card-category">Configuraciones</h5>
                <h4 class="card-title">Componente</h4>
                  <asp:UpdatePanel runat="server" class="float-right new-button">
                      <ContentTemplate>
                          <asp:LinkButton runat="server" ID="btnNuevoComponente" OnClick="btnNuevoComponente_Click" CssClass="btn btn-round btn-outline-info btn-simple no-caret m-0">
                            <i class="now-ui-icons design-2_ruler-pencil"></i> Nuevo
                          </asp:LinkButton>
                      </ContentTemplate>
                  </asp:UpdatePanel>
              </div>
              <div class="card-body">
                  <div class="row">
                      <asp:UpdatePanel class="col-12" runat="server" UpdateMode="Always" >
                          <ContentTemplate>
                              <asp:Panel runat="server" ID="pnlComponente" Visible="false">
                                  <div class="form-group col-12 "> 
                                      Nombre del Componente
                                      <asp:TextBox runat="server" data-name="Nombre del Componente" ID="txtComponente" CssClass="form-control" placeholder="Tipo de Componente" />
                                      Breve Descripción
                                      <asp:TextBox runat="server" data-name="Descripción del Componente" ID="txtComponenteDetalle" CssClass="form-control" placeholder="Tipo de Componente" />
                                      URL del Componente
                                      <asp:TextBox runat="server" data-name="URL del Componente" ID="txtComponenteUrl" CssClass="form-control" placeholder="Tipo de Componente" />
                                      Icono
                                      <asp:TextBox runat="server" data-name="Icono del Componente" ID="txtComponenteIcono" CssClass="form-control" placeholder="Icono de Componente" />
                                      Tipo de Componente
                                      <asp:DropDownList runat="server" data-name="Tipo de Componente" ID="cboTipoComponente" CssClass="form-control" />
                                      <div class="col-12">
                                            <asp:CheckBox runat="server" CssClass="span-checkbox" ID="chkHabilitar" />
                                            <asp:CheckBox runat="server" CssClass="span-checkbox" ID="chkMostrar" />
                                      </div>
                                  </div>
                                  <asp:LinkButton runat="server" ID="btnGuardarComponente" OnClick="btnGuardarComponente_Click" CssClass="btn btn-round btn-outline-success float-right no-caret">
                                    <i class="now-ui-icons design-2_ruler-pencil"></i> Guardar
                                  </asp:LinkButton>
                              </asp:Panel>
                          </ContentTemplate>
                      </asp:UpdatePanel>
        <!-- TreeView Context Menu -->
                      <asp:UpdatePanel class="col-12 table-responsive" runat="server" ID="upTvComponentes" UpdateMode="Conditional">
                          <ContentTemplate>

                              <asp:TreeView ID="tvComponentes" OnSelectedNodeChanged="tvComponentes_SelectedNodeChanged" OnTreeNodeCollapsed="tvComponentes_TreeNodeCollapsed" OnTreeNodeExpanded="tvComponentes_TreeNodeExpanded" CssClass="tree-view-asp" runat="server"></asp:TreeView>
                              
                          </ContentTemplate>
                      </asp:UpdatePanel>
                        <asp:UpdatePanel runat="server" UpdateMode="Always" Visible="true">
                            <ContentTemplate>
                                <div class="dropdown-menu dropdown-menu-sm" id="tree-view-context-menu">
                                  <center style="margin-left:5px;margin-right:5px;"><label id="context-menu-title"></label></center>
                                    <asp:LinkButton runat="server" ID="btnContextEdit" OnClick="btnContextEdit_Click" CssClass="dropdown-item" style="cursor:pointer;" oncontextmenu="event.preventDefault();" data-asd="treeViewContextMenuItemSelected(event);"><i class="fas fa-pencil-alt"></i> Edit</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnContextAddChild" OnClick="btnContextAddChild_Click" CssClass="dropdown-item" style="cursor:pointer;" oncontextmenu="event.preventDefault();" data-asd="treeViewContextMenuItemSelected(event);"><i class="fas fa-plus"></i> Add Child Component</asp:LinkButton>
                                    <div class="dropdown-divider"></div>
                                    <asp:LinkButton runat="server" ID="btnContextDelete" OnClick="btnContextDelete_Click" CssClass="dropdown-item text-danger" style="cursor:pointer;" oncontextmenu="event.preventDefault();" data-asd="treeViewContextMenuItemSelected(event);"><i class="fas fa-trash"></i> Delete</asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                  </div>
            </div>
          </div>
        </div>
            <!-- COMPONENTES FIN -->
            
            <!-- TIPO COMPONENTE -->
          <div class="col-12 col-md-6 col-lg-6 col-xl-4">
            <div class="card card-chart">
              <div class="card-header">
                <h5 class="card-category">Configuraciones</h5>
                <h4 class="card-title">Tipo Componente</h4>
                  <asp:UpdatePanel runat="server" class="float-right new-button">
                      <ContentTemplate>
                          <asp:LinkButton runat="server" ID="btnNuevoTipoComponente" OnClick="btnNuevoTipoComponente_Click" CssClass="btn btn-round btn-outline-info btn-simple no-caret m-0">
                            <i class="now-ui-icons design-2_ruler-pencil"></i> Nuevo
                          </asp:LinkButton>
                      </ContentTemplate>
                  </asp:UpdatePanel>
              </div>
              <div class="card-body">
                  <div class="row">
                      <asp:UpdatePanel class="col-12" runat="server" UpdateMode="Always" >
                          <ContentTemplate>
                              <asp:Panel runat="server" ID="pnlTipoComponente" Visible="false">
                                  <div class="form-group col-12 "> 
                                      Nombre del Tipo de Componente
                                      <asp:TextBox runat="server" ID="txtTipoComponente" data-name="Tipo de Componente" CssClass="form-control" placeholder="Tipo de Componente" />
                                  </div>
                                  <asp:LinkButton runat="server" ID="btnGuardarTipoComponente" OnClick="btnGuardarTipoComponente_Click" CssClass="btn btn-round btn-outline-success float-right no-caret">
                                    <i class="now-ui-icons design-2_ruler-pencil"></i> Guardar
                                  </asp:LinkButton>
                              </asp:Panel>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                      <asp:UpdatePanel class="col-12 table-responsive" runat="server" ID="upGridTipoComponente" UpdateMode="Conditional">
                          <ContentTemplate>
                              <asp:GridView CssClass="table" ID="gvTipoComponente" runat="server" AllowPaging="true" AutoGenerateColumns="false" EmptyDataText="No Profile found." GridLines="None"
                                   OnPageIndexChanging="gvTipoComponente_PageIndexChanging" OnRowCommand="gvTipoComponente_RowCommand" PageSize="5"
                                  ShowHeader="false" >
                                  <Columns>
                                      <asp:BoundField HeaderText="Description" DataField="strDescrip" />
                                      <asp:TemplateField ItemStyle-Width="100">
                                          <ItemTemplate>    
                                              <asp:LinkButton ID="btnEditar" CommandName="editItem" CommandArgument='<%# Eval("intId") %>' runat="server" CssClass="btn btn-warning btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons design-2_ruler-pencil"></i></asp:LinkButton>
                                              <asp:LinkButton ID="btnEliminar" CommandName="deleteItem" CommandArgument='<%# Eval("intId") %>' runat="server" CssClass="btn btn-danger btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons ui-1_simple-remove"></i></asp:LinkButton>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                  </Columns>
                                <pagerstyle horizontalalign="Right" cssclass="pagination-ys"/>
                              </asp:GridView>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
            </div>
          </div>
        </div>
            <!-- TIPO COMPONENTE FIN -->
            
            <!-- TIPO DOCUMENTO -->
          <div class="col-12 col-md-6 col-lg-6 col-xl-4">
            <div class="card card-chart">
              <div class="card-header">
                <h5 class="card-category">Configuraciones</h5>
                <h4 class="card-title">Tipo Documento</h4>
                  <asp:UpdatePanel runat="server" class="float-right new-button">
                      <ContentTemplate>
                          <asp:LinkButton runat="server" ID="btnNuevoTipoDocumento" OnClick="btnNuevoTipoDocumento_Click" CssClass="btn btn-round btn-outline-info btn-simple no-caret m-0">
                            <i class="now-ui-icons design-2_ruler-pencil"></i> Nuevo
                          </asp:LinkButton>
                      </ContentTemplate>
                  </asp:UpdatePanel>
              </div>
              <div class="card-body">
                  <div class="row">
                      <asp:UpdatePanel class="col-12" runat="server" UpdateMode="Always" >
                          <ContentTemplate>
                              <asp:Panel runat="server" ID="pnlTipoDocumento" Visible="false">
                                  <div class="form-group col-12 "> 
                                      Nombre del Tipo de Documento
                                      <asp:TextBox runat="server" ID="txtTipoDocumento" data-name="Tipo de Documento" CssClass="form-control" placeholder="Tipo de Documento" />
                                  </div>
                                  <asp:LinkButton runat="server" ID="btnGuardarTipoDocumento" OnClick="btnGuardarTipoDocumento_Click" CssClass="btn btn-round btn-outline-success float-right no-caret">
                                    <i class="now-ui-icons design-2_ruler-pencil"></i> Guardar
                                  </asp:LinkButton>
                              </asp:Panel>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                      <asp:UpdatePanel class="col-12 table-responsive" runat="server" ID="upGridTipoDocumento" UpdateMode="Conditional">
                          <ContentTemplate>
                              <asp:GridView CssClass="table" ID="gvTipoDocumento" runat="server" AllowPaging="true" AutoGenerateColumns="false" EmptyDataText="No Profile found." GridLines="None"
                                   OnPageIndexChanging="gvTipoDocumento_PageIndexChanging" OnRowCommand="gvTipoDocumento_RowCommand" PageSize="5"
                                  ShowHeader="false" >
                                  <Columns>
                                      <asp:BoundField HeaderText="Description" DataField="strDescrip" />
                                      <asp:TemplateField ItemStyle-Width="100">
                                          <ItemTemplate>    
                                              <asp:LinkButton ID="btnEditar" CommandName="editItem" CommandArgument='<%# Eval("intId") %>' runat="server" CssClass="btn btn-warning btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons design-2_ruler-pencil"></i></asp:LinkButton>
                                              <asp:LinkButton ID="btnEliminar" CommandName="deleteItem" CommandArgument='<%# Eval("intId") %>' runat="server" CssClass="btn btn-danger btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons ui-1_simple-remove"></i></asp:LinkButton>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                  </Columns>
                                <pagerstyle horizontalalign="Right" cssclass="pagination-ys"/>
                              </asp:GridView>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
            </div>
          </div>
        </div>
            <!-- TIPO DOCUMENTO FIN -->
            
      </div>
      <footer class="footer">
        <div class=" container-fluid ">
          <nav>
            <ul>
              <li>
                <a href="https://www.creative-tim.com">
                  Creative Tim
                </a>
              </li>
              <li>
                <a href="http://presentation.creative-tim.com">
                  About Us
                </a>
              </li>
              <li>
                <a href="http://blog.creative-tim.com">
                  Blog
                </a>
              </li>
            </ul>
          </nav>
          <div class="copyright" id="copyright">
            &copy; <script>
              document.getElementById('copyright').appendChild(document.createTextNode(new Date().getFullYear()))
            </script>, Designed by <a href="https://www.invisionapp.com" target="_blank">Invision</a>. Coded by <a href="https://www.creative-tim.com" target="_blank">Creative Tim</a>.
          </div>
        </div>
      </footer>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
    <script>
        function bindScripts() {
            var lista = $("input[type=checkbox]");
            for (var i = 0; i < lista.length; i++) {
                var item = $(lista[i])[0];
                var div = $("<div class='custom-control custom-switch custom-switch-off-danger custom-switch-on-success float-right'></div>")
                $(item).wrap(div).addClass('custom-control-input');
                var name = item.id.split('_')[1];
                if (name == "chkMostrar" || name == "chkHabilitar") {
                    $(item).after('<label class="custom-control-label text-dark" for="' + item.id + '">' + name.replace('chk', '') + '</label>');
                } else {
                    $(item).after('<label class="custom-control-label text-white" for="' + item.id + '">.</label>');
                }
            }

            $(".span-checkbox").addClass('custom-control custom-switch custom-switch-off-danger custom-switch-on-success')
            $(".span-checkbox").children('input').addClass('custom-control-input');
            $(".span-checkbox").children('label').addClass('custom-control-label');
        }
        bindScripts();

        function treeViewContextMenu(event, id) {
            event.preventDefault();
            $("#<%= HFintIdComponente.ClientID %>").val(id);
            $("#context-menu-title").html(event.target.innerHTML);


            var eventDoc = (event.target && event.target.ownerDocument) || document;
            doc = eventDoc.documentElement;
            body = eventDoc.body;

            var offset = $(event.target).offset();
            var width = (window.innerWidth >= 1025 ) ? 260 : 0;
            var left = event.x - width;
            var top = event.screenY - offset.top + 20;

            $("#tree-view-context-menu").css({
                display: "block",
                top: top,
                left: left
            }).addClass("show");
            return false;
        }

        function treeViewContextMenuItemSelected(event) {
            event.preventDefault();
            $("#tree-view-context-menu a").parent().removeClass("show").hide();
        }

        $(document).click(function (e) {
            $("#tree-view-context-menu").hide();
        })
    </script>
</asp:Content>
