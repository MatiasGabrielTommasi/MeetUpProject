using System;

namespace Entidades
{
	[Serializable]
	public class Evento 
	{
		public int intIdEvento { get; set; }
		public string strEvento { get; set; }
		public DateTime datFechaEvento { get; set; }
		public int intTodalAsistentes { get; set; }
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
			this.intTodalAsistentes = _cero;
		}
		public Evento (int intIdEvento, string strEvento, DateTime datFechaEvento, int intTodalAsistentes)
		{
			this.intIdEvento = intIdEvento;
			this.oSala = new Sala();
			this.oUsuarioAnfitrion = new Usuario();
			this.strEvento = strEvento;
			this.datFechaEvento = datFechaEvento;
			this.intTodalAsistentes = intTodalAsistentes;
		}
        public override string ToString()
        {
			return string.Format("({0}) {1} el día {2}", this.intIdEvento, this.strEvento, this.datFechaEvento.ToString("dd/MM/yyyy"));
        }
    }
}