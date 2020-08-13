<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="MeetUp.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="HFintIdUsuario" Value="0" />
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
            <a class="navbar-brand">Usuarios</a>
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
            
            <!-- USUARIOS -->
          <div class="col-12">
            <div class="card card-chart">
              <div class="card-header">
                <h5 class="card-category">Configuraciones</h5>
                <h4 class="card-title">Usuarios</h4>
                  <asp:UpdatePanel runat="server" class="float-right new-button p-0">
                      <ContentTemplate>
                          <asp:LinkButton runat="server" ID="btnNuevoUsuario" OnClick="btnNuevoUsuario_Click" CssClass="btn btn-round btn-outline-info btn-simple no-caret m-0">
                            <i class="now-ui-icons design-2_ruler-pencil"></i> Nuevo
                          </asp:LinkButton>
                      </ContentTemplate>
                  </asp:UpdatePanel>
              </div>
              <div class="card-body">
                  <div class="row">
                      <asp:UpdatePanel class="col-12" runat="server" UpdateMode="Always" >
                          <ContentTemplate>
                              <asp:Panel runat="server" CssClass="row" ID="pnlUsuario" Visible="false">
                                  <div class="col-12 col-md-9 row"> 
                                      <div class="form-group col-12 col-md-4"> 
                                          Nombre
                                          <asp:TextBox runat="server" ID="txtNombre" data-name="Nombre del Usuario" CssClass="form-control" placeholder="Nombre" />
                                      </div>
                                      <div class="form-group col-12 col-md-4"> 
                                          Apellido
                                          <asp:TextBox runat="server" ID="txtApellido" data-name="Nombre del Usuario" CssClass="form-control" placeholder="apellido" />
                                      </div>
                                      <div class="form-group col-12 col-md-4"> 
                                          Correo
                                          <asp:TextBox runat="server" ID="txtCorreo" data-name="Nombre del Usuario" CssClass="form-control" placeholder="Correo" />
                                      </div>
                                      <div class="form-group col-12 col-md-4"> 
                                          Nombre de Usuario
                                          <asp:TextBox runat="server" ID="txtUsuario" data-name="Nombre del Usuario" CssClass="form-control" placeholder="Usuario" />
                                      </div>
                                      <div class="form-group col-12 col-md-4"> 
                                          Tipo de Documento
                                          <asp:DropDownList runat="server" ID="cboTipoDocumento" data-name="Tipo de Documento" CssClass="form-control" />
                                      </div>
                                      <div class="form-group col-12 col-md-4"> 
                                          Documento
                                          <asp:TextBox runat="server" ID="txtDocumento" MaxLength="20" data-name="Nombre del Usuario" CssClass="form-control" placeholder="Usuario" />
                                      </div>
                                  </div>
                                  <asp:UpdatePanel class="col-12 col-md-3" style="overflow-y:scroll;max-height:300px!important;" runat="server" ID="upTvPerfiles" UpdateMode="Conditional">
                                      <ContentTemplate>
                                          <asp:TreeView ID="tvPerfiles" CssClass="tree-view-asp" runat="server"></asp:TreeView>
                                      </ContentTemplate>
                                  </asp:UpdatePanel>
                                  <div class="col-12">
                                      <asp:LinkButton runat="server" ID="btnGuardarUsuario" OnClick="btnGuardarUsuario_Click" CssClass="btn btn-round btn-outline-success float-right no-caret">
                                        <i class="now-ui-icons design-2_ruler-pencil"></i> Guardar
                                      </asp:LinkButton>
                                  </div>
                              </asp:Panel>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                      <asp:UpdatePanel class="col-12" runat="server" ID="upGridUsuario" UpdateMode="Conditional">
                          <ContentTemplate>
                              <asp:GridView CssClass="table table-hover" ID="gvUsuario" runat="server" AllowPaging="true" AutoGenerateColumns="false" EmptyDataText="No Profile found." GridLines="None"
                                   OnPageIndexChanging="gvUsuario_PageIndexChanging" OnRowCommand="gvUsuario_RowCommand" PageSize="10" ShowHeader="false">
                                  <Columns>
                                      <asp:BoundField HeaderText="Usuario" DataField="strUsuario" />
                                      <asp:BoundField HeaderText="Apellido" DataField="strApellido" />
                                      <asp:BoundField HeaderText="Nombre" DataField="strNombre" />
                                      <asp:BoundField HeaderText="Documento" DataField="strNumeroDocumento" />
                                      <asp:TemplateField ItemStyle-Width="100">
                                          <ItemTemplate>    
                                              <asp:LinkButton ID="btnEditar" CommandName="editItem" CommandArgument='<%# Eval("intIdUsuario") %>' runat="server" CssClass="btn btn-warning btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons design-2_ruler-pencil"></i></asp:LinkButton>
                                              <asp:LinkButton ID="btnReestablecerContrasenia" CommandName="reestablecerContrasenia" CommandArgument='<%# Eval("intIdUsuario") %>' runat="server" CssClass="btn btn-primary btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons objects_key-25"></i></asp:LinkButton>
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
            <!-- USUARIOS FIN -->
            
      </div>
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
                $(item).after('<label class="custom-control-label text-white" for="' + item.id + '">.</label>');
            }

            $(".span-checkbox").addClass('custom-control custom-switch custom-switch-off-danger custom-switch-on-success')
            $(".span-checkbox").children('input').addClass('custom-control-input');
            $(".span-checkbox").children('label').addClass('custom-control-label');
        }
        bindScripts();
    </script>
</asp:Content>