using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Service.Common
{
    public interface IStudentService
    {
        Task<PagedList<Student>> GetAllAsync(Sorting sorting, Paging paging, StudentFilter filter);
        Task<Student> GetStudentByIdAsync(string id);
        Task<int> PostAsync(Student student);
        Task<int> DeleteAsync(string id);
        Task<int> PutAsync(string id, Student student);
        
    }
}
