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
        Task<PagedList<StudyArea>> GetAllInternshipApplicationsAsync(Paging paging, Sorting sorting, StudyAreaFilter studyAreaFilter);
        Task<StudyArea> GetInternshipApplicationByIdAsync(Guid id);
        Task<bool> PostInternshipApplicationAsync(StudyArea studyArea);
        Task<StudyArea> PutInternshipApplicationAsync(Guid id, StudyArea studyArea);
        Task<bool> DeleteInternshipApplicationAsync(Guid id);
    }
}
