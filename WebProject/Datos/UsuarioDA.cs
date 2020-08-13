using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class UsuarioDA {
		public List<Usuario> Cargar(Usuario obj)
		{
			string strSQL = "Usuario_get";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Usuario> iUsuario = new List<Usuario>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", obj.intIdUsuario);
				cmd.Parameters.AddWithValue("@usuario", obj.strUsuario);
				cmd.Parameters.AddWithValue("@nombre", obj.strNombre);
				cmd.Parameters.AddWithValue("@apellido", obj.strApellido);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Usuario oUsuario = new Usuario();
					oUsuario.intIdUsuario = (objRow["id_usuario"] != DBNull.Value) ? Convert.ToInt32(objRow["id_usuario"].ToString()) : 0;
					oUsuario.strUsuario = (objRow["usuario"] != DBNull.Value) ? objRow["usuario"].ToString() : string.Empty;
					oUsuario.strContrasenia = (objRow["contrasenia"] != DBNull.Value) ? objRow["contrasenia"].ToString() : string.Empty;
					oUsuario.datFechaUsuario = (objRow["fecha_usuario"] != DBNull.Value) ? Convert.ToDateTime(objRow["fecha_usuario"].ToString()) : new DateTime();
					oUsuario.strCorreoUsuario = (objRow["correo_usuario"] != DBNull.Value) ? objRow["correo_usuario"].ToString() : string.Empty;
					oUsuario.strNombre = (objRow["nombre"] != DBNull.Value) ? objRow["nombre"].ToString() : string.Empty;
					oUsuario.strApellido = (objRow["apellido"] != DBNull.Value) ? objRow["apellido"].ToString() : string.Empty;
					oUsuario.strNumeroDocumento = (objRow["numero_documento"] != DBNull.Value) ? objRow["numero_documento"].ToString() : string.Empty;

					oUsuario.oTipoDocumento.intId = (objRow["id_tipo_documento"] != DBNull.Value) ? Convert.ToInt32(objRow["id_tipo_documento"].ToString()) : 0;
					oUsuario.oTipoDocumento.strDescrip = (objRow["tipo_documento"] != DBNull.Value) ? objRow["tipo_documento"].ToString() : string.Empty;

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
		public int Guardar(Usuario obj)
		{
			string strSQL = "Usuario_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@usuario", obj.strUsuario);
				cmd.Parameters.AddWithValue("@contrasenia", obj.strContrasenia);
				cmd.Parameters.AddWithValue("@correo_usuario", obj.strCorreoUsuario);
				cmd.Parameters.AddWithValue("@nombre", obj.strNombre);
				cmd.Parameters.AddWithValue("@apellido", obj.strApellido);
				cmd.Parameters.AddWithValue("@id_tipo_documento", obj.oTipoDocumento.intId);
				cmd.Parameters.AddWithValue("@numero_documento", obj.strNumeroDocumento);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					r = Convert.ToInt32(objRow["@identity"]);
					obj.intIdUsuario = r;
				}

                if (r > 0)
                {
                    foreach (Perfil perfil in obj.iPerfiles)
					{
						GuardarPerfil(perfil, obj.intIdUsuario);
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
		public int Actualizar(Usuario obj)
		{
			string strSQL = "Usuario_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", obj.intIdUsuario);
				cmd.Parameters.AddWithValue("@usuario", obj.strUsuario);
				cmd.Parameters.AddWithValue("@correo_usuario", obj.strCorreoUsuario);
				cmd.Parameters.AddWithValue("@nombre", obj.strNombre);
				cmd.Parameters.AddWithValue("@apellido", obj.strApellido);
				cmd.Parameters.AddWithValue("@id_tipo_documento", obj.oTipoDocumento.intId);
				cmd.Parameters.AddWithValue("@numero_documento", obj.strNumeroDocumento);
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
					EliminarPerfiles(obj.intIdUsuario);
					foreach (Perfil perfil in obj.iPerfiles)
					{
						GuardarPerfil(perfil, obj.intIdUsuario);
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
		public int Eliminar(Usuario obj)
		{
			string strSQL = "Usuario_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", obj.intIdUsuario);
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
		public Usuario Login(string strUsuario)
		{
			string strSQL = "UsuarioLogin_get";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			Usuario obj = null;
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@usuario", strUsuario);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					obj = new Usuario();
					obj.intIdUsuario = (objRow["id_usuario"] != DBNull.Value) ? Convert.ToInt32(objRow["id_usuario"].ToString()) : 0;
					obj.strUsuario = (objRow["usuario"] != DBNull.Value) ? objRow["usuario"].ToString() : string.Empty;
					obj.strContrasenia = (objRow["contrasenia"] != DBNull.Value) ? objRow["contrasenia"].ToString() : string.Empty;
					obj.datFechaUsuario = (objRow["fecha_usuario"] != DBNull.Value) ? Convert.ToDateTime(objRow["fecha_usuario"].ToString()) : new DateTime();
					obj.strCorreoUsuario = (objRow["correo_usuario"] != DBNull.Value) ? objRow["correo_usuario"].ToString() : string.Empty;
					obj.strNombre = (objRow["nombre"] != DBNull.Value) ? objRow["nombre"].ToString() : string.Empty;
					obj.strApellido = (objRow["apellido"] != DBNull.Value) ? objRow["apellido"].ToString() : string.Empty;
					obj.strNumeroDocumento = (objRow["numero_documento"] != DBNull.Value) ? objRow["numero_documento"].ToString() : string.Empty;

					obj.oTipoDocumento.intId = (objRow["id_tipo_documento"] != DBNull.Value) ? Convert.ToInt32(objRow["id_tipo_documento"].ToString()) : 0;
					obj.oTipoDocumento.strDescrip = (objRow["tipo_documento"] != DBNull.Value) ? objRow["tipo_documento"].ToString() : string.Empty;

					obj.iPerfiles = CargarPerfiles(obj.intIdUsuario);
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
        public int ReestablecerContrasenia(Usuario obj)
		{
			string strSQL = "UsuarioContrasenia_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", obj.intIdUsuario);
				cmd.Parameters.AddWithValue("@contrasenia", obj.strContrasenia);
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

        public List<Perfil> CargarPerfiles(int intIdUsuario)
		{
			string strSQL = "PerfilUsuario_get";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Perfil> iPerfil = new List<Perfil>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", intIdUsuario);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Perfil oPerfil = new Perfil();
					oPerfil.intId = (objRow["id_perfil"] != DBNull.Value) ? Convert.ToInt32(objRow["id_perfil"].ToString()) : 0;
					oPerfil.strDescrip = (objRow["perfil"] != DBNull.Value) ? objRow["perfil"].ToString() : string.Empty;

					PerfilDA objDA = new PerfilDA();
					oPerfil.iComponentes = objDA.CargarComponentes(oPerfil.intId);

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
		public int GuardarPerfil(Perfil obj, int intIdUsuario)
		{
			string strSQL = "PerfilUsuario_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", obj.intId);
				cmd.Parameters.AddWithValue("@id_usuario", intIdUsuario);
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
		public int EliminarPerfiles(int intIdUsuario)
		{
			string strSQL = "PerfilUsuario_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_usuario", intIdUsuario);
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