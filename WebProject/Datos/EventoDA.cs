using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class EventoDA
	{
		/// <summary>
		/// Cargar los eventos, enviando un objeto del tipo Evento como parámetro donde se especifican los campos
		/// Id, Salon.Id y UsuarioAnfitrion.Id. devuelve un listado de eventos, si todos se pasan en cero, carga la totalidad de eventos existentes,
		/// si alguno se especifica se aplicará el filtro correspondiente al campo indicado
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public List<Evento> Cargar(Evento obj)
		{
			string strSQL = "Evento_get";
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Evento> iEvento = new List<Evento>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_evento", obj.Id);
				cmd.Parameters.AddWithValue("@id_sala", obj.Salon.Id);
				cmd.Parameters.AddWithValue("@id_usuario", obj.UsuarioAnfitrion.Id);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Evento oEvento = new Evento();
					oEvento.Id = (objRow["id_evento"] != DBNull.Value) ? Convert.ToInt32(objRow["id_evento"].ToString()) : 0;
					oEvento.Nombre = (objRow["evento"] != DBNull.Value) ? objRow["evento"].ToString() : string.Empty;
					oEvento.DiaSeleccionado.Fecha = (objRow["fecha_evento"] != DBNull.Value) ? Convert.ToDateTime(objRow["fecha_evento"].ToString()) : new DateTime();
					oEvento.TotalAsistentes = (objRow["todal_asistentes"] != DBNull.Value) ? Convert.ToInt32(objRow["todal_asistentes"].ToString()) : 0;
					oEvento.TotalReservas = (objRow["total_reservas"] != DBNull.Value) ? Convert.ToInt32(objRow["total_reservas"].ToString()) : 0;

					oEvento.Salon.Id = (objRow["id_sala"] != DBNull.Value) ? Convert.ToInt32(objRow["id_sala"].ToString()) : 0;
					oEvento.Salon.Nombre = (objRow["sala"] != DBNull.Value) ? objRow["sala"].ToString() : string.Empty;
					oEvento.Salon.Ubicacion = (objRow["ubicacion"] != DBNull.Value) ? objRow["ubicacion"].ToString() : string.Empty;

					oEvento.UsuarioAnfitrion.Id = (objRow["id_usuario"] != DBNull.Value) ? Convert.ToInt32(objRow["id_usuario"].ToString()) : 0;
					oEvento.UsuarioAnfitrion.Username = (objRow["usuario"] != DBNull.Value) ? objRow["usuario"].ToString() : string.Empty;
					oEvento.UsuarioAnfitrion.Nombre = (objRow["nombre"] != DBNull.Value) ? objRow["nombre"].ToString() : string.Empty;
					oEvento.UsuarioAnfitrion.Apellido = (objRow["apellido"] != DBNull.Value) ? objRow["apellido"].ToString() : string.Empty;
					oEvento.UsuarioAnfitrion.NumeroDoc = (objRow["numero_documento"] != DBNull.Value) ? objRow["numero_documento"].ToString() : string.Empty;

					iEvento.Add(oEvento);
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
			return iEvento;
		}
		/// <summary>
		/// Guardar, recibe el evento a guardar, y devuelve un numero entero con el id correspondiente a ese registro, 
		/// si el entero es un cero quiere decir que no se pudo insertar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Guardar(Evento obj)
		{
			string strSQL = "Evento_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_sala", obj.Salon.Id);
				cmd.Parameters.AddWithValue("@id_usuario", obj.UsuarioAnfitrion.Id);
				cmd.Parameters.AddWithValue("@evento", obj.Nombre);
				cmd.Parameters.AddWithValue("@fecha_evento", obj.DiaSeleccionado.Fecha);
				cmd.Parameters.AddWithValue("@todal_asistentes", obj.TotalAsistentes);
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
		public int Actualizar(Evento obj)
		{
			string strSQL = "Evento_upt";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_evento", obj.Id);
				cmd.Parameters.AddWithValue("@id_sala", obj.Salon.Id);
				cmd.Parameters.AddWithValue("@id_usuario", obj.UsuarioAnfitrion.Id);
				cmd.Parameters.AddWithValue("@evento", obj.Nombre);
				cmd.Parameters.AddWithValue("@fecha_evento", obj.DiaSeleccionado.Fecha);
				cmd.Parameters.AddWithValue("@todal_asistentes", obj.TotalAsistentes);
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
		public int Eliminar(Evento obj)
		{
			string strSQL = "Evento_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_evento", obj.Id);
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