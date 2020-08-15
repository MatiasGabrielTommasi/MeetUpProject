using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class SalaDA
	{
		/// <summary>
		/// Cargar las salas, enviando un objeto del tipo Sala como parámetro donde se especifican el campo
		/// Id. devuelve un listado de salas, si se pasa en cero, carga la totalidad de salas existentes,
		/// si se especifica alguno, se aplicará el filtro correspondiente
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public List<Sala> Cargar(Sala obj)
		{
			string strSQL = "Sala_get";
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Sala> iSala = new List<Sala>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_sala", obj.Id);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Sala oSala = new Sala();
					oSala.Id = (objRow["id_sala"] != DBNull.Value) ? Convert.ToInt32(objRow["id_sala"].ToString()) : 0;
					oSala.Nombre = (objRow["sala"] != DBNull.Value) ? objRow["sala"].ToString() : string.Empty;
					oSala.Cupo = (objRow["cupo"] != DBNull.Value) ? Convert.ToInt32(objRow["cupo"].ToString()) : 0;
					oSala.Ubicacion = (objRow["ubicacion"] != DBNull.Value) ? objRow["ubicacion"].ToString() : string.Empty;

					oSala.EventosHoy = (objRow["eventos_hoy"] != DBNull.Value) ? Convert.ToInt32(objRow["eventos_hoy"].ToString()) : 0;
					oSala.EventosVencidos = (objRow["eventos_vencidos"] != DBNull.Value) ? Convert.ToInt32(objRow["eventos_vencidos"].ToString()) : 0;
					oSala.EventosVigentes = (objRow["eventos_vigentes"] != DBNull.Value) ? Convert.ToInt32(objRow["eventos_vigentes"].ToString()) : 0;

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
		/// <summary>
		/// Guardar, recibe la sala a guardar, y devuelve un numero entero con el id correspondiente a ese registro, 
		/// si el entero es un cero quiere decir que no se pudo insertar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Guardar(Sala obj)
		{
			string strSQL = "Sala_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@sala", obj.Nombre);
				cmd.Parameters.AddWithValue("@cupo", obj.Cupo);
				cmd.Parameters.AddWithValue("@ubicacion", obj.Ubicacion);
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
		/// Actualizar, recibe la sala a actualizar, y devuelve un numero entero con el numero de filas afectadas en la consulta, 
		/// si el entero es un cero quiere decir que no se pudo actualizar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Actualizar(Sala obj)
		{
			string strSQL = "Sala_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_sala", obj.Id);
				cmd.Parameters.AddWithValue("@sala", obj.Nombre);
				cmd.Parameters.AddWithValue("@cupo", obj.Cupo);
				cmd.Parameters.AddWithValue("@ubicacion", obj.Ubicacion);
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
		/// Eliminar, recibe el sala a eliminar, y devuelve un numero entero con el numero de filas afectadas en la consulta, 
		/// si el entero es un cero quiere decir que no se pudo eliminar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Eliminar(Sala obj)
		{
			string strSQL = "Sala_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_sala", obj.Id);
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