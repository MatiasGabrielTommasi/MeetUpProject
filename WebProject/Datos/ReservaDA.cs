using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utilidades;

namespace Datos
{
	public class ReservaDA {
		public List<Reserva> Cargar(Reserva obj)
		{
			string strSQL = "Reserva_get";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			List<Reserva> iReserva = new List<Reserva>();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_reserva", obj.intIdReserva);
				cmd.Parameters.AddWithValue("@id_evento", obj.oEvento.intIdEvento);
				cmd.Parameters.AddWithValue("@id_usuario", obj.oUsuario.intIdUsuario);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					Reserva oReserva = new Reserva();
					oReserva.intIdReserva = (objRow["id_reserva"] != DBNull.Value) ? Convert.ToInt32(objRow["id_reserva"].ToString()) : 0;
					oReserva.datFechaReserva = (objRow["fecha_reserva"] != DBNull.Value) ? Convert.ToDateTime(objRow["fecha_reserva"].ToString()) : new DateTime();
					oReserva.strQr = (objRow["qr"] != DBNull.Value) ? objRow["qr"].ToString() : string.Empty;
					oReserva.decConsumo = (objRow["consumo"] != DBNull.Value) ? Convert.ToDecimal(objRow["consumo"].ToString()) : 0;
					oReserva.bitConsumoFinalizado = (objRow["consumo_finalizado"] != DBNull.Value) ? Convert.ToBoolean(objRow["consumo_finalizado"].ToString()) : false;

					oReserva.oEvento.intIdEvento = (objRow["id_evento"] != DBNull.Value) ? Convert.ToInt32(objRow["id_evento"].ToString()) : 0;
					oReserva.oEvento.strEvento = (objRow["evento"] != DBNull.Value) ? objRow["evento"].ToString() : string.Empty;
					oReserva.oEvento.datFechaEvento = (objRow["fecha_evento"] != DBNull.Value) ? Convert.ToDateTime(objRow["fecha_evento"].ToString()) : new DateTime();
					
					oReserva.oEvento.oSala.intIdSala = (objRow["id_sala"] != DBNull.Value) ? Convert.ToInt32(objRow["id_sala"].ToString()) : 0;
					oReserva.oEvento.oSala.strSala = (objRow["sala"] != DBNull.Value) ? objRow["sala"].ToString() : string.Empty;
					oReserva.oEvento.oSala.strUbicacion = (objRow["ubicacion"] != DBNull.Value) ? objRow["ubicacion"].ToString() : string.Empty;

					oReserva.oUsuario.intIdUsuario = (objRow["id_usuario"] != DBNull.Value) ? Convert.ToInt32(objRow["id_usuario"].ToString()) : 0;
					oReserva.oUsuario.strUsuario = (objRow["usuario"] != DBNull.Value) ? objRow["usuario"].ToString() : string.Empty;
					oReserva.oUsuario.strNombre = (objRow["nombre"] != DBNull.Value) ? objRow["nombre"].ToString() : string.Empty;
					oReserva.oUsuario.strApellido = (objRow["apellido"] != DBNull.Value) ? objRow["apellido"].ToString() : string.Empty;
					oReserva.oUsuario.strNumeroDocumento = (objRow["numero_documento"] != DBNull.Value) ? objRow["numero_documento"].ToString() : string.Empty;

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

		public int Guardar(Reserva obj)
		{
			string strSQL = "Reserva_ins";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
		try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_evento", obj.oEvento.intIdEvento);
				cmd.Parameters.AddWithValue("@fecha_reserva", obj.datFechaReserva);
				cmd.Parameters.AddWithValue("@id_usuario", obj.oUsuario.intIdUsuario);
				cmd.Parameters.AddWithValue("@consumo", obj.decConsumo);
				cmd.Parameters.AddWithValue("@consumo_finalizado", obj.bitConsumoFinalizado);
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				conn.Close();
				cmd.Dispose();
				foreach (DataRow objRow in dt.Rows)
				{
					r = Convert.ToInt32(objRow["@identity"]);
					obj.intIdReserva = r;
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

		public int Eliminar(Reserva obj)
		{
			string strSQL = "Reserva_del";
			int r = 0;
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_reserva", obj.intIdReserva);
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

		private void GuardarQR(Reserva obj)
		{
			string strSQL = "ReservaQR_ins";
			SqlConnection conn = DatabaseConnection.oConn;
			SqlCommand cmd = new SqlCommand(strSQL, conn);
			DataTable dt = new DataTable();
			try
			{
				string strValorQR = string.Format("{0};{1};{2};{3}", obj.intIdReserva, obj.oUsuario.intIdUsuario, obj.oEvento.intIdEvento, obj.oUsuario.strContrasenia);
				string _encryptedQR = Funciones.Encrypt(strValorQR);
				obj.strQr = Funciones.GenerarQR(_encryptedQR);

				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id_reserva", obj.intIdReserva);
				cmd.Parameters.AddWithValue("@qr", obj.strQr);
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