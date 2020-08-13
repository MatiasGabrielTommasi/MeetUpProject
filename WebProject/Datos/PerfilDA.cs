using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class PerfilDA {
		public List<Perfil> Cargar(Perfil obj)
		{
			string strSQL = "Perfil_get";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Perfil> iPerfil = new List<Perfil>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", obj.intId);
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

		public int Guardar(Perfil obj)
		{
			string strSQL = "Perfil_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
		{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@perfil", obj.strDescrip);
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

		public int Actualizar(Perfil obj)
		{
			string strSQL = "Perfil_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", obj.intId);
				cmd.Parameters.AddWithValue("@perfil", obj.strDescrip);
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

		public int Eliminar(Perfil obj)
		{
			string strSQL = "Perfil_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", obj.intId);
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
		public List<Componente> CargarComponentes(int intIdPerfil)
		{
			string strSQL = "ComponentePerfil_get";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Componente> iComponente = new List<Componente>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", intIdPerfil);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Componente oComponente = new Componente();
					oComponente.intIdComponente = (objRow["id_componente"] != DBNull.Value) ? Convert.ToInt32(objRow["id_componente"].ToString()) : 0;
					oComponente.intIdComponentePadre = (objRow["id_componente_padre"] != DBNull.Value) ? Convert.ToInt32(objRow["id_componente_padre"].ToString()) : 0;
					oComponente.strComponente = (objRow["componente"] != DBNull.Value) ? objRow["componente"].ToString() : string.Empty;
					oComponente.strUrlComponente = (objRow["url_componente"] != DBNull.Value) ? objRow["url_componente"].ToString() : string.Empty;
					oComponente.bitHabilitarComponente = (objRow["habilitar_componente"] != DBNull.Value) ? Convert.ToBoolean(objRow["habilitar_componente"].ToString()) : false;
					oComponente.bitMostrarComponente = (objRow["mostrar_componente"] != DBNull.Value) ? Convert.ToBoolean(objRow["mostrar_componente"].ToString()) : false;
					oComponente.strDetalleComponente = (objRow["detalle_componente"] != DBNull.Value) ? objRow["detalle_componente"].ToString() : string.Empty;
					oComponente.strIconoComponente = (objRow["icono_componente"] != DBNull.Value) ? objRow["icono_componente"].ToString() : string.Empty;

					oComponente.oTipoComponente.intId = (objRow["id_tipo_componente"] != DBNull.Value) ? Convert.ToInt32(objRow["id_tipo_componente"].ToString()) : 0;
					oComponente.oTipoComponente.strDescrip = (objRow["tipo_componente"] != DBNull.Value) ? objRow["tipo_componente"].ToString() : string.Empty;

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
		public int GuardarComponente(Componente obj, int intIdPerfil)
		{
			string strSQL = "ComponentePerfil_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", intIdPerfil);
				cmd.Parameters.AddWithValue("@id_componente", obj.intIdComponente);
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
		public int EliminarComponentes(int intIdPerfil)
		{
			string strSQL = "ComponentePerfil_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_perfil", intIdPerfil);
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