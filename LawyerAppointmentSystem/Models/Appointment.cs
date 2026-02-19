namespace LawyerAppointmentSystem.Models;

public class Appointment
{
    public long Id { get; set; }

    // Foreign Keys
    public long LawyerId { get; set; }
    public Lawyer Lawyer { get; set; }
    // Foreign Keys
    public long UserId { get; set; }
    public ApplicationUser User { get; set; }

    public DateTime AppointmentDate { get; set; }

    //public AppointmentStatus Status { get; set; }

    public string ProblemDetails { get; set; }

    // 🔗 Navigation
    //public long PaymentId { get; set; }
    //public Payment Payment { get; set; }
    public ICollection<Payment> Payments { get; set; }
}
