using System;
using System.Collections.Generic;
using Datos;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeetUpTest
{
    [TestClass]
    public class EventoTest
    {
        [TestMethod]
        public void Cargar()
        {
            Evento obj = new Evento();
            EventoDA objDA = new EventoDA();
            List<Evento> ret = objDA.Cargar(obj);
            Assert.IsFalse((ret == null));
        }
        [TestMethod]
        public void Guardar()
        {
            Evento obj = new Evento();
            obj.datFechaEvento = DateTime.Now.AddDays(15);
            obj.TotalAsistentes = 15;
            obj.Salon.Id = 2;
            obj.UsuarioAnfitrion.Id = 2;
            obj.Nombre = "Festejamos los logros del año!";
            EventoDA objDA = new EventoDA();
            int r = objDA.Guardar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Actualizar()
        {
            Evento obj = new Evento();
            obj.Id = 1;
            obj.datFechaEvento = DateTime.Now.AddDays(30);
            obj.TotalAsistentes = 15;
            obj.Salon.Id = 2;
            obj.UsuarioAnfitrion.Id = 2;
            obj.Nombre = "SE CAMBIO LA FECHA!!!! Festejamos los logros del año!";
            EventoDA objDA = new EventoDA();
            int r = objDA.Actualizar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Eliminar()
        {
            Evento obj = new Evento();
            obj.Id = 1;
            EventoDA objDA = new EventoDA();
            int r = objDA.Eliminar(obj);
            Assert.IsTrue(r > 0);
        }
    }
}
