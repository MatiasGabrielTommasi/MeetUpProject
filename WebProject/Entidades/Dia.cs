using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Dia : ApiTemperature
    {
        public Dia() : base() { }
        public Dia(DateTime Fecha, decimal Temperatura) : base(Fecha, Temperatura) { }
    }
}
