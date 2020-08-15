<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Reservas.aspx.cs" Inherits="MeetUp.Reservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="assets/fullcalendar/css/fullcalendar.min.css" rel="stylesheet" />
    <link href="assets/fullcalendar/css/styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">   
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="HFintIdEvento" Value="0" />
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
            <a class="navbar-brand" href="#pablo">Reservas</a>
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
            
            <!-- ReservaS -->
          <asp:Panel runat="server" ID="pnlReservas" class="col-12">
            <div class="card card-chart">
              <div class="card-header">
                <h5 class="card-category">Configuraciones</h5>
                <h4 class="card-title">Reservas</h4>
                  <asp:UpdatePanel runat="server" class="float-right new-button p-0">
                      <ContentTemplate>
                          <asp:LinkButton runat="server" ID="btnNuevoReserva" OnClick="btnNuevoReserva_Click" CssClass="btn btn-round btn-outline-success btn-simple no-caret m-0">
                            <i class="now-ui-icons ui-1_calendar-60"></i> Buscar Eventos
                          </asp:LinkButton>
                      </ContentTemplate>
                  </asp:UpdatePanel>
              </div>
              <div class="card-body">
                  <div class="row">
                      <asp:UpdatePanel class="col-12" runat="server" ID="upGridReserva" UpdateMode="Conditional">
                          <ContentTemplate>
                              <asp:GridView CssClass="table table-hover" ID="gvReserva" runat="server" AllowPaging="true" AutoGenerateColumns="false" EmptyDataText="No Profile found." GridLines="None"
                                   OnPageIndexChanging="gvReserva_PageIndexChanging" OnRowCommand="gvReserva_RowCommand" PageSize="10" ShowHeader="false">
                                  <Columns>
                                      <asp:TemplateField>
                                          <ItemTemplate>    
                                              <asp:Label runat="server" Text='<%# Eval("oEvento.strEvento") %>' />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField>
                                          <ItemTemplate>    
                                              <asp:Label runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("oEvento.datFechaEvento")) %>' />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField>
                                          <ItemTemplate>    
                                              <asp:Label runat="server" Text='<%# string.Format("{0} ({1})", Eval("oEvento.oSala.strSala"), Eval("oEvento.oSala.strUbicacion")) %>' />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField ItemStyle-Width="100">
                                          <ItemTemplate>    
                                              <asp:LinkButton ID="btnComprobante" CommandName="qrItem" CommandArgument='<%# Eval("intIdReserva") %>' runat="server" CssClass="btn btn-info btn-round btn-icon btn-icon-mini btn-neutral"><i class="fa fa-qrcode"></i></asp:LinkButton>
                                              <asp:LinkButton ID="btnEliminar" CommandName="deleteItem" CommandArgument='<%# Eval("intIdReserva") %>' runat="server" CssClass="btn btn-primary btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons ui-1_simple-remove"></i></asp:LinkButton>
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
                  <div id="pnlReserva" style="display: none;" class="col-12 text-center">
                      <div class="col-12"> 
                          <label id="lblEvento" />
                      </div>
                          <asp:LinkButton runat="server" ID="btnGuardarReserva" OnClick="btnGuardarReserva_Click" CssClass="btn btn-round btn-outline-success btn-simple no-caret m-0">
                            <i class="now-ui-icons ui-1_calendar-60"></i> Confirmar Reserva
                          </asp:LinkButton>
                  </div>
                <div id="calendar"></div>
              </div>
            </div>
          </div>
        </div>
            <!-- MODAL CALENDARIO FIN -->
            <!-- MODAL QR -->
        <div class="modal fade" id="modalQR" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
          <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
              <div class="modal-body">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                  <br />
                  <asp:UpdatePanel runat="server" class="text-center">
                      <ContentTemplate>
                  <label ID="lblEventoQR" runat="server" />
                <asp:Image runat="server" ID="imgQR" />

                      </ContentTemplate>
                  </asp:UpdatePanel>
              </div>
            </div>
          </div>
        </div>
            <!-- MODAL QR FIN -->
        </asp:Panel>
            <!-- ReservaS FIN -->
        </div>
      </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
    <script src="assets/fullcalendar/js/moment.min.js"></script>
    <script src="assets/fullcalendar/js/fullcalendar.min.js"></script>
    <script>
        function CargarCalendario(Reservas, calendario) {
            $('#' + calendario).fullCalendar("destroy");
            $('#' + calendario).fullCalendar({
                defaultDate: new Date(),
                buttonIcons: true,
                weekNumbers: false,
                editable: false,
                selectable: true,
                eventLimit: false,
                events: Reservas,
                eventClick: function (evento, jsEvent, view) {
                    var _evento = evento.evento;
                    var _sala = _evento.oSala;
                    var _anfitrion = _evento.oUsuarioAnfitrion;
                    var _descrip = `Evento: ${_evento.strEvento} <br/> Fecha: ${moment(_evento.datFechaEvento).format('DD/MM/YYYY')} <br/> Sala: ${_sala.strSala} (${_sala.strUbicacion}) <br/> Anfitrión: ${_anfitrion.strApellido}, ${_anfitrion.strNombre}`;
                    $("#<%= HFintIdEvento.ClientID%>").val(_evento.intIdEvento);
                    $("#lblEvento").html(_descrip);
                    $("#pnlReserva").show();
                }
            });

            if (calendario == "calendar") {
                $("#modalCalendario").modal("show");
            }
        }
        function bindScripts() {
        }
        bindScripts();
    </script>
</asp:Content>
