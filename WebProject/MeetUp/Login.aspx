<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MeetUp.Login" %>
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
            <a class="navbar-brand">Iniciar Sesión</a>
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

            <!-- LOGIN -->
          <div class="col-12 col-lg-6 mx-auto">
            <div class="card text-center">
              <div class="card-header">
              </div>
              <div class="card-body">
                    Usuario
                    <asp:TextBox runat="server" data-name="Nombre de Usuario" ID="txtUsuario" CssClass="form-control" placeholder="Usuario" />
                    Contraseña
                    <asp:TextBox runat="server" data-name="Contraseña" ID="txtContrasenia" TextMode="Password" CssClass="form-control" placeholder="Contraseña" />
                  <asp:UpdatePanel runat="server" UpdateMode="Always" class="col-12">
                      <ContentTemplate>                          
                          <asp:LinkButton runat="server" ID="btnLogin" OnClick="btnLogin_Click" CssClass="btn btn-round btn-outline-danger btn-simple no-caret my-2">
                            <i class="now-ui-icons objects_key-25"></i> Iniciar Sesión
                          </asp:LinkButton>
                            <br />                    
                          <a href="Registrarse.aspx" class="btn btn-round btn-outline-info btn-simple no-caret my-2"> Registrarse</a>
                      </ContentTemplate>
                  </asp:UpdatePanel>
              </div>
              <div class="card-footer">
              </div>
            </div>
        </div>
            <!-- LOGIN FIN -->
            
            
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