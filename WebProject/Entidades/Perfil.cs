using System;
using System.Collections.Generic;

namespace Entidades
{
    [Serializable]
    public class Perfil : _ModelType
	{
        public List<Componente> Componentes { get; set; }
		public Perfil() : base() { this.Componentes = new List<Componente>(); }
		public Perfil(int Id, string Descrip) : base(Id, Descrip) { this.Componentes = new List<Componente>(); }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}