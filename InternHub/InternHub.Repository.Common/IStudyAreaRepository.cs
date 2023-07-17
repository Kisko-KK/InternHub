using InternHub.Common;
using InternHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Repository.Common
{
    public interface IStudyAreaRepository
    {
        Task<PagedList<StudyArea>> GetAllStudyAreasAsync(Paging paging, Sorting sorting, StudyAreaFilter studyAreaFilter);
        Task<StudyArea> GetStudyAreaByIdAsync(Guid id);
        Task<bool> PostStudyAreaByAsync(StudyArea studyArea);
        Task<bool> PutStudyAreaByAsync(StudyArea studyArea);
        Task<bool> DeleteStudyAreaByAsync(Guid id);
    }
}
