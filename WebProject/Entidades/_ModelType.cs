using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class _ModelType
    {
        public int Id { get; set; }
        public string Descrip { get; set; }
        public _ModelType()
        {
            this.Id = 0;
            this.Descrip = string.Empty;
        }
        public _ModelType(int Id, string Descrip)
        {
            this.Id = Id;
            this.Descrip = Descrip;
        }
        public override string ToString()
        {
            return string.Format("({0}) {1}", this.Id, this.Descrip);
        }
    }
}
