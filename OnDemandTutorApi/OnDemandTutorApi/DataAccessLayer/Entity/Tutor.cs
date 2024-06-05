
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("Tutor")]
    public class Tutor
    {
        [Key]
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

        [AllowNull]
        public string? Status { get; set; }
        [AllowNull]
        public string? TutorServiceName { get; set; }
        [AllowNull]
        public string? TutorServiceDescription { get; set; }
        [AllowNull]
        public string? TutorServiceVideo { get; set;}
        [AllowNull]
        public string? LearningMaterialDemo { get; set; }
    }
}
