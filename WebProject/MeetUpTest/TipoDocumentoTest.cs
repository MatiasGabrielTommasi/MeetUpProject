using System;
using System.Collections.Generic;
using Datos;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeetUpTest
{
    [TestClass]
    public class TipoDocumentoTest
    {
        [TestMethod]
        public void Cargar()
        {
            TipoDocumento obj = new TipoDocumento(0, string.Empty);
            TipoDocumentoDA objDA = new TipoDocumentoDA();
            List<TipoDocumento> ret = objDA.Cargar(obj);
            Assert.IsFalse((ret == null));
        }
        [TestMethod]
        public void Guardar()
        {
            TipoDocumento obj = new TipoDocumento(0, "DNI");
            TipoDocumentoDA objDA = new TipoDocumentoDA();
            int r = objDA.Guardar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Actualizar()
        {
            TipoDocumento obj = new TipoDocumento(1, "DNI");
            TipoDocumentoDA objDA = new TipoDocumentoDA();
            int r = objDA.Actualizar(obj);
            Assert.IsTrue(r > 0);
        }
    }
}
