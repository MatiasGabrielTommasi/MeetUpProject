using System;
using System.Web.Script.Serialization;

namespace Entidades
{
	[Serializable]
	public class Evento 
	{
		public int intIdEvento { get; set; }
		public string strEvento { get; set; }
		public DateTime datFechaEvento { get; set; }
		public int intTotalAsistentes { get; set; }
		public int intTotalReservas { get; set; }
		public Sala oSala { get; set; }
		public Usuario oUsuarioAnfitrion { get; set; }
		public Evento ()
		{
			int _cero = 0;
			string _vacio = string.Empty;
			this.intIdEvento = _cero;
			this.oSala = new Sala();
			this.oUsuarioAnfitrion = new Usuario();
			this.strEvento = _vacio;
			this.datFechaEvento = DateTime.Now;
			this.intTotalAsistentes = _cero;
			this.intTotalReservas = _cero;
		}
		public Evento (int intIdEvento, string strEvento, DateTime datFechaEvento, int intTodalAsistentes, int intTotalReservas)
		{
			this.intIdEvento = intIdEvento;
			this.oSala = new Sala();
			this.oUsuarioAnfitrion = new Usuario();
			this.strEvento = strEvento;
			this.datFechaEvento = datFechaEvento;
			this.intTotalAsistentes = intTodalAsistentes;
			this.intTotalReservas = intTotalReservas;
		}
        public override string ToString()
        {
			return string.Format("({0}) {1} el día {2}", this.intIdEvento, this.strEvento, this.datFechaEvento.ToString("dd/MM/yyyy"));
        }
		public string ToSalasCalendarioString()
		{
			string _finalizado = "#dc3545";
			string _hoy = "#28a745";
			string _vigente = "#17a2b8";
			string strEvento = string.Empty;

			strEvento += string.Format("title: '{0}',", this.strEvento);
			strEvento += string.Format("start: '{0}'", this.datFechaEvento.ToString("yyyy-MM-dd"));
			if (this.datFechaEvento.Date == DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _hoy);
			else if (this.datFechaEvento.Date > DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _vigente);
			else if (this.datFechaEvento.Date < DateTime.Now.Date)
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
			strEvento += string.Format("start: '{0}'", this.datFechaEvento.ToString("yyyy-MM-dd"));
			if (this.datFechaEvento.Date == DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _hoy);
			else if (this.datFechaEvento.Date > DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _vigente);
			else if (this.datFechaEvento.Date < DateTime.Now.Date)
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

			strEvento += string.Format("title: '{0}',", this.strEvento);
			strEvento += "selectable: true,";
			strEvento += string.Format("evento: {0},", new JavaScriptSerializer().Serialize(this));
			strEvento += string.Format("start: '{0}'", this.datFechaEvento.ToString("yyyy-MM-dd"));
			if (this.datFechaEvento.Date == DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _hoy);
			else if (this.datFechaEvento.Date > DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _vigente);
			else if (this.datFechaEvento.Date < DateTime.Now.Date)
				strEvento += string.Format(",color: '{0}'", _finalizado);
			strEvento = string.Format("{{ {0} }}", strEvento);
			return strEvento;
		}
	}
}