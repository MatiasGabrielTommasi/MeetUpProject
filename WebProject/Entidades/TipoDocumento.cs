using System;

namespace Entidades
{
    [Serializable]
    public class TipoDocumento : _ModelType
    {
        public TipoDocumento() : base() { }
        public TipoDocumento(int Id, string Descrip) : base(Id, Descrip) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}