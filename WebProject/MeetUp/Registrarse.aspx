<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="MeetUp.Registrarse" %>
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
              </div>
              <div class="card-body">
                  <div class="row">
                      <asp:UpdatePanel class="col-12" runat="server" UpdateMode="Always" >
                          <ContentTemplate>
                              <asp:Panel runat="server" CssClass="row" ID="pnlUsuario" Visible="false">
                                  <div class="col-12 row"> 
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
                                  <div class="col-12">
                                      <asp:LinkButton runat="server" ID="btnGuardarUsuario" OnClick="btnGuardarUsuario_Click" CssClass="btn btn-round btn-outline-success float-right no-caret">
                                        <i class="now-ui-icons design-2_ruler-pencil"></i> Guardar
                                      </asp:LinkButton>
                                  </div>
                              </asp:Panel>
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
        }
        bindScripts();
    </script>
</asp:Content>