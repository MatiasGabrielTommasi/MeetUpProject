using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class UsuarioDA
	{
		/// <summary>
		/// Cargar los usuarios, enviando un objeto del tipo Usuario como parámetro donde se especifican el campo
		/// Id, Username, Nombre y Apellido. devuelve un listado de usuarios, si no se especifican valores, carga la totalidad de usuarios existentes, 
		/// si se especifica alguno, se aplicará el filtro correspondiente
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public List<Usuario> Cargar(Usuario obj)
		{
			string strSQL = "Usuario_get";
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Usuario> iUsuario = new List<Usuario>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", obj.Id);
				cmd.Parameters.AddWithValue("@usuario", obj.Username);
				cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
				cmd.Parameters.AddWithValue("@apellido", obj.Apellido);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Usuario oUsuario = new Usuario();
					oUsuario.Id = (objRow["id_usuario"] != DBNull.Value) ? Convert.ToInt32(objRow["id_usuario"].ToString()) : 0;
					oUsuario.Username = (objRow["usuario"] != DBNull.Value) ? objRow["usuario"].ToString() : string.Empty;
					oUsuario.Contrasenia = (objRow["contrasenia"] != DBNull.Value) ? objRow["contrasenia"].ToString() : string.Empty;
					oUsuario.FechaCreacion = (objRow["fecha_usuario"] != DBNull.Value) ? Convert.ToDateTime(objRow["fecha_usuario"].ToString()) : new DateTime();
					oUsuario.Correo = (objRow["correo_usuario"] != DBNull.Value) ? objRow["correo_usuario"].ToString() : string.Empty;
					oUsuario.Nombre = (objRow["nombre"] != DBNull.Value) ? objRow["nombre"].ToString() : string.Empty;
					oUsuario.Apellido = (objRow["apellido"] != DBNull.Value) ? objRow["apellido"].ToString() : string.Empty;
					oUsuario.NumeroDoc = (objRow["numero_documento"] != DBNull.Value) ? objRow["numero_documento"].ToString() : string.Empty;

					oUsuario.TipoDoc.Id = (objRow["id_tipo_documento"] != DBNull.Value) ? Convert.ToInt32(objRow["id_tipo_documento"].ToString()) : 0;
					oUsuario.TipoDoc.Descrip = (objRow["tipo_documento"] != DBNull.Value) ? objRow["tipo_documento"].ToString() : string.Empty;

					iUsuario.Add(oUsuario);
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
			return iUsuario;
		}
		/// <summary>
		/// Guardar, recibe el usuario a guardar y registra también los perfiles asociados a ese usuario, y devuelve un numero entero con el id correspondiente a ese registro, 
		/// si el entero es un cero quiere decir que no se pudo insertar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Guardar(Usuario obj)
		{
			string strSQL = "Usuario_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@usuario", obj.Username);
				cmd.Parameters.AddWithValue("@contrasenia", obj.Contrasenia);
				cmd.Parameters.AddWithValue("@correo_usuario", obj.Correo);
				cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
				cmd.Parameters.AddWithValue("@apellido", obj.Apellido);
				cmd.Parameters.AddWithValue("@id_tipo_documento", obj.TipoDoc.Id);
				cmd.Parameters.AddWithValue("@numero_documento", obj.NumeroDoc);
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
                    foreach (Perfil perfil in obj.Perfiles)
					{
						GuardarPerfil(perfil, obj.Id);
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
		/// Actualizar, recibe el usuario a actualizar, y devuelve un numero entero con el numero de filas afectadas en la consulta, 
		/// si el entero es un cero quiere decir que no se pudo actualizar el registro. No registra cambios de contraseña
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Actualizar(Usuario obj)
		{
			string strSQL = "Usuario_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", obj.Id);
				cmd.Parameters.AddWithValue("@usuario", obj.Username);
				cmd.Parameters.AddWithValue("@correo_usuario", obj.Correo);
				cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
				cmd.Parameters.AddWithValue("@apellido", obj.Apellido);
				cmd.Parameters.AddWithValue("@id_tipo_documento", obj.TipoDoc.Id);
				cmd.Parameters.AddWithValue("@numero_documento", obj.NumeroDoc);
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
					EliminarPerfiles(obj.Id);
					foreach (Perfil perfil in obj.Perfiles)
					{
						GuardarPerfil(perfil, obj.Id);
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
		/// Eliminar, recibe el usuario a eliminar, y devuelve un numero entero con el numero de filas afectadas en la consulta, 
		/// si el entero es un cero quiere decir que no se pudo eliminar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Eliminar(Usuario obj)
		{
			string strSQL = "Usuario_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", obj.Id);
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
		/// Método para realizar login en el sistema, devuelve un usuario en caso de encontrarlo, por el contrario si no se encuentra el usuario indicado devolvera null
		/// </summary>
		/// <param name="Usuario"></param>
		/// <returns></returns>
		public Usuario Login(string Usuario)
		{
			string strSQL = "UsuarioLogin_get";
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			Usuario obj = null;
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@usuario", Usuario);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					obj = new Usuario();
					obj.Id = (objRow["id_usuario"] != DBNull.Value) ? Convert.ToInt32(objRow["id_usuario"].ToString()) : 0;
					obj.Username = (objRow["usuario"] != DBNull.Value) ? objRow["usuario"].ToString() : string.Empty;
					obj.Contrasenia = (objRow["contrasenia"] != DBNull.Value) ? objRow["contrasenia"].ToString() : string.Empty;
					obj.FechaCreacion = (objRow["fecha_usuario"] != DBNull.Value) ? Convert.ToDateTime(objRow["fecha_usuario"].ToString()) : new DateTime();
					obj.Correo = (objRow["correo_usuario"] != DBNull.Value) ? objRow["correo_usuario"].ToString() : string.Empty;
					obj.Nombre = (objRow["nombre"] != DBNull.Value) ? objRow["nombre"].ToString() : string.Empty;
					obj.Apellido = (objRow["apellido"] != DBNull.Value) ? objRow["apellido"].ToString() : string.Empty;
					obj.NumeroDoc = (objRow["numero_documento"] != DBNull.Value) ? objRow["numero_documento"].ToString() : string.Empty;

					obj.TipoDoc.Id = (objRow["id_tipo_documento"] != DBNull.Value) ? Convert.ToInt32(objRow["id_tipo_documento"].ToString()) : 0;
					obj.TipoDoc.Descrip = (objRow["tipo_documento"] != DBNull.Value) ? objRow["tipo_documento"].ToString() : string.Empty;

					obj.Perfiles = CargarPerfiles(obj.Id);
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
			return obj;
		}
		/// <summary>
		/// Reestablece la contraseña del usuario a la indicada en el objeto que se pasa como parámetro,
		/// devuelve un entero con el numero de filas afectadas. Si devuelve cero es porque no se pudo registrar la operación
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
        public int ReestablecerContrasenia(Usuario obj)
		{
			string strSQL = "UsuarioContrasenia_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", obj.Id);
				cmd.Parameters.AddWithValue("@contrasenia", obj.Contrasenia);
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
		/// Devuelve un listado con los perfiles del usuario indicado
		/// </summary>
		/// <param name="IdUsuario"></param>
		/// <returns></returns>
        public List<Perfil> CargarPerfiles(int IdUsuario)
		{
			string strSQL = "PerfilUsuario_get";
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Perfil> iPerfil = new List<Perfil>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", IdUsuario);
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

					PerfilDA objDA = new PerfilDA();
					oPerfil.Componentes = objDA.CargarComponentes(oPerfil.Id);

					iPerfil.Add(oPerfil);
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
			return iPerfil;
		}
		/// <summary>
		/// Guarda el perfil indicado al usuario que se pasa como parámetro
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="IdUsuario"></param>
		/// <returns></returns>
		public int GuardarPerfil(Perfil obj, int IdUsuario)
		{
			string strSQL = "PerfilUsuario_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", obj.Id);
				cmd.Parameters.AddWithValue("@id_usuario", IdUsuario);
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
		//Elimina la asignación de los perfiles al usuario indicado como parámetro
		public int EliminarPerfiles(int IdUsuario)
		{
			string strSQL = "PerfilUsuario_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", IdUsuario);
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