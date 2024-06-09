using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class SubjectLevelDTO
    {
        [Required]
        public string LevelName { get; set; }
        [Required]
        public string SubjectName { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class SubjectLevelDTOWithId : SubjectLevelDTO
    {
        [Required]
        public int Id { get; set; }
    }

    public class SubjectLevelDTOWithData : SubjectLevelDTOWithId
    {
        [Required]
        public string TutorName { get; set; }
    }
}
