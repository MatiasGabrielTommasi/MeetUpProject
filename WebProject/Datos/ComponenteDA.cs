using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace Datos
{
	public class ComponenteDA {
		/// <summary>
		/// Cargar los componentes, enviando un objeto del tipo Componente como parámetro donde se especifican los campos
		/// Id, IdPadre y Tipo.Id. devuelve un listado de componentes, si todos se pasan en cero, carga la totalidad de componentes existentes, si alguno se especifica,
		/// se aplicará el filtro correspondiente al campo indicado
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public List<Componente> Cargar(Componente obj)
		{
			string strSQL = "Componente_get";
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Componente> iComponente = new List<Componente>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_componente", obj.Id);
				cmd.Parameters.AddWithValue("@id_componente_padre", obj.IdPadre);
				cmd.Parameters.AddWithValue("@id_tipo_componente", obj.Tipo.Id);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Componente oComponente = new Componente();
					oComponente.Id = (objRow["id_componente"] != DBNull.Value) ? Convert.ToInt32(objRow["id_componente"].ToString()) : 0;
					oComponente.IdPadre = (objRow["id_componente_padre"] != DBNull.Value) ? Convert.ToInt32(objRow["id_componente_padre"].ToString()) : 0;
					oComponente.Nombre = (objRow["componente"] != DBNull.Value) ? objRow["componente"].ToString() : string.Empty;
					oComponente.Url = (objRow["url_componente"] != DBNull.Value) ? objRow["url_componente"].ToString() : string.Empty;
					oComponente.Habilitado = (objRow["habilitar_componente"] != DBNull.Value) ? Convert.ToBoolean(objRow["habilitar_componente"].ToString()) : false;
					oComponente.Mostrado = (objRow["mostrar_componente"] != DBNull.Value) ? Convert.ToBoolean(objRow["mostrar_componente"].ToString()) : false;
					oComponente.Detalle = (objRow["detalle_componente"] != DBNull.Value) ? objRow["detalle_componente"].ToString() : string.Empty;
					oComponente.Icono = (objRow["icono_componente"] != DBNull.Value) ? objRow["icono_componente"].ToString() : string.Empty;

					oComponente.Tipo.Id = (objRow["id_tipo_componente"] != DBNull.Value) ? Convert.ToInt32(objRow["id_tipo_componente"].ToString()) : 0;
					oComponente.Tipo.Descrip = (objRow["tipo_componente"] != DBNull.Value) ? objRow["tipo_componente"].ToString() : string.Empty;

					iComponente.Add(oComponente);
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
			return iComponente;
		}
		/// <summary>
		/// Guardar, recibe el componente a guardar, y devuelve un numero entero con el id correspondiente a ese registro, 
		/// si el entero es un cero quiere decir que no se pudo insertar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Guardar(Componente obj)
		{
			string strSQL = "Componente_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_componente_padre", obj.IdPadre);
				cmd.Parameters.AddWithValue("@id_tipo_componente", obj.Tipo.Id);
				cmd.Parameters.AddWithValue("@componente", obj.Nombre);
				cmd.Parameters.AddWithValue("@url_componente", obj.Url);
				cmd.Parameters.AddWithValue("@habilitar_componente", Convert.ToInt32(obj.Habilitado));
				cmd.Parameters.AddWithValue("@mostrar_componente", Convert.ToInt32(obj.Mostrado));
				cmd.Parameters.AddWithValue("@detalle_componente", obj.Detalle);
				cmd.Parameters.AddWithValue("@icono_componente", obj.Icono);
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
		/// Actualizar, recibe el componente a actualizar, y devuelve un numero entero con el numero de filas afectadas en la consulta, 
		/// si el entero es un cero quiere decir que no se pudo actualizar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Actualizar(Componente obj)
		{
			string strSQL = "Componente_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_componente", obj.Id);
				cmd.Parameters.AddWithValue("@id_componente_padre", obj.IdPadre);
				cmd.Parameters.AddWithValue("@id_tipo_componente", obj.Tipo.Id);
				cmd.Parameters.AddWithValue("@componente", obj.Nombre);
				cmd.Parameters.AddWithValue("@url_componente", obj.Url);
				cmd.Parameters.AddWithValue("@habilitar_componente", obj.Habilitado);
				cmd.Parameters.AddWithValue("@mostrar_componente", obj.Mostrado);
				cmd.Parameters.AddWithValue("@detalle_componente", obj.Detalle);
				cmd.Parameters.AddWithValue("@icono_componente", obj.Icono);
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
		/// Eliminar, recibe el componente a eliminar, y devuelve un numero entero con el numero de filas afectadas en la consulta, 
		/// si el entero es un cero quiere decir que no se pudo eliminar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Eliminar(Componente obj)
		{
			string strSQL = "Componente_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_componente", obj.Id);
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