using System;

namespace Entidades
{
	[Serializable]
	public class Reserva 
	{
		public int intIdReserva { get; set; }
		public Evento oEvento { get; set; }
		public DateTime datFechaReserva { get; set; }
		public Usuario oUsuario { get; set; }
		public string strQr { get; set; }
		public decimal decConsumo { get; set; }
		public bool bitConsumoFinalizado { get; set; }
		public Reserva ()
		{
			int _cero = 0;
			string _vacio = string.Empty;
			this.intIdReserva = _cero;
			this.oEvento = new Evento();
			this.datFechaReserva = DateTime.Now;
			this.oUsuario = new Usuario();
			this.strQr = _vacio;
			this.decConsumo = Convert.ToDecimal(_cero);
			this.bitConsumoFinalizado = false;
		}
		public Reserva (int intIdReserva, DateTime datFechaReserva, string strQr, decimal decConsumo, bool bitConsumoFinalizado)
		{
			this.intIdReserva = intIdReserva;
			this.oEvento = new Evento();
			this.datFechaReserva = datFechaReserva;
			this.oUsuario = new Usuario();
			this.strQr = strQr;
			this.decConsumo = decConsumo;
			this.bitConsumoFinalizado = bitConsumoFinalizado;
		}
        public override string ToString()
        {
			return string.Format("({0}) Usuario: {1} - Evento: {2} - Fecha de la reserva: {3}"
				, this.intIdReserva, this.oUsuario.strUsuario, this.oEvento.strEvento, this.datFechaReserva.ToString("dd/MM/yyyy"));
        }
    }
}