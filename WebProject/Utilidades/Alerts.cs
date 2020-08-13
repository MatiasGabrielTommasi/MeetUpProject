using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public static class Alerts
    {
        public enum Alert
        {
            TipoDocumento,
            TipoComponente,
            Componente,
            Perfil,
            Usuario,
            Sala,
            Evento,
            Reserva
        }
        public static string TipoDocumento = "tipo-documento";
        public static string TipoComponente = "tipo-componente";
        public static string Componente = "componente";
        public static string Perfil = "perfil";
        public static string Usuario = "usuario";
        public static string Sala = "sala";
        public static string Evento = "evento";
        public static string Reserva = "reserva";
    }
}
