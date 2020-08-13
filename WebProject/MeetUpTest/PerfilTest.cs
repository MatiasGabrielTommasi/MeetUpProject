using System;
using System.Collections.Generic;
using Datos;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeetUpTest
{
    [TestClass]
    public class PerfilTest
    {
        [TestMethod]
        public void Cargar()
        {
            Perfil obj = new Perfil();
            PerfilDA objDA = new PerfilDA();
            List<Perfil> ret = objDA.Cargar(obj);
            Assert.IsFalse((ret == null));
        }
        [TestMethod]
        public void Guardar()
        {
            Perfil obj = new Perfil(0, "System Admin");
            PerfilDA objDA = new PerfilDA();
            int r = objDA.Guardar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Actualizar()
        {
            Perfil obj = new Perfil(1, "System Admin Actualizado");
            PerfilDA objDA = new PerfilDA();
            int r = objDA.Actualizar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Eliminar()
        {
            Perfil obj = new Perfil(1, string.Empty);
            PerfilDA objDA = new PerfilDA();
            int r = objDA.Eliminar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void CargarComponentes()
		{
			PerfilDA objDA = new PerfilDA();
			List<Componente> r = objDA.CargarComponentes(2);
			Assert.IsTrue(r.Count > 0);
        }
        [TestMethod]
        public void GuardarComponente()
		{
			Componente obj = new Componente();
            obj.intIdComponente = 6;
			PerfilDA objDA = new PerfilDA();
			int r = objDA.GuardarComponente(obj, 2);
			Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void EliminarComponentes()
        {
            PerfilDA objDA = new PerfilDA();
            int r = objDA.EliminarComponentes(2);
            Assert.IsTrue(r > 0);
        }
	}
}
