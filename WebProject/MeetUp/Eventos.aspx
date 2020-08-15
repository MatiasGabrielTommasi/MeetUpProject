<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Eventos.aspx.cs" Inherits="MeetUp.Eventos" %>
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
            <a class="navbar-brand" href="#pablo">Eventos</a>
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
            
            <!-- EVENTOS -->
          <asp:Panel runat="server" ID="pnlEventos" class="col-12">
            <div class="card card-chart">
              <div class="card-header">
                <h5 class="card-category">Configuraciones</h5>
                <h4 class="card-title">Eventos</h4>
                  <asp:UpdatePanel runat="server" class="float-right new-button p-0">
                      <ContentTemplate>
                          <asp:LinkButton runat="server" ID="btnNuevoEvento" OnClick="btnNuevoEvento_Click" CssClass="btn btn-round btn-outline-info btn-simple no-caret m-0">
                            <i class="now-ui-icons design-2_ruler-pencil"></i> Nuevo
                          </asp:LinkButton>
                      </ContentTemplate>
                  </asp:UpdatePanel>
              </div>
              <div class="card-body">
                  <div class="row">
                      <asp:UpdatePanel class="col-12" runat="server" UpdateMode="Always" >
                          <ContentTemplate>
                              <asp:Panel runat="server" CssClass="row" ID="pnlEvento" Visible="false">
                                  <div class="col-12 col-md-6 col-xl-8 row"> 
                                      <div class="form-group col-12 col-md-6"> 
                                          Fecha Seleccionada
                                          <asp:TextBox runat="server" ID="txtFecha" data-name="Fecha del Evento" CssClass="form-control" Enabled="false" />
                                      </div>
                                      <div class="form-group col-12 col-md-6"> 
                                          Sala para el evento
                                          <asp:UpdatePanel runat="server">
                                              <ContentTemplate>
                                                      <asp:DropDownList runat="server" ID="cboSala" OnSelectedIndexChanged="cboSala_SelectedIndexChanged" AutoPostBack="true" data-name="Sala para el Evento" CssClass="form-control" />
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                      </div>
                                      <div class="form-group col-12 col-md-6"> 
                                          Nombre de la Evento
                                          <asp:TextBox runat="server" ID="txtNombre" data-name="Nombre de la Evento" CssClass="form-control" placeholder="Nombre de la Evento" />
                                      </div>
                                      <div class="form-group col-12 col-md-6"> 
                                          Cantidad de invitados <small runat="server" id="smCantidadMaxima"></small>
                                          <asp:TextBox runat="server" ID="txtCupo" TextMode="Number" data-name="Cupo" CssClass="form-control" placeholder="Cupo de personas" />
                                      </div>
                                  </div>
                                  <div class="col-12 col-md-6 col-xl-4">                                      
                                    <div id="calendar-reservation"></div>
                                  </div>
                                  <div class="col-12">
                                      <asp:LinkButton runat="server" ID="btnGuardarEvento" OnClick="btnGuardarEvento_Click" CssClass="btn btn-round btn-outline-success float-right no-caret">
                                        <i class="now-ui-icons design-2_ruler-pencil"></i> Guardar
                                      </asp:LinkButton>
                                  </div>
                              </asp:Panel>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                      <asp:UpdatePanel class="col-12" runat="server" ID="upGridEvento" UpdateMode="Conditional">
                          <ContentTemplate>
                              <asp:GridView CssClass="table table-hover" ID="gvEvento" runat="server" AllowPaging="true" AutoGenerateColumns="false" EmptyDataText="No Profile found." GridLines="None"
                                   OnPageIndexChanging="gvEvento_PageIndexChanging" OnRowDataBound="gvEvento_RowDataBound" OnRowCommand="gvEvento_RowCommand" PageSize="10" ShowHeader="false">
                                  <Columns>
                                      <asp:BoundField HeaderText="Evento" DataField="Nombre" />
                                      <asp:TemplateField>
                                          <ItemTemplate>    
                                              <asp:Label runat="server" ID="lblCervezas" />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField>
                                          <ItemTemplate>    
                                              <asp:Label runat="server" ID="lblFecha" />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField>
                                          <ItemTemplate>    
                                              <asp:Label runat="server" Text='<%# string.Format("{0} / {1}", Eval("TotalReservas"), Eval("TotalAsistentes")) %>' />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField ItemStyle-Width="100">
                                          <ItemTemplate>    
                                              <asp:LinkButton ID="btnEditar" CommandName="editItem" CommandArgument='<%# string.Format("{0}-{1}", Eval("Id"), Eval("Salon.Id")) %>' runat="server" CssClass="btn btn-warning btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons design-2_ruler-pencil"></i></asp:LinkButton>
                                              <asp:LinkButton ID="btnEliminar" CommandName="deleteItem" CommandArgument='<%# string.Format("{0}-{1}", Eval("Id"), Eval("Salon.Id")) %>' runat="server" CssClass="btn btn-primary btn-round btn-icon btn-icon-mini btn-neutral"><i class="now-ui-icons ui-1_simple-remove"></i></asp:LinkButton>
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
            <!-- EVENTOS FIN -->
        </div>
      </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
    <script src="assets/fullcalendar/js/moment.min.js"></script>
    <script src="assets/fullcalendar/js/fullcalendar.min.js"></script>
    <script>
        function CargarCalendario(eventos, calendario) {
            $('#' + calendario).fullCalendar("destroy");
            $('#' + calendario).fullCalendar({
                defaultDate: new Date(),
                buttonIcons: true,
                weekNumbers: false,
                editable: false,
                selectable: true,
                eventLimit: false,
                events: eventos,
                dayClick: function (date, jsEvent, view) {
                    console.log(date._d);
                    $("#<%= txtFecha.ClientID %>").val(date._d.toISOString().split("T")[0]);
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
