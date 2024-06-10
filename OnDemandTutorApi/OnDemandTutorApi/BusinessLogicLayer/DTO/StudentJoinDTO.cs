using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class StudentJoinDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int SubjectLevelId { get; set; }

    }

    public class StudentJoinDTOWithId : StudentJoinDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
