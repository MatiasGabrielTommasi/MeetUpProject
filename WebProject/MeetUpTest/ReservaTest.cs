using System;
using System.Collections.Generic;
using Datos;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilidades;

namespace MeetUpTest
{
    [TestClass]
    public class ReservaTest
    {
        [TestMethod]
        public void Cargar()
        {
            Reserva obj = new Reserva();
            ReservaDA objDA = new ReservaDA();
            List<Reserva> ret = objDA.Cargar(obj);
            Assert.IsFalse((ret == null));
        }
        [TestMethod]
        public void Guardar()
        {
            Reserva obj = new Reserva();
            obj.Fecha = DateTime.Now;
            obj.ConsumeTotal = false;
            obj.EventoSeleccionado.Id = 2;
            obj.Asistente.Id = 2;
            obj.Asistente.Contrasenia = "hHMylv7X7ifcMtSz9YrBoQ==";
            obj.Consumo = Convert.ToDecimal(0);
            ReservaDA objDA = new ReservaDA();
            int r = objDA.Guardar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Eliminar()
        {
            Reserva obj = new Reserva();
            obj.Id = 1;
            ReservaDA objDA = new ReservaDA();
            int r = objDA.Eliminar(obj);
            Assert.IsTrue(r > 0);
        }
    }
}
