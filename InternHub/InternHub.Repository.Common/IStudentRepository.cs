using InternHub.Common.Filter;
using InternHub.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternHub.Model;

namespace InternHub.Repository.Common
{
    public interface IStudentRepository
    {
        Task<PagedList<Student>> GetStudentsAsync(Sorting sorting, Paging paging, StudentFilter filter);

        Task<Student> GetStudentByIdAsync(string id);

        Task<int> PostAsync(Student student);

        Task<int> DeleteAsync(string id);

        Task<int> PutAsync(string id, Student student);


    }
}
