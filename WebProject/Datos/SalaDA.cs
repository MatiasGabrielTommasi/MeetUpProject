using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class SalaDA {
		public List<Sala> Cargar(Sala obj)
		{
			string strSQL = "Sala_get";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Sala> iSala = new List<Sala>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_sala", obj.intIdSala);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Sala oSala = new Sala();
					oSala.intIdSala = (objRow["id_sala"] != DBNull.Value) ? Convert.ToInt32(objRow["id_sala"].ToString()) : 0;
					oSala.strSala = (objRow["sala"] != DBNull.Value) ? objRow["sala"].ToString() : string.Empty;
					oSala.intCupo = (objRow["cupo"] != DBNull.Value) ? Convert.ToInt32(objRow["cupo"].ToString()) : 0;
					oSala.strUbicacion = (objRow["ubicacion"] != DBNull.Value) ? objRow["ubicacion"].ToString() : string.Empty;

					oSala.intEventosHoy = (objRow["eventos_hoy"] != DBNull.Value) ? Convert.ToInt32(objRow["eventos_hoy"].ToString()) : 0;
					oSala.intEventosVencidos = (objRow["eventos_vencidos"] != DBNull.Value) ? Convert.ToInt32(objRow["eventos_vencidos"].ToString()) : 0;
					oSala.intEventosVigentes = (objRow["eventos_vigentes"] != DBNull.Value) ? Convert.ToInt32(objRow["eventos_vigentes"].ToString()) : 0;

					iSala.Add(oSala);
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
			return iSala;
		}

		public int Guardar(Sala obj)
		{
			string strSQL = "Sala_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@sala", obj.strSala);
				cmd.Parameters.AddWithValue("@cupo", obj.intCupo);
				cmd.Parameters.AddWithValue("@ubicacion", obj.strUbicacion);
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

		public int Actualizar(Sala obj)
		{
			string strSQL = "Sala_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_sala", obj.intIdSala);
				cmd.Parameters.AddWithValue("@sala", obj.strSala);
				cmd.Parameters.AddWithValue("@cupo", obj.intCupo);
				cmd.Parameters.AddWithValue("@ubicacion", obj.strUbicacion);
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

		public int Eliminar(Sala obj)
		{
			string strSQL = "Sala_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_sala", obj.intIdSala);
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


	}
}