using System;
using System.Collections.Generic;

namespace Entidades
{
	[Serializable]
	public class Usuario 
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Contrasenia { get; set; }
		public DateTime FechaCreacion { get; set; }
		public string Correo { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public TipoDocumento TipoDoc { get; set; }
		public string NumeroDoc { get; set; }
        public List<Perfil> Perfiles { get; set; }
        public Usuario ()
		{
			this.Id = 0;
			this.Username = string.Empty;
			this.Contrasenia = string.Empty;
			this.FechaCreacion = DateTime.Now;
			this.Correo = string.Empty;
			this.Nombre = string.Empty;
			this.Apellido = string.Empty;
			this.TipoDoc = new TipoDocumento();
			this.NumeroDoc = string.Empty;
			this.Perfiles = new List<Perfil>();
		}
		public Usuario (int Id, string Username, string Contrasenia, DateTime FechaCreacion, string Correo, string Nombre, string Apellido, string NumeroDoc)
		{
			this.Id = Id;
			this.Username = Username;
			this.Contrasenia = Contrasenia;
			this.FechaCreacion = FechaCreacion;
			this.Correo = Correo;
			this.Nombre = Nombre;
			this.Apellido = Apellido;
			this.TipoDoc = new TipoDocumento();
			this.NumeroDoc = NumeroDoc;
			this.Perfiles = new List<Perfil>();
		}
        public override string ToString()
        {
			return string.Format("({0}) {1} - Nombre: {2}, {3} - Documento: {4}", this.Id, this.Username, this.Apellido, this.Nombre, this.NumeroDoc);
        }
    }
}