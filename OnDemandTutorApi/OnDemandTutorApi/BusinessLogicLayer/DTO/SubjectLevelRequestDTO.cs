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
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Url { get; set; } = null!;
        [Required]
        public float Coin { get; set; }
        [Range(0, 5, ErrorMessage = "Để đảm bảo chất lượng giảng dạy, số lượng thành viên tối đa không được lớn hơn 5")]
        public int LimitMember { get; set; }
        public string? Image {  get; set; }
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
        public string ServiceName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Url { get; set; } = null!;
        public float Coin { get; set; }
        public string LimitMember { get; set; } = null!;
        public string? Image { get; set; }
        public bool IsLocked { get; set; }
    }
    #endregion

}
