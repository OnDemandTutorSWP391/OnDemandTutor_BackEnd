namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class SubjectDTO
    {
        public string Name { get; set; } = null!;
    }

    public class SubjectDTOWithId : SubjectDTO
    {
        public int Id { get; set; }
    }
}
