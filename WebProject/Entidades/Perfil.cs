using System;
using System.Collections.Generic;

namespace Entidades
{
    [Serializable]
    public class Perfil : _ModelType
	{
        public List<Componente> iComponentes { get; set; }
		public Perfil() : base() { this.iComponentes = new List<Componente>(); }
		public Perfil(int intId, string strDescrip) : base(intId, strDescrip) { this.iComponentes = new List<Componente>(); }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}