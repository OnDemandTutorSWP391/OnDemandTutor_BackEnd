﻿using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class StudentJoinRepo : IStudentJoinRepo
    {
        private readonly StudentJoinDAO _studentJoinDAO;

        public StudentJoinRepo(StudentJoinDAO studentJoinDAO)
        {
            _studentJoinDAO = studentJoinDAO;
        }

        public async Task<bool> CreateAsync(StudentJoin studentJoin)
        {
           return await _studentJoinDAO.CreateAsync(studentJoin);
        }

        public async Task<bool> DeleteAsync(StudentJoin studentJoin)
        {
            return await _studentJoinDAO.DeleteAysnc(studentJoin);
        }

        public async Task<IEnumerable<StudentJoin>> GetAllAsync()
        {
            return await _studentJoinDAO.GetAllAsync();
        }

        public async Task<StudentJoin> GetByIdAsync(int id)
        {
            return await _studentJoinDAO.GetByIdAsync(id);
        }

        public async Task<IEnumerable<StudentJoin>> GetAllBySubjectLevelIdAsync(int id)
        {
            return await _studentJoinDAO.GetBySubjectLevelIdAsync(id);
        }

        public async Task<IEnumerable<StudentJoin>> GetAllByUserIdAsync(string userId)
        {
            return await _studentJoinDAO.GetAllByUserIdAsync(userId);
        }

        public async Task<IEnumerable<StudentJoin>> GetAllByTutorIdAsync(int id)
        {
            return await _studentJoinDAO.GetByTutorIdAsync(id);
        }
    }
}
