namespace FlightSchoolV2.Models
{
    public class StudentDocument
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string DocumentType { get; set; } // Medical, Passport, etc.
        public string FilePath { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Status { get; set; } = "Pending";
    }
}