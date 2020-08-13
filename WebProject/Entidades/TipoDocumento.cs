using System;

namespace Entidades
{
    [Serializable]
    public class TipoDocumento : _ModelType
    {
        public TipoDocumento() : base() { }
        public TipoDocumento(int intId, string strDescrip) : base(intId, strDescrip) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}