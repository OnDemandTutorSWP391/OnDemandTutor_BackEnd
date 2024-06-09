﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnDemandTutorApi.BusinessLogicLayer.DTO
{
    public class TutorDTO
    {
        public string UserId { get; set; }
    }

    public class ProfileRequestDTO
    {
        [Required]
        public string? AcademicLevel { get; set; }
        [Required]
        public string? WorkPlace { get; set; }
        [Required]
        public string? Degree { get; set; }
        [Required]
        public string? CreditCard { get; set; }
        [Required]
        public string? TutorServiceName { get; set; }
        [Required]
        public string? TutorServiceDescription { get; set; }
        [Required]
        public string? TutorServiceVideo { get; set; }
        [Required]
        public string? LearningMaterialDemo { get; set; }
    }

    public class ProfileResponseDTO : ProfileRequestDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? OnlineStatus { get; set; }
        [Required]
        public string? Status { get; set; }
    }
}
