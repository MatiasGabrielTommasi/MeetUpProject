using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Datos
{
    /// <summary>
    /// Objeto de Conexión con la base de datos, se obtiene oConn que es el objeto de la clase
    /// SqlConnection que se utilizará para interactuar con sqlServer
    /// </summary>
    public class DatabaseConnection
    {
        //localhost
        internal static string CadenaConexion = @"Data Source=DESKTOP-AB1CKUJ;Initial Catalog=MeetUp;Integrated Security = true";
        //production
        //internal static string strConexion = @"server=db763093926.hosting-data.io;DataBase=db763093926; user id = dbo763093926; password = Iqinspire1!;";
        //testing
        //internal static string strConexion = @"server=db763963056.hosting-data.io;DataBase=db763963056; user id = dbo763963056; password = Mati2310!;";
        //development
        //internal static string strConexion = @"server=db778813374.hosting-data.io;DataBase=db778813374; user id = dbo778813374; password = Mati2310!;";
        internal static SqlConnection ConexionBase = new SqlConnection(CadenaConexion);
    }
} 