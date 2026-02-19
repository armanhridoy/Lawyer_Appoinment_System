namespace LawyerAppointmentSystem.Models;

public class LawyerAvailability
{
    public long Id { get; set; }

    public long LawyerId { get; set; }
    public Lawyer Lawyer { get; set; }

    public DayOfWeek Day { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

 }
