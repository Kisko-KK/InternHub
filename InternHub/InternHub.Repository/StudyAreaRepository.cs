using InternHub.Common;
using InternHub.Model;
using InternHub.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Repository
{
    internal class StudyAreaRepository : IStudyAreaRepository
    {
        public Task<bool> DeleteInternshipApplicationAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<StudyArea>> GetAllInternshipApplicationsAsync(Paging paging, Sorting sorting, StudyAreaFilter studyAreaFilter)
        {
            throw new NotImplementedException();
        }

        public Task<StudyArea> GetInternshipApplicationByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostInternshipApplicationAsync(StudyArea studyArea)
        {
            throw new NotImplementedException();
        }

        public Task<StudyArea> PutInternshipApplicationAsync(Guid id, StudyArea studyArea)
        {
            throw new NotImplementedException();
        }
    }
}
