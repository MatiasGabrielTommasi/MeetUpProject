using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utilidades;

namespace Datos
{
	public class ReservaDA
	{
		/// <summary>
		/// Cargar las reservas realizadas, enviando un objeto del tipo Reserva como parámetro donde se especifican el campo
		/// Id, EventoSeleccionado.Id y Asistente.Id. devuelve un listado de reservas, si se pasa en cero, carga la totalidad de reservas existentes,
		/// si se especifica alguno, se aplicará el filtro correspondiente
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public List<Reserva> Cargar(Reserva obj)
		{
			string strSQL = "Reserva_get";
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Reserva> iReserva = new List<Reserva>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_reserva", obj.Id);
				cmd.Parameters.AddWithValue("@id_evento", obj.EventoSeleccionado.Id);
				cmd.Parameters.AddWithValue("@id_usuario", obj.Asistente.Id);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Reserva oReserva = new Reserva();
					oReserva.Id = (objRow["id_reserva"] != DBNull.Value) ? Convert.ToInt32(objRow["id_reserva"].ToString()) : 0;
					oReserva.Fecha = (objRow["fecha_reserva"] != DBNull.Value) ? Convert.ToDateTime(objRow["fecha_reserva"].ToString()) : new DateTime();
					oReserva.Qr = (objRow["qr"] != DBNull.Value) ? objRow["qr"].ToString() : string.Empty;
					oReserva.Consumo = (objRow["consumo"] != DBNull.Value) ? Convert.ToDecimal(objRow["consumo"].ToString()) : 0;
					oReserva.ConsumeTotal = (objRow["consumo_finalizado"] != DBNull.Value) ? Convert.ToBoolean(objRow["consumo_finalizado"].ToString()) : false;

					oReserva.EventoSeleccionado.Id = (objRow["id_evento"] != DBNull.Value) ? Convert.ToInt32(objRow["id_evento"].ToString()) : 0;
					oReserva.EventoSeleccionado.Nombre = (objRow["evento"] != DBNull.Value) ? objRow["evento"].ToString() : string.Empty;
					oReserva.EventoSeleccionado.DiaSeleccionado.Fecha = (objRow["fecha_evento"] != DBNull.Value) ? Convert.ToDateTime(objRow["fecha_evento"].ToString()) : new DateTime();

					oReserva.EventoSeleccionado.UsuarioAnfitrion.Apellido = (objRow["apellido_anfitrion"] != DBNull.Value) ? objRow["apellido_anfitrion"].ToString() : string.Empty;
					oReserva.EventoSeleccionado.UsuarioAnfitrion.Nombre = (objRow["nombre_anfitrion"] != DBNull.Value) ? objRow["nombre_anfitrion"].ToString() : string.Empty;

					oReserva.EventoSeleccionado.Salon.Id = (objRow["id_sala"] != DBNull.Value) ? Convert.ToInt32(objRow["id_sala"].ToString()) : 0;
					oReserva.EventoSeleccionado.Salon.Nombre = (objRow["sala"] != DBNull.Value) ? objRow["sala"].ToString() : string.Empty;
					oReserva.EventoSeleccionado.Salon.Ubicacion = (objRow["ubicacion"] != DBNull.Value) ? objRow["ubicacion"].ToString() : string.Empty;

					oReserva.Asistente.Id = (objRow["id_usuario"] != DBNull.Value) ? Convert.ToInt32(objRow["id_usuario"].ToString()) : 0;
					oReserva.Asistente.Username = (objRow["usuario"] != DBNull.Value) ? objRow["usuario"].ToString() : string.Empty;
					oReserva.Asistente.Nombre = (objRow["nombre"] != DBNull.Value) ? objRow["nombre"].ToString() : string.Empty;
					oReserva.Asistente.Apellido = (objRow["apellido"] != DBNull.Value) ? objRow["apellido"].ToString() : string.Empty;
					oReserva.Asistente.NumeroDoc = (objRow["numero_documento"] != DBNull.Value) ? objRow["numero_documento"].ToString() : string.Empty;

					iReserva.Add(oReserva);
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
			return iReserva;
		}
		/// <summary>
		/// Guardar, recibe la reserva a guardar, y devuelve un numero entero con el id correspondiente a ese registro, 
		/// si el entero es un cero quiere decir que no se pudo insertar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Guardar(Reserva obj)
		{
			string strSQL = "Reserva_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_evento", obj.EventoSeleccionado.Id);
				cmd.Parameters.AddWithValue("@fecha_reserva", obj.Fecha);
				cmd.Parameters.AddWithValue("@id_usuario", obj.Asistente.Id);
				cmd.Parameters.AddWithValue("@consumo", obj.Consumo);
				cmd.Parameters.AddWithValue("@consumo_finalizado", obj.ConsumeTotal);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					r = Convert.ToInt32(objRow["@identity"]);
					obj.Id = r;
					if (r > 0)
						GuardarQR(obj);
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
		/// Eliminar, recibe el reserva a eliminar, y devuelve un numero entero con el numero de filas afectadas en la consulta, 
		/// si el entero es un cero quiere decir que no se pudo eliminar el registro
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Eliminar(Reserva obj)
		{
			string strSQL = "Reserva_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_reserva", obj.Id);
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
		/// genera el QR de la reserva realizada y lo guarda en la base de datos en formato Base64String que contiene informacion de la reserva encriptada
		/// </summary>
		/// <param name="obj"></param>
		private void GuardarQR(Reserva obj)
		{
			string strSQL = "ReservaQR_ins";
			SqlConnection conn = DatabaseConnection.ConexionBase;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				string strValorQR = string.Format("{0};{1};{2};{3}", obj.Id, obj.Asistente.Id, obj.EventoSeleccionado.Id, obj.Asistente.Contrasenia);
				string _encryptedQR = Funciones.Encrypt(strValorQR);
				obj.Qr = Funciones.GenerarQR(_encryptedQR);

				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_reserva", obj.Id);
				cmd.Parameters.AddWithValue("@qr", obj.Qr);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
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
		}
	}
}