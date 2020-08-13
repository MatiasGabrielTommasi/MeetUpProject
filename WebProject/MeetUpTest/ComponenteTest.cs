using System;
using System.Collections.Generic;
using Datos;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeetUpTest
{
    [TestClass]
    public class ComponenteTest
    {
        [TestMethod]
        public void Cargar()
        {
            Componente obj = new Componente();
            ComponenteDA objDA = new ComponenteDA();
            List<Componente> ret = objDA.Cargar(obj);
            Assert.IsFalse((ret == null));
        }
        [TestMethod]
        public void Guardar()
        {
            string str = "componente";
            Componente obj = new Componente();
            obj.bitHabilitarComponente = true;
            obj.bitMostrarComponente = true;
            obj.intIdComponentePadre = 0;
            obj.oTipoComponente = new TipoComponente(2, string.Empty);
            obj.strComponente = str;
            obj.strDetalleComponente = str;
            obj.strIconoComponente = str;
            obj.strUrlComponente = str;
            ComponenteDA objDA = new ComponenteDA();
            int r = objDA.Guardar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Actualizar()
        {
            string str = "componente actualizado";
            Componente obj = new Componente();
            obj.intIdComponente = 5;
            obj.bitHabilitarComponente = true;
            obj.bitMostrarComponente = true;
            obj.intIdComponentePadre = 0;
            obj.oTipoComponente = new TipoComponente(2, string.Empty);
            obj.strComponente = str;
            obj.strDetalleComponente = str;
            obj.strIconoComponente = str;
            obj.strUrlComponente = str;
            ComponenteDA objDA = new ComponenteDA();
            int r = objDA.Actualizar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Eliminar()
        {
            Componente obj = new Componente();
            obj.intIdComponente = 5;
            ComponenteDA objDA = new ComponenteDA();
            int r = objDA.Eliminar(obj);
            Assert.IsTrue(r > 0);
        }
    }
}
