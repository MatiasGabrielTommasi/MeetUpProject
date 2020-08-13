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
        public int intId { get; set; }
        public string strDescrip { get; set; }
        public _ModelType()
        {
            this.intId = 0;
            this.strDescrip = string.Empty;
        }
        public _ModelType(int intId, string strDescrip)
        {
            this.intId = intId;
            this.strDescrip = strDescrip;
        }
        public override string ToString()
        {
            return string.Format("({0}) {1}", this.intId, this.strDescrip);
        }
    }
}
