<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Salas.aspx.cs" Inherits="MeetUp.Salas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="assets/fullcalendar/css/fullcalendar.min.css" rel="stylesheet" />
    <link href="assets/fullcalendar/css/styles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="HFintIdSala" Value="0" />
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
            <a class="navbar-brand">Salas</a>
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
            
            <!-- SALAS -->
          <asp:Panel runat="server" ID="pnlSalas" class="col-12">
            <div class="card card-chart">
              <div class="card-header">
                <h5 class="card-category">Configuraciones</h5>
                <h4 class="card-title">Salas</h4>
                  <asp:UpdatePanel runat="server" class="float-right new-button p-0">
                      <ContentTemplate>
                          <asp:LinkButton runat="server" ID="btnNuevoSala" OnClick="btnNuevoSala_Click" CssClass="btn btn-round btn-outline-info btn-simple no-caret m-0">
                            <i class="now-ui-icons design-2_ruler-pencil"></i> Nuevo
                          </asp:LinkButton>
                      </ContentTemplate>
                  </asp:UpdatePanel>
              </div>
              <div class="card-body">
                  <div class="row">
                      <asp:UpdatePanel class="col-12" runat="server" UpdateMode="Always" >
                          <ContentTemplate>
                              <asp:Panel runat="server" CssClass="row" ID="pnlSala" Visible="false">
                                  <div class="col-12 row"> 
                                      <div class="form-group col-12 col-md-4"> 
                                          Nombre de la sala
                                          <asp:TextBox runat="server" ID="txtNombre" data-name="Nombre de la Sala" CssClass="form-control" placeholder="Nombre de la sala" />
                                      </div>
                                      <div class="form-group col-12 col-md-4"> 
                                          Cupo máximo de personas
                                          <asp:TextBox runat="server" ID="txtCupo" TextMode="Number" data-name="Cupo" CssClass="form-control" placeholder="Cupo de personas" />
                                      </div>
                                      <div class="form-group col-12 col-md-4"> 
                                          Ubicación de la Sala
                                          <asp:TextBox runat="server" ID="txtUbicacion" data-name="Ubicación de la Sala" CssClass="form-control" placeholder="Ubicación en el edificio" />
                                      </div>
                                  </div>
                                  <div class="col-12">
                                      <asp:LinkButton runat="server" ID="btnGuardarSala" OnClick="btnGuardarSala_Click" CssClass="btn btn-round btn-outline-success float-right no-caret">
                                        <i class="now-ui-icons design-2_ruler-pencil"></i> Guardar
                                      </asp:LinkButton>
                                  </div>
                              </asp:Panel>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                      <asp:UpdatePanel class="col-12" runat="server" ID="upGridSala" UpdateMode="Conditional">
                          <ContentTemplate>
                              <asp:GridView CssClass="table table-hover" ID="gvSala" runat="server" AllowPaging="true" AutoGenerateColumns="false" EmptyDataText="No Profile found." GridLines="None"
                                   OnPageIndexChanging="gvSala_PageIndexChanging" OnRowCommand="gvSala_RowCommand" PageSize="10" ShowHeader="false">
                                  <Columns>
                                      <asp:BoundField HeaderText="Sala" DataField="strSala" />
                                      <asp:BoundField HeaderText="Cupo" DataField="intCupo" />
                                      <asp:BoundField HeaderText="Ubicación" DataField="strUbicacion" />
                                      <asp:TemplateField ItemStyle-Width="100">
                                          <ItemTemplate>    
                                              <asp:Label CssClass="badge badge-pill badge-danger" Text='<%# string.Format("{0}", Eval("intEventosVencidos")) %>' runat="server" />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField ItemStyle-Width="100">
                                          <ItemTemplate>    
                                              <asp:Label CssClass="badge badge-pill badge-success" Text='<%# string.Format("{0}", Eval("intEventosHoy")) %>' runat="server" />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField ItemStyle-Width="100">
                                          <ItemTemplate>    
                                              <asp:Label CssClass="badge badge-pill badge-info" Text='<%# string.Format("{0}", Eval("intEventosVigentes")) %>' runat="server" />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField ItemStyle-Width="100">
                                          <ItemTemplate>    
                                              <asp:LinkButton ID="btnEditar" CommandName="editItem" CommandArgument='<%# Eval("intIdSala") %>' runat="server" CssClass="btn btn-warning btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons design-2_ruler-pencil"></i></asp:LinkButton>
                                              <asp:LinkButton ID="btnEventos" CommandName="eventosItem" CommandArgument='<%# Eval("intIdSala") %>' runat="server" CssClass="btn btn-primary btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons ui-1_calendar-60"></i></asp:LinkButton>
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
            <!-- MODAL CALENDARIO -->
        <div class="modal fade" id="modalCalendario" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
          <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
            <div class="modal-content">
              <div class="modal-body">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                  <br />
                <div id="calendar"></div>
              </div>
            </div>
          </div>
        </div>
            <!-- MODAL CALENDARIO FIN -->
        </asp:Panel>
            <!-- SALAS FIN -->

          
            
      </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
    <script src="assets/fullcalendar/js/moment.min.js"></script>
    <script src="assets/fullcalendar/js/fullcalendar.min.js"></script>
    <script>
        function CargarCalendario(eventos) {
            console.log(eventos);
            $('#calendar').fullCalendar("destroy");
            $('#calendar').fullCalendar({
                defaultDate: new Date(),
                buttonIcons: true,
                weekNumbers: false,
                editable: false,
                eventLimit: false,
                events:  eventos,
                dayClick: function (date, jsEvent, view) {
                    console.log("Dia click");
                },
                eventClick: function (calEvent, jsEvent, view) {
                    console.log("Event click");
                },
            });
            $("#modalCalendario").modal("show"); 
        }
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