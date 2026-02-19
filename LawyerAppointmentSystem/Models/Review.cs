namespace LawyerAppointmentSystem.Models;

public class Review
{
    public long Id { get; set; }
    public long LawyerId { get; set; }
    public Lawyer Lawyer { get; set; }
    public long UserId { get; set; }
    public ApplicationUser User { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
