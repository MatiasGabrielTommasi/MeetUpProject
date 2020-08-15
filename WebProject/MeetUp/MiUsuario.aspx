<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MiUsuario.aspx.cs" Inherits="MeetUp.MiUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
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
            <a class="navbar-brand">Cambiar Contraseña</a>
          </div>
          <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigation" aria-controls="navigation-index" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-bar navbar-kebab"></span>
            <span class="navbar-toggler-bar navbar-kebab"></span>
            <span class="navbar-toggler-bar navbar-kebab"></span>
          </button>
        </div>
      </nav>
      <!-- End Navbar -->
      <div class="panel-header panel-header-sm"></div>
      <div class="content">
        <div class="row">
            
            <!-- USUARIOS -->
          <asp:Panel runat="server" ID="pnlUsuarios" class="col-12 col-md-6 offset-md-3 ">
            <div class="card card-chart">
              <div class="card-header">
              </div>
              <div class="card-body">
                  <div class="row">
                      <asp:UpdatePanel class="col-12" runat="server" UpdateMode="Always" >
                          <ContentTemplate>
                              <asp:Panel runat="server" CssClass="row" ID="pnlUsuario">
                                  <div class="col-12"> 
                                      <div class="form-group col-12"> 
                                          Contraseña Nueva
                                          <asp:TextBox runat="server" ID="txtContrasenia" data-name="Contraseña" CssClass="form-control" placeholder="Contraseña" />
                                      </div>
                                      <div class="form-group col-12"> 
                                          Repetir Contraseña Nueva
                                          <asp:TextBox runat="server" ID="txtContrasenia2" data-name="Repetir Contraseña" CssClass="form-control" placeholder="Repetir Contraseña" />
                                      </div>
                                  </div>
                                  <div class="col-12 text-center">
                                      <asp:LinkButton runat="server" ID="btnCambiarContraseña" OnClick="btnCambiarContraseña_Click" CssClass="btn btn-round btn-outline-success no-caret">
                                        <i class="now-ui-icons objects_key-25"></i> Cambiar Contraseña
                                      </asp:LinkButton>
                                  </div>
                              </asp:Panel>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
            </div>
          </div>
        </asp:Panel>
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