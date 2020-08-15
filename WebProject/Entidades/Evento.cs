using System;
using System.Web.Script.Serialization;

namespace Entidades
{
	[Serializable]
	public class Evento 
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
        public Dia DiaSeleccionado { get; set; }
		public int TotalAsistentes { get; set; }
		public int TotalReservas { get; set; }
		public Sala Salon { get; set; }
		public Usuario UsuarioAnfitrion { get; set; }
		public Evento ()
		{
			this.Id = 0;
			this.Salon = new Sala();
			this.UsuarioAnfitrion = new Usuario();
			this.Nombre = string.Empty;
			this.DiaSeleccionado = new Dia();
			this.TotalAsistentes = 0;
			this.TotalReservas = 0;
		}
		public Evento (int Id, string Nombre, int TotalAsistentes, int TotalReservas)
		{
			this.Id = Id;
			this.Salon = new Sala();
			this.UsuarioAnfitrion = new Usuario();
			this.Nombre = Nombre;
			this.DiaSeleccionado = new Dia(); ;
			this.TotalAsistentes = TotalAsistentes;
			this.TotalReservas = TotalReservas;
		}
        public override string ToString()
        {
			return string.Format("({0}) {1} el día {2}", this.Id, this.Nombre, this.DiaSeleccionado.Fecha.ToString("dd/MM/yyyy"));
        }
		public string ToSalasCalendarioString()
		{
			string _finalizado = "#dc3545";
			string _hoy = "#28a745";
			string _vigente = "#17a2b8";
			string strEvento = string.Empty;

			strEvento += string.Format("title: '{0}',", this.Nombre);
			strEvento += string.Format("start: '{0}'", this.DiaSeleccionado.Fecha.ToString("yyyy-MM-dd"));
			if (this.DiaSeleccionado.Fecha.Date == DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _hoy);
			else if (this.DiaSeleccionado.Fecha.Date > DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _vigente);
			else if (this.DiaSeleccionado.Fecha.Date < DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _finalizado);
			strEvento = string.Format("{{ {0} }}", strEvento);
			return strEvento;
		}
		public string ToEventoCalendarioString()
		{
			string _finalizado = "#dc3545";
			string _hoy = "#28a745";
			string _vigente = "#17a2b8";
			string strEvento = string.Empty;

			strEvento += "title: 'RESERVADA',";
			strEvento += "selectable: false,";
			strEvento += string.Format("start: '{0}'", this.DiaSeleccionado.Fecha.ToString("yyyy-MM-dd"));
			if (this.DiaSeleccionado.Fecha.Date == DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _hoy);
			else if (this.DiaSeleccionado.Fecha.Date > DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _vigente);
			else if (this.DiaSeleccionado.Fecha.Date < DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _finalizado);
			strEvento = string.Format("{{ {0} }}", strEvento);
			return strEvento;
		}
		public string ToReservaCalendarioString()
		{
			string _finalizado = "#dc3545";
			string _hoy = "#28a745";
			string _vigente = "#17a2b8";
			string strEvento = string.Empty;

			strEvento += string.Format("title: '{0}',", this.Nombre);
			strEvento += "selectable: true,";
			strEvento += string.Format("evento: {0},", new JavaScriptSerializer().Serialize(this));
			strEvento += string.Format("start: '{0}'", this.DiaSeleccionado.Fecha.ToString("yyyy-MM-dd"));
			if (this.DiaSeleccionado.Fecha.Date == DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _hoy);
			else if (this.DiaSeleccionado.Fecha.Date > DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _vigente);
			else if (this.DiaSeleccionado.Fecha.Date < DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _finalizado);
			strEvento = string.Format("{{ {0} }}", strEvento);
			return strEvento;
		}
	}
}