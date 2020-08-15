using System;

namespace Entidades
{
	[Serializable]
	public class Reserva 
	{
		public int Id { get; set; }
		public Evento EventoSeleccionado { get; set; }
		public DateTime Fecha { get; set; }
		public Usuario Asistente { get; set; }
		public string Qr { get; set; }
		public decimal Consumo { get; set; }
		public bool ConsumeTotal { get; set; }
		public Reserva ()
		{
			this.Id = 0;
			this.EventoSeleccionado = new Evento();
			this.Fecha = DateTime.Now;
			this.Asistente = new Usuario();
			this.Qr = string.Empty;
			this.Consumo = Convert.ToDecimal(0);
			this.ConsumeTotal = false;
		}
		public Reserva (int Id, DateTime Fecha, string QR, decimal Consumo, bool ConsumeTotal)
		{
			this.Id = Id;
			this.EventoSeleccionado = new Evento();
			this.Fecha = Fecha;
			this.Asistente = new Usuario();
			this.Qr = QR;
			this.Consumo = Consumo;
			this.ConsumeTotal = ConsumeTotal;
		}
        public override string ToString()
        {
			return string.Format("({0}) Usuario: {1} - Evento: {2} - Fecha de la reserva: {3}"
				, this.Id, this.Asistente.Username, this.EventoSeleccionado.Nombre, this.Fecha.ToString("dd/MM/yyyy"));
        }
    }
}