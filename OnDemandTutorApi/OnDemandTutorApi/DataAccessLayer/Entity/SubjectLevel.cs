﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    [Table("SubjectLevel")]
    public class SubjectLevel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int LevelId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int TutorId { get; set; }
        public string Description { get; set; } = null!;
    }
}
