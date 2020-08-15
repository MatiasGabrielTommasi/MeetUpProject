using System;
using System.Collections.Generic;
using System.Net.Security;
using Datos;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilidades;

namespace MeetUpTest
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void Cargar()
        {
            Usuario obj = new Usuario();
            UsuarioDA objDA = new UsuarioDA();
            List<Usuario> ret = objDA.Cargar(obj);
            Assert.IsFalse((ret == null));
        }
        [TestMethod]
        public void Guardar()
        {
            Usuario obj = new Usuario();
            obj.TipoDoc = new TipoDocumento(1, string.Empty);
            obj.Apellido = "Tommasi";
            obj.Nombre = "Matías Gabriel";
            obj.Contrasenia = Funciones.Encrypt("123456");
            obj.Correo = "matiasgabrieltommasi@gmail.com";
            obj.NumeroDoc = "36402762";
            obj.Username = "Charango";
            UsuarioDA objDA = new UsuarioDA();
            int r = objDA.Guardar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Actualizar()
        {
            Usuario obj = new Usuario();
            UsuarioDA objDA = new UsuarioDA();
            obj.Id = 1;
            obj.Apellido = "Tommasi Actualizado";
            obj.Nombre = "Matías Gabriel Actualizado";
            obj.Contrasenia = Funciones.Encrypt("32654");
            obj.Correo = "matiasgabrieltommasi@gmail.com";
            obj.NumeroDoc = "36402762";
            obj.Username = "Charango";
            obj.TipoDoc = new TipoDocumento(1, string.Empty);
            int r = objDA.Actualizar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void Eliminar()
        {
            Usuario obj = new Usuario();
            obj.Id = 1;
            UsuarioDA objDA = new UsuarioDA();
            int r = objDA.Eliminar(obj);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void CargarPerfiles()
        {
            UsuarioDA objDA = new UsuarioDA();
            List<Perfil> r = objDA.CargarPerfiles(2);
            Assert.IsTrue(r.Count > 0);
        }
        [TestMethod]
        public void GuardarPerfil()
        {
            Perfil obj = new Perfil();
            obj.Id = 2;
            UsuarioDA objDA = new UsuarioDA();
            int r = objDA.GuardarPerfil(obj, 2);
            Assert.IsTrue(r > 0);
        }
        [TestMethod]
        public void EliminarPerfiles()
        {
            UsuarioDA objDA = new UsuarioDA();
            int r = objDA.EliminarPerfiles(2);
            Assert.IsTrue(r > 0);
        }
    }
}
