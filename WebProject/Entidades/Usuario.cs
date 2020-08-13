using System;
using System.Collections.Generic;

namespace Entidades
{
	[Serializable]
	public class Usuario 
	{
		public int intIdUsuario { get; set; }
		public string strUsuario { get; set; }
		public string strContrasenia { get; set; }
		public DateTime datFechaUsuario { get; set; }
		public string strCorreoUsuario { get; set; }
		public string strNombre { get; set; }
		public string strApellido { get; set; }
		public TipoDocumento oTipoDocumento { get; set; }
		public string strNumeroDocumento { get; set; }
        public List<Perfil> iPerfiles { get; set; }
        public Usuario ()
		{
			int _cero = 0;
			string _vacio = string.Empty;
			this.intIdUsuario = _cero;
			this.strUsuario = _vacio;
			this.strContrasenia = _vacio;
			this.datFechaUsuario = DateTime.Now;
			this.strCorreoUsuario = _vacio;
			this.strNombre = _vacio;
			this.strApellido = _vacio;
			this.oTipoDocumento = new TipoDocumento();
			this.strNumeroDocumento = _vacio;
			this.iPerfiles = new List<Perfil>();
		}
		public Usuario (int intIdUsuario, string strUsuario, string strContrasenia, DateTime datFechaUsuario, string strCorreoUsuario, string strNombre, string strApellido, string strNumeroDocumento)
		{
			this.intIdUsuario = intIdUsuario;
			this.strUsuario = strUsuario;
			this.strContrasenia = strContrasenia;
			this.datFechaUsuario = datFechaUsuario;
			this.strCorreoUsuario = strCorreoUsuario;
			this.strNombre = strNombre;
			this.strApellido = strApellido;
			this.oTipoDocumento = new TipoDocumento();
			this.strNumeroDocumento = strNumeroDocumento;
			this.iPerfiles = new List<Perfil>();
		}
        public override string ToString()
        {
			return string.Format("({0}) {1} - Nombre: {2}, {3} - Documento: {4}", this.intIdUsuario, this.strUsuario, this.strApellido, this.strNombre, this.strNumeroDocumento);
        }
    }
}