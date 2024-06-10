using System.ComponentModel.DataAnnotations;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    #region Request
    public class TimeRequestDTO
    {
        public int SubjectLevelId { get; set; }
        public string SlotName { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime StartSlot {  get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime EndSlot {  get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date {  get; set; }
    }
    #endregion

    #region Response
    public class TimeResponseDTO : TimeRequestDTO
    {
        public int Id { get; set; }
    }
    #endregion
}
