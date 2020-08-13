using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class TipoComponenteDA {
		public List<TipoComponente> Cargar(TipoComponente obj)
		{
			string strSQL = "TipoComponente_get";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<TipoComponente> iTipoComponente = new List<TipoComponente>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_tipo_componente", obj.intId);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					TipoComponente oTipoComponente = new TipoComponente();
					oTipoComponente.intId = (objRow["id_tipo_componente"] != DBNull.Value) ? Convert.ToInt32(objRow["id_tipo_componente"].ToString()) : 0;
					oTipoComponente.strDescrip = (objRow["tipo_componente"] != DBNull.Value) ? objRow["tipo_componente"].ToString() : string.Empty;
					iTipoComponente.Add(oTipoComponente);
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
			return iTipoComponente;
		}

		public int Guardar(TipoComponente obj)
		{
			string strSQL = "TipoComponente_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@tipo_componente", obj.strDescrip);
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

		public int Actualizar(TipoComponente obj)
		{
			string strSQL = "TipoComponente_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_tipo_componente", obj.intId);
				cmd.Parameters.AddWithValue("@tipo_componente", obj.strDescrip);
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

		public int Eliminar(TipoComponente obj)
		{
			string strSQL = "TipoComponente_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_tipo_componente", obj.intId);
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