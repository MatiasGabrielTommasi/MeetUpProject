using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class TipoDocumentoDA
	{
		/// <summary>
		/// Cargar los tipos de documento, enviando un objeto del tipo TipoDocumento como parámetro donde se especifican el campo
		/// Id. devuelve un listado de salas, si se pasa en cero, carga la totalidad de tipos de documentos existentes,
		/// si se especifica alguno, se aplicará el filtro correspondiente
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public List<TipoDocumento> Cargar(TipoDocumento obj)
		{
			string strSQL = "TipoDocumento_get";
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<TipoDocumento> iTipoDocumento = new List<TipoDocumento>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_tipo_documento", obj.Id);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					TipoDocumento oTipoDocumento = new TipoDocumento();
					oTipoDocumento.Id = (objRow["id_tipo_documento"] != DBNull.Value) ? Convert.ToInt32(objRow["id_tipo_documento"].ToString()) : 0;
					oTipoDocumento.Descrip = (objRow["tipo_documento"] != DBNull.Value) ? objRow["tipo_documento"].ToString() : string.Empty;
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
		/// <summary>
		/// Guardar, recibe el tipo de documento a guardar, y devuelve un numero entero con el id correspondiente a ese registro, 
		/// si el entero es un cero quiere decir que no se pudo insertar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Guardar(TipoDocumento obj)
		{
			string strSQL = "TipoDocumento_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
		{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@tipo_documento", obj.Descrip);
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
		/// <summary>
		/// Actualizar, recibe el tipo de documento a actualizar, y devuelve un numero entero con el numero de filas afectadas en la consulta, 
		/// si el entero es un cero quiere decir que no se pudo actualizar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Actualizar(TipoDocumento obj)
		{
			string strSQL = "TipoDocumento_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_tipo_documento", obj.Id);
				cmd.Parameters.AddWithValue("@tipo_documento", obj.Descrip);
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
		/// <summary>
		/// Eliminar, recibe el tipo de documento a eliminar, y devuelve un numero entero con el numero de filas afectadas en la consulta, 
		/// si el entero es un cero quiere decir que no se pudo eliminar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Eliminar(TipoDocumento obj)
		{
			string strSQL = "TipoDocumento_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_tipo_documento", obj.Id);
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