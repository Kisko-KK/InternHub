using InternHub.Common;
using InternHub.Model;
using InternHub.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InternHub.WebApi.Controllers
{
    public class InternshipApplicationController : ApiController
    {
        protected IInternshipApplicationService _internshipApplicationService { get; set; }  

        public InternshipApplicationController(IInternshipApplicationService internshipApplicationService) 
        {
            _internshipApplicationService = internshipApplicationService;   
        }

        public async Task<HttpResponseMessage> GetAll(int currentPage, int pageSize, string sortBy, string sortOrder, string stateName, string internshipName)
        {
            try
            {
                Sorting sorting = new Sorting()
                {
                    SortBy = sortBy,
                    SortOrder = sortOrder
                };
                if (sorting.SortOrder.ToLower() != "desc" && sorting.SortOrder.ToLower() != "asc") sorting.SortOrder = "ASC";
                Paging paging = new Paging()
                {
                    PageSize = pageSize,
                    CurrentPage = currentPage
                };
                InternshipApplicationFilter filter = new InternshipApplicationFilter()
                {
                    StateName = stateName,
                    InternshipName = internshipName
                };

                PagedList<InternshipApplication> internshipApplications = await _internshipApplicationService.GetAllInternshipApplicationsAsync(paging, sorting, filter);
                if (internshipApplications == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                PagedList<InternshipApplicationView> pagedApplications = new PagedList<InternshipApplicationView>
                {
                    CurrentPage = internshipApplications.CurrentPage,
                    DatabaseRecordsCount = internshipApplications.DatabaseRecordsCount,
                    LastPage = internshipApplications.LastPage,
                    PageSize = internshipApplications.PageSize,
                    Data = internshipApplications.Data.Select(x => new InternshipApplicationView(x)).ToList()
                };
                return Request.CreateResponse(HttpStatusCode.OK, pagedApplications);
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError,"Code crashed");}
        }

    }
}
