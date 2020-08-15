using System;

namespace Entidades
{
    [Serializable]
    public class TipoComponente : _ModelType
	{
		public TipoComponente() : base() { }
		public TipoComponente(int Id, string Descrip) : base(Id, Descrip) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}