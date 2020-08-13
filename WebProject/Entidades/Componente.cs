using System;

namespace Entidades
{
	[Serializable]
	public class Componente 
	{
		public int intIdComponente { get; set; }
		public int intIdComponentePadre { get; set; }
		public TipoComponente oTipoComponente { get; set; }
		public string strComponente { get; set; }
		public string strUrlComponente { get; set; }
		public bool bitHabilitarComponente { get; set; }
		public bool bitMostrarComponente { get; set; }
		public string strDetalleComponente { get; set; }
		public string strIconoComponente { get; set; }
		public Componente()
		{
			int _cero = 0;
			string _vacio = string.Empty;
			this.intIdComponente = _cero;
			this.intIdComponentePadre = _cero;
			this.oTipoComponente = new TipoComponente(); ;
			this.strComponente = _vacio;
			this.strUrlComponente = _vacio;
			this.bitHabilitarComponente = false;
			this.bitMostrarComponente = false;
			this.strDetalleComponente = _vacio;
			this.strIconoComponente = _vacio;
		}
		public Componente (int intIdComponente, int intIdComponentePadre, TipoComponente oTipoComponente, string strComponente, string strUrlComponente, bool bitHabilitarComponente, bool bitMostrarComponente, string strDetalleComponente, string strIconoComponente)
		{
			this.intIdComponente = intIdComponente;
			this.intIdComponentePadre = intIdComponentePadre;
			this.oTipoComponente = oTipoComponente;
			this.strComponente = strComponente;
			this.strUrlComponente = strUrlComponente;
			this.bitHabilitarComponente = bitHabilitarComponente;
			this.bitMostrarComponente = bitMostrarComponente;
			this.strDetalleComponente = strDetalleComponente;
			this.strIconoComponente = strIconoComponente;
		}
        public override string ToString()
        {
			return string.Format("({0}) {1}", this.intIdComponente, this.strComponente);
        }
    }
}