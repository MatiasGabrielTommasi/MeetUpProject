using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class TipoDocumentoDA {
		public List<TipoDocumento> Cargar(TipoDocumento obj)
		{
			string strSQL = "TipoDocumento_get";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<TipoDocumento> iTipoDocumento = new List<TipoDocumento>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_tipo_documento", obj.intId);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					TipoDocumento oTipoDocumento = new TipoDocumento();
					oTipoDocumento.intId = (objRow["id_tipo_documento"] != DBNull.Value) ? Convert.ToInt32(objRow["id_tipo_documento"].ToString()) : 0;
					oTipoDocumento.strDescrip = (objRow["tipo_documento"] != DBNull.Value) ? objRow["tipo_documento"].ToString() : string.Empty;
					iTipoDocumento.Add(oTipoDocumento);
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
			return iTipoDocumento;
		}

		public int Guardar(TipoDocumento obj)
		{
			string strSQL = "TipoDocumento_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
		{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@tipo_documento", obj.strDescrip);
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

		public int Actualizar(TipoDocumento obj)
		{
			string strSQL = "TipoDocumento_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_tipo_documento", obj.intId);
				cmd.Parameters.AddWithValue("@tipo_documento", obj.strDescrip);
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

		public int Eliminar(TipoDocumento obj)
		{
			string strSQL = "TipoDocumento_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_tipo_documento", obj.intId);
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