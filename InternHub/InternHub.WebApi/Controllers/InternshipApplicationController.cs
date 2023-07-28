using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Service.Common;
using InternHub.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;

namespace InternHub.WebApi.Controllers
{
    [Authorize]
    public class InternshipApplicationController : ApiController
    {
        private IInternshipApplicationService InternshipApplicationService { get; }
        private IStateService StateService { get; }

        public InternshipApplicationController(IInternshipApplicationService internshipApplicationService, IStateService stateService)
        {
            InternshipApplicationService = internshipApplicationService;
            StateService = stateService;
        }

        public async Task<HttpResponseMessage> GetAllAsync([FromUri] Sorting sorting = null, [FromUri] Paging paging = null, [FromUri] InternshipApplicationFilter filter = null)
        {
            try
            {
                PagedList<InternshipApplication> internshipApplications = await InternshipApplicationService.GetAllInternshipApplicationsAsync(paging, sorting, filter);
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
            catch (Exception ex)
            {
                // Log the exception here
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while processing the request.", ex);
            }
        }

        public async Task<HttpResponseMessage> GetByIdAsync(Guid id)
        {
            try
            {

                if (id == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
                InternshipApplication internshipApplication = await InternshipApplicationService.GetInternshipApplicationByIdAsync(id);

                if (internshipApplication == null) return Request.CreateResponse(HttpStatusCode.NotFound);
                return Request.CreateResponse(HttpStatusCode.OK, new InternshipApplicationView(internshipApplication));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "Student")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] PutInternshipApplication internshipApplication)
        {
            if (internshipApplication == null) { return Request.CreateResponse(HttpStatusCode.BadRequest); }
            string currentUserId = User.Identity.GetUserId();
            try
            {
                State state = await StateService.GetByNameAsync("Processing");

                if (state == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

                InternshipApplication newInternshipApplication = new InternshipApplication()
                {
                    Id = Guid.NewGuid(),
                    InternshipId = internshipApplication.InternshipId,
                    StudentId = currentUserId,
                    StateId = state.Id
                };

                bool success = await InternshipApplicationService.PostInternshipApplicationAsync(newInternshipApplication, currentUserId);

                if (!success) return Request.CreateResponse(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.OK, new InternshipApplicationView(newInternshipApplication));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
