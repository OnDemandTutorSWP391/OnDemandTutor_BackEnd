using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class StudentJoinRequestDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int SubjectLevelId { get; set; }

    }

    public class StudentJoinResponseDTO : StudentJoinRequestDTO
    {
        [Required]
        public int Id { get; set; }
        public int TutorId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsLocked { get; set; }
    }
}
