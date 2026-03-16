namespace HealthAids.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string DoctorSpecialty { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "pending"; // pending, completed, cancelled
        public DateTime CreatedAt { get; set; }
    }
}