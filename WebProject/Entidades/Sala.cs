using System;
using System.Collections.Generic;

namespace Entidades
{
	[Serializable]
	public class Sala
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int Cupo { get; set; }
		public string Ubicacion { get; set; }
        public int EventosVencidos { get; set; }
        public int EventosHoy { get; set; }
        public int EventosVigentes { get; set; }
        public List<Evento> Eventos { get; set; }
        public Sala ()
		{
			this.Id = 0;
			this.Nombre = string.Empty;
			this.Cupo = 0;
			this.Ubicacion = string.Empty;
			this.EventosHoy = 0;
			this.EventosVencidos = 0;
			this.EventosVigentes = 0;
			this.Eventos = new List<Evento>();
		}
        public Sala (int Id, string Nombre, int Cupo, string Ubicacion)
		{
			int _cero = 0;
			this.Id = Id;
			this.Nombre = Nombre;
			this.Cupo = Cupo;
			this.Ubicacion = Ubicacion;
			this.EventosHoy = _cero;
			this.EventosVencidos = _cero;
			this.EventosVigentes = _cero;
			this.Eventos = new List<Evento>();
		}
        public override string ToString()
        {
			return string.Format("({0}) {1} - cupo total de {2}", this.Id, this.Nombre, this.Cupo);
        }
    }
}