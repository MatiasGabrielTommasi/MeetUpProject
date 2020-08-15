using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class PerfilDA
	{
		/// <summary>
		/// Cargar los perfiles, enviando un objeto del tipo Perfil como parámetro donde se especifican el campo
		/// Id. devuelve un listado de perfiles, si se pasa en cero, carga la totalidad de perfiles existentes, si se especifica alguno,
		/// se aplicará el filtro correspondiente
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public List<Perfil> Cargar(Perfil obj)
		{
			string strSQL = "Perfil_get";
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Perfil> iPerfil = new List<Perfil>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", obj.Id);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Perfil oPerfil = new Perfil();
					oPerfil.Id = (objRow["id_perfil"] != DBNull.Value) ? Convert.ToInt32(objRow["id_perfil"].ToString()) : 0;
					oPerfil.Descrip = (objRow["perfil"] != DBNull.Value) ? objRow["perfil"].ToString() : string.Empty;
					oPerfil.Componentes = CargarComponentes(oPerfil.Id);
					iPerfil.Add(oPerfil);
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
			return iPerfil;
		}
		/// <summary>
		/// Guardar, recibe el perfil a guardar, y devuelve un numero entero con el id correspondiente a ese registro, 
		/// si el entero es un cero quiere decir que no se pudo insertar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Guardar(Perfil obj)
		{
			string strSQL = "Perfil_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
		{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@perfil", obj.Descrip);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					r = Convert.ToInt32(objRow["@identity"]);
					obj.Id = r;
				}
                if (r > 0)
                {
                    foreach (Componente componente in obj.Componentes)
                    {
						GuardarComponente(componente, obj.Id);
                    }
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
		/// Actualizar, recibe el perfil a actualizar, y devuelve un numero entero con el numero de filas afectadas en la consulta, 
		/// si el entero es un cero quiere decir que no se pudo actualizar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Actualizar(Perfil obj)
		{
			string strSQL = "Perfil_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", obj.Id);
				cmd.Parameters.AddWithValue("@perfil", obj.Descrip);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					r = Convert.ToInt32(objRow["@rowcount"]);
				}
                if (r > 0)
                {
					EliminarComponentes(obj.Id);
                    foreach (Componente componente in obj.Componentes)
                    {
						GuardarComponente(componente, obj.Id);
                    }
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
		/// Eliminar, recibe el perfil a eliminar, y devuelve un numero entero con el numero de filas afectadas en la consulta, 
		/// si el entero es un cero quiere decir que no se pudo eliminar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Eliminar(Perfil obj)
		{
			string strSQL = "Perfil_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", obj.Id);
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
		/// Devuelve un listado con los componentes correspondientes al perfil indicado en el campo IdPerfil, si se pasa cero no se cargara ninguno
		/// </summary>
		/// <param name="IdPerfil"></param>
		/// <returns></returns>
		public List<Componente> CargarComponentes(int IdPerfil)
		{
			string strSQL = "ComponentePerfil_get";
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Componente> iComponente = new List<Componente>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", IdPerfil);
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
			catch (Exception ex)
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
		/// Guarda el componente pasado como parámetro al perfil indicado, devuelve la cantidad de filas afectadas en la consulta.
		/// Si devuelve cero quiere decir que no se pudo insertar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="IdPerfil"></param>
		/// <returns></returns>
		public int GuardarComponente(Componente obj, int IdPerfil)
		{
			string strSQL = "ComponentePerfil_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", IdPerfil);
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
			catch (Exception ex)
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
		/// Elimina la totalidad de los componentes relacionados al perfil que se pasa como parámetro, devuelve la cantidad de filas afectadas en la consulta.
		/// Si devuelve cero quiere decir que no se pudo insertar el registro
		/// </summary>
		/// <param name="IdPerfil"></param>
		/// <returns></returns>
		public int EliminarComponentes(int IdPerfil)
		{
			string strSQL = "ComponentePerfil_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", IdPerfil);
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
			catch (Exception ex)
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