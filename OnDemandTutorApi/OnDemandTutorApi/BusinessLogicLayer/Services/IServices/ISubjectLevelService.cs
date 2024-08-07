﻿using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ISubjectLevelService
    {
        public Task<ResponseApiDTO<SubjectLevelResponseDTO>> CreateAsync(string userId, SubjectLevelRequestDTO subjectLevelDTO);
        public Task<ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>> GetAllAsync(string? level, string? subject, string? tutor, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>> GetAllByTutorIdAsync(string userId, string? level, string? subject, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>> GetAllByLevelIdAsync(int levelId,  string? tutor, string? subject, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>> GetAllBySubjectIdAsync(int subjectId, string? tutor, string? level, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>> GetAllForStaffAsync(string? level, string? subject, string? tutor, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>> GetTop6FollowRatingAsync(string? level, string? subject, string? tutor, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>> GetThreeCourseOfAnyTutorAsync(int tutorId, string? level, string? subject, int page = 1);
        public Task<ResponseApiDTO<SubjectLevelResponseDTO>> GetByIdAsync(int subjectLevelId);
        public Task<ResponseApiDTO> UpdateForTutorAsync(int id, string userId, SubjectLevelRequestDTO subjectLevelDTO);
        public Task<ResponseApiDTO> DeleteForTutorAsync(int id);
        public Task<ResponseApiDTO> DeleteForStaffAsync(int id);
    }
}
