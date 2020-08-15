using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Utilidades
{
    public static class ClimaParameters
    {
        public static RestSharp.Parameter oLatParam { get => new RestSharp.Parameter("lat", -34.639115, RestSharp.ParameterType.QueryString); }
        public static RestSharp.Parameter oLonParam { get => new RestSharp.Parameter("lon", -58.404263, RestSharp.ParameterType.QueryString); }
        public static RestSharp.Parameter oLangParam { get => new RestSharp.Parameter("lans", "es", RestSharp.ParameterType.QueryString); }
    }
}
