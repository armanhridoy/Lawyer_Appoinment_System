namespace LawyerAppointmentSystem.Models;

public class Lawyer
{
    public long Id { get; set; }

    public string FullName { get; set; }
    public string Specialization { get; set; }
    public int ExperienceYear { get; set; }
    public decimal ConsultationFee { get; set; }

    public bool IsActive { get; set; }
    public string? PhoneNumber { get; set; }
    public string ImageUrl { get; set; }
    public string Address { get; set; }

    // 🔗 Navigation
    public ICollection<LawyerAvailability> Availabilities { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Review> Reviews { get; set; }
}
