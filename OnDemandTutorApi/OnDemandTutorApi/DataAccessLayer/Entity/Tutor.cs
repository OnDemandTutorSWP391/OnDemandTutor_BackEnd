using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("Tutors")]
    public class Tutor
    {
        
        public int TutorId { get; set; }
        [AllowNull]
        public string? AcademicLevel { get; set; }
        [AllowNull]
        public string? WorkPlace { get; set; }
        [AllowNull]
        public string? OnlineStatus { get; set; }
        [AllowNull]
        public double AverageStar { get; set; }
        [AllowNull]
        public string? Degree { get; set; }
        [AllowNull]
        public string? CreditCard { get; set; }
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
        

    }
}
