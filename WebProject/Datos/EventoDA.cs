using Entidades;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class EventoDA {
		public List<Evento> Cargar(Evento obj)
		{
			string strSQL = "Evento_get";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Evento> iEvento = new List<Evento>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_evento", obj.intIdEvento);
				cmd.Parameters.AddWithValue("@id_sala", obj.oSala.intIdSala);
				cmd.Parameters.AddWithValue("@id_usuario", obj.oUsuarioAnfitrion.intIdUsuario);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Evento oEvento = new Evento();
					oEvento.intIdEvento = (objRow["id_evento"] != DBNull.Value) ? Convert.ToInt32(objRow["id_evento"].ToString()) : 0;
					oEvento.strEvento = (objRow["evento"] != DBNull.Value) ? objRow["evento"].ToString() : string.Empty;
					oEvento.datFechaEvento = (objRow["fecha_evento"] != DBNull.Value) ? Convert.ToDateTime(objRow["fecha_evento"].ToString()) : new DateTime();
					oEvento.intTotalAsistentes = (objRow["todal_asistentes"] != DBNull.Value) ? Convert.ToInt32(objRow["todal_asistentes"].ToString()) : 0;
					oEvento.intTotalReservas = (objRow["total_reservas"] != DBNull.Value) ? Convert.ToInt32(objRow["total_reservas"].ToString()) : 0;

					oEvento.oSala.intIdSala = (objRow["id_sala"] != DBNull.Value) ? Convert.ToInt32(objRow["id_sala"].ToString()) : 0;
					oEvento.oSala.strSala = (objRow["sala"] != DBNull.Value) ? objRow["sala"].ToString() : string.Empty;
					oEvento.oSala.strUbicacion = (objRow["ubicacion"] != DBNull.Value) ? objRow["ubicacion"].ToString() : string.Empty;

					oEvento.oUsuarioAnfitrion.intIdUsuario = (objRow["id_usuario"] != DBNull.Value) ? Convert.ToInt32(objRow["id_usuario"].ToString()) : 0;
					oEvento.oUsuarioAnfitrion.strUsuario = (objRow["usuario"] != DBNull.Value) ? objRow["usuario"].ToString() : string.Empty;
					oEvento.oUsuarioAnfitrion.strNombre = (objRow["nombre"] != DBNull.Value) ? objRow["nombre"].ToString() : string.Empty;
					oEvento.oUsuarioAnfitrion.strApellido = (objRow["apellido"] != DBNull.Value) ? objRow["apellido"].ToString() : string.Empty;
					oEvento.oUsuarioAnfitrion.strNumeroDocumento = (objRow["numero_documento"] != DBNull.Value) ? objRow["numero_documento"].ToString() : string.Empty;

					iEvento.Add(oEvento);
				}
			}
			catch(Exception ex)
			{
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
					conn.Close();
				cmd.Dispose();
			}
			return iEvento;
		}
		public int Guardar(Evento obj)
		{
			string strSQL = "Evento_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_sala", obj.oSala.intIdSala);
				cmd.Parameters.AddWithValue("@id_usuario", obj.oUsuarioAnfitrion.intIdUsuario);
				cmd.Parameters.AddWithValue("@evento", obj.strEvento);
				cmd.Parameters.AddWithValue("@fecha_evento", obj.datFechaEvento);
				cmd.Parameters.AddWithValue("@todal_asistentes", obj.intTotalAsistentes);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					r = Convert.ToInt32(objRow["@identity"]);
				}
			}
			catch(Exception ex)
			{
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
					conn.Close();
				cmd.Dispose();
			}
			return r;
		}
		public int Actualizar(Evento obj)
		{
			string strSQL = "Evento_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_evento", obj.intIdEvento);
				cmd.Parameters.AddWithValue("@id_sala", obj.oSala.intIdSala);
				cmd.Parameters.AddWithValue("@id_usuario", obj.oUsuarioAnfitrion.intIdUsuario);
				cmd.Parameters.AddWithValue("@evento", obj.strEvento);
				cmd.Parameters.AddWithValue("@fecha_evento", obj.datFechaEvento);
				cmd.Parameters.AddWithValue("@todal_asistentes", obj.intTotalAsistentes);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					r = Convert.ToInt32(objRow["@rowcount"]);
				}
			}
			catch(Exception ex)
			{
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
					conn.Close();
				cmd.Dispose();
			}
			return r;
		}
		public int Eliminar(Evento obj)
		{
			string strSQL = "Evento_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_evento", obj.intIdEvento);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					r = Convert.ToInt32(objRow["@rowcount"]);
				}
			}
			catch(Exception ex)
			{
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
					conn.Close();
				cmd.Dispose();
			}
			return r;
		}
		private void ObtenerTemperatura()
        {
			var client = new RestClient("https://weatherbit-v1-mashape.p.rapidapi.com/forecast/daily?lang=en&lat=-34.639115&lon=-58.404263");
			var request = new RestRequest(Method.GET);
			request.AddHeader("x-rapidapi-host", "weatherbit-v1-mashape.p.rapidapi.com");
			request.AddHeader("x-rapidapi-key", "a88d29db4cmsh3207ea10e762531p1fe01ejsn3dfad5a29d09");
			IRestResponse response = client.Execute(request);
		}
	}
}