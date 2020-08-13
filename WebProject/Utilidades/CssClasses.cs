using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public static class CssClasses
    {
        public enum CssClass
        {
            Info,
            Success,
            Warning,
            Danger,
            Default
        }
        public static string Info = "info";
        public static string Success = "success";
        public static string Warning = "warning";
        public static string Danger = "danger";
        public static string Default = "default";
    }
}
