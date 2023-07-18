using InternHub.Common;
using InternHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternHub.Service.Common;
using InternHub.Repository;
using InternHub.Common.Filter;
using InternHub.Model.Common;
using InternHub.Repository.Common;

namespace InternHub.Service
{
    public class StudentService : IStudentService
    {
        IStudentRepository _studentRepository;
        
        public StudentService(IStudentRepository repository) 
        {
            _studentRepository = repository;
      
        }
        public async Task<PagedList<Student>> GetAllAsync(Sorting sorting, Paging paging, StudentFilter filter)
        {
            return await _studentRepository.GetStudentsAsync(sorting, paging, filter);
        }

        public async Task<Student> GetStudentByIdAsync(string id)
        {
            return await _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task<int> PostAsync(Student student)
        {
            return await _studentRepository.PostAsync(student);
        }
        public async Task<int> DeleteAsync(string id)
        {
            return await _studentRepository.DeleteAsync(id);
        }

        public async Task<int> PutAsync(string id, Student student)
        {
            return await _studentRepository.PutAsync(id, student);
        }
    }
}
