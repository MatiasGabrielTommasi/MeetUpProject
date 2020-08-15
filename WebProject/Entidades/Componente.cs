using System;

namespace Entidades
{
	[Serializable]
	public class Componente 
	{
		public int Id { get; set; }
		public int IdPadre { get; set; }
		public TipoComponente Tipo { get; set; }
		public string Nombre { get; set; }
		public string Url { get; set; }
		public bool Habilitado { get; set; }
		public bool Mostrado { get; set; }
		public string Detalle { get; set; }
		public string Icono { get; set; }
		public Componente()
		{
			this.Id = 0;
			this.IdPadre = 0;
			this.Tipo = new TipoComponente(); ;
			this.Nombre = string.Empty;
			this.Url = string.Empty;
			this.Habilitado = false;
			this.Mostrado = false;
			this.Detalle = string.Empty;
			this.Icono = string.Empty;
		}
		public Componente (int Id, int IdPadre, TipoComponente Tipo, string Nombre, string Url, bool Habilitado, bool Mostrado, string Detalle, string Icono)
		{
			this.Id = Id;
			this.IdPadre = IdPadre;
			this.Tipo = Tipo;
			this.Nombre = Nombre;
			this.Url = Url;
			this.Habilitado = Habilitado;
			this.Mostrado = Mostrado;
			this.Detalle = Detalle;
			this.Icono = Icono;
		}
        public override string ToString()
        {
			return string.Format("({0}) {1}", this.Id, this.Nombre);
        }
    }
}