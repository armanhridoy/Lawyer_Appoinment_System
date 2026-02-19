namespace LawyerAppointmentSystem.Models;

public class Payment
{
        public long Id { get; set; }

        public long AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus  { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;

}
