using System.ComponentModel.DataAnnotations;

namespace FlightSchoolV2.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public DateTime DOB { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string IdPassportVisaNumber { get; set; }

        public string SelectedPackage { get; set; } // The course they choose
        public string ApplicationStatus { get; set; } = "Pending";
        public string RejectionReason { get; set; }

        public virtual ICollection<StudentDocument> Documents { get; set; }
    }
}