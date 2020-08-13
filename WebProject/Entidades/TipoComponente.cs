using System;

namespace Entidades
{
    [Serializable]
    public class TipoComponente : _ModelType
	{
		public TipoComponente() : base() { }
		public TipoComponente(int intId, string strDescrip) : base(intId, strDescrip) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}