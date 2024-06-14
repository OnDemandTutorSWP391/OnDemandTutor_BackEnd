namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    #region Request

    public class RatingRequestDTO
    {
        public int TutorId { get; set; }
        public float Star {  get; set; }
        public string Description { get; set; } = null!;
    }

    public class RatingUpdateDTO
    {
        public float Star { get; set; }
        public string Description { get; set; } = null!;
    }

    #endregion

    #region Response

    public class RatingResponseDTO
    {
        public int Id { get; set; }
        public string TutorName { get; set; }
        public string StudentName { get; set; } = null!;
        public float Star { get; set; }
        public string Description { get; set; } = null!;
    }

    #endregion
}
