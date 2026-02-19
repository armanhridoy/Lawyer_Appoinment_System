namespace LawyerAppointmentSystem.Models;

public class Lawyer
{
    public long Id { get; set; }

    public string FullName { get; set; }
    public string Specialization { get; set; }
    public int ExperienceYear { get; set; }
    public decimal ConsultationFee { get; set; }

    public bool IsActive { get; set; }

    // 🔗 Navigation
    public ICollection<LawyerAvailability> Availabilities { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Review> Reviews { get; set; }
}
