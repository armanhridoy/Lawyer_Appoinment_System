namespace LawyerAppointmentSystem.Models;

public class ApplicationUser
{
        public long Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; }

        //pKey
        public ICollection<Appointment> Appointments { get; set; } // Navigation property for related appointments
        public ICollection<Review> Reviews { get; set; }
}


