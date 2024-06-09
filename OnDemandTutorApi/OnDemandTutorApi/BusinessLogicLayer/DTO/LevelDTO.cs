namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class LevelDTO
    {
        public string Name { get; set; } = null!;
    }

    public class LevelDTOWithId : LevelDTO
    {
        public int Id { get; set; }
    }
}
