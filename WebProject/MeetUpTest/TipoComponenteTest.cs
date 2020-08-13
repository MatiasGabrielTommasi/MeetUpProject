using System;
using Entidades;
using Datos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MeetUpTest
{
    [TestClass]
    public class TipoComponenteTest
    {
        [TestMethod]
        public void Cargar()
        {
            TipoComponente obj = new TipoComponente(0, string.Empty);
            TipoComponenteDA objDA = new TipoComponenteDA();
            List<TipoComponente> ret = objDA.Cargar(obj);
            Assert.IsFalse((ret == null));
        }
        [TestMethod]
        public void Guardar()
        {
            TipoComponente obj = new TipoComponente(0, "Boton");
            TipoComponenteDA objDA = new TipoComponenteDA();
            int r = objDA.Guardar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Actualizar()
        {
            TipoComponente obj = new TipoComponente(1, "Boton Actualizado");
            TipoComponenteDA objDA = new TipoComponenteDA();
            int r = objDA.Actualizar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Eliminar()
        {
            TipoComponente obj = new TipoComponente(1, string.Empty);
            TipoComponenteDA objDA = new TipoComponenteDA();
            int r = objDA.Eliminar(obj);
            Assert.IsTrue(r > 0);
        }
    }
}
