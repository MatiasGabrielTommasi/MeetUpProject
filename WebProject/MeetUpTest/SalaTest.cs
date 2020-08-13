using System;
using System.Collections.Generic;
using Datos;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeetUpTest
{
    [TestClass]
    public class SalaTest
    {
        [TestMethod]
        public void Cargar()
        {
            Sala obj = new Sala();
            SalaDA objDA = new SalaDA();
            List<Sala> ret = objDA.Cargar(obj);
            Assert.IsFalse((ret == null));
        }
        [TestMethod]
        public void Guardar()
        {
            Sala obj = new Sala();
            obj.intCupo = 20;
            obj.strSala = "Sala arcoiris";
            obj.strUbicacion = "6to Pito of. 23";
            SalaDA objDA = new SalaDA();
            int r = objDA.Guardar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Actualizar()
        {
            Sala obj = new Sala();
            obj.intIdSala = 1;
            obj.intCupo = 20;
            obj.strSala = "Sala arcoiris actualizada";
            obj.strUbicacion = "6to Pito of. 124";
            SalaDA objDA = new SalaDA();
            int r = objDA.Actualizar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Eliminar()
        {
            Sala obj = new Sala();
            obj.intIdSala = 1;
            SalaDA objDA = new SalaDA();
            int r = objDA.Eliminar(obj);
            Assert.IsTrue(r > 0);
        }
    }
}
