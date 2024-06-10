using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    #region Request
    public class SubjectLevelRequestDTO
    {
        [Required]
        public int LevelId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int TutorId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public float Coin { get; set; }
    }

    #endregion

    #region Response
    public class SubjectLevelResponseDTO
    {
        [Required]
        public int Id { get; set; }
        public string LevelName { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public string TutorName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Url { get; set; } = null!;
        public float Coin { get; set; }
    }
    #endregion

}
