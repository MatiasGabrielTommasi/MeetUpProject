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
		public List<Componente> Cargar(Componente obj)
		{
			string strSQL = "Componente_get";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Componente> iComponente = new List<Componente>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_componente", obj.intIdComponente);
				cmd.Parameters.AddWithValue("@id_componente_padre", obj.intIdComponentePadre);
				cmd.Parameters.AddWithValue("@id_tipo_componente", obj.oTipoComponente.intId);
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

		public int Guardar(Componente obj)
		{
			string strSQL = "Componente_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_componente_padre", obj.intIdComponentePadre);
				cmd.Parameters.AddWithValue("@id_tipo_componente", obj.oTipoComponente.intId);
				cmd.Parameters.AddWithValue("@componente", obj.strComponente);
				cmd.Parameters.AddWithValue("@url_componente", obj.strUrlComponente);
				cmd.Parameters.AddWithValue("@habilitar_componente", Convert.ToInt32(obj.bitHabilitarComponente));
				cmd.Parameters.AddWithValue("@mostrar_componente", Convert.ToInt32(obj.bitMostrarComponente));
				cmd.Parameters.AddWithValue("@detalle_componente", obj.strDetalleComponente);
				cmd.Parameters.AddWithValue("@icono_componente", obj.strIconoComponente);
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

		public int Actualizar(Componente obj)
		{
			string strSQL = "Componente_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_componente", obj.intIdComponente);
				cmd.Parameters.AddWithValue("@id_componente_padre", obj.intIdComponentePadre);
				cmd.Parameters.AddWithValue("@id_tipo_componente", obj.oTipoComponente.intId);
				cmd.Parameters.AddWithValue("@componente", obj.strComponente);
				cmd.Parameters.AddWithValue("@url_componente", obj.strUrlComponente);
				cmd.Parameters.AddWithValue("@habilitar_componente", obj.bitHabilitarComponente);
				cmd.Parameters.AddWithValue("@mostrar_componente", obj.bitMostrarComponente);
				cmd.Parameters.AddWithValue("@detalle_componente", obj.strDetalleComponente);
				cmd.Parameters.AddWithValue("@icono_componente", obj.strIconoComponente);
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

		public int Eliminar(Componente obj)
		{
			string strSQL = "Componente_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
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