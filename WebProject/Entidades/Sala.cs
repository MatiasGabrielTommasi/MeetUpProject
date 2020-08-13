using System;
using System.Collections.Generic;

namespace Entidades
{
	[Serializable]
	public class Sala
	{
		public int intIdSala { get; set; }
		public string strSala { get; set; }
		public int intCupo { get; set; }
		public string strUbicacion { get; set; }
        public List<Evento> iEventos { get; set; }
        public Sala ()
		{
			int _cero = 0;
			string _vacio = string.Empty;
			this.intIdSala = _cero;
			this.strSala = _vacio;
			this.intCupo = _cero;
			this.strUbicacion = _vacio;
			this.iEventos = new List<Evento>();
		}
        public Sala (int intIdSala, string strSala, int intCupo, string strUbicacion)
		{
			this.intIdSala = intIdSala;
			this.strSala = strSala;
			this.intCupo = intCupo;
			this.strUbicacion = strUbicacion;
			this.iEventos = new List<Evento>();
		}
        public override string ToString()
        {
			return string.Format("({0}) {1} - cupo total de {2}", this.intIdSala, this.strSala, this.intCupo);
        }
    }
}