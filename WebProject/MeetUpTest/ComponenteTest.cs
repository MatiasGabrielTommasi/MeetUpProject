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
            obj.Habilitado = true;
            obj.Mostrado = true;
            obj.IdPadre = 0;
            obj.Tipo = new TipoComponente(2, string.Empty);
            obj.Nombre = str;
            obj.Detalle = str;
            obj.Icono = str;
            obj.Url = str;
            ComponenteDA objDA = new ComponenteDA();
            int r = objDA.Guardar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Actualizar()
        {
            string str = "componente actualizado";
            Componente obj = new Componente();
            obj.Id = 5;
            obj.Habilitado = true;
            obj.Mostrado = true;
            obj.IdPadre = 0;
            obj.Tipo = new TipoComponente(2, string.Empty);
            obj.Nombre = str;
            obj.Detalle = str;
            obj.Icono = str;
            obj.Url = str;
            ComponenteDA objDA = new ComponenteDA();
            int r = objDA.Actualizar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Eliminar()
        {
            Componente obj = new Componente();
            obj.Id = 5;
            ComponenteDA objDA = new ComponenteDA();
            int r = objDA.Eliminar(obj);
            Assert.IsTrue(r > 0);
        }
    }
}
