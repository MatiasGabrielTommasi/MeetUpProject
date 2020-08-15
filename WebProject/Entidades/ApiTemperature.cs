using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ApiTemperature
    {
        public DateTime Fecha { get; set; }
        public decimal Temperatura { get; set; }
        public ApiTemperature()
        {
            this.Fecha = new DateTime();
            this.Temperatura = 0;
        }
        public ApiTemperature(DateTime Fecha, decimal Temperatura)
        {
            this.Fecha = Fecha;
            this.Temperatura = Temperatura;
        }
    }
}
