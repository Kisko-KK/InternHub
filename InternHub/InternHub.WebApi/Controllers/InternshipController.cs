using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Service;
using InternHub.Service.Common;
using InternHub.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;

namespace InternHub.WebApi.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Internship")]
    public class InternshipController : ApiController
    {
        private IInternshipService InternshipService { get; set; }
        public InternshipController(IInternshipService internshipService) {
            InternshipService = internshipService;
        }

        // GET api/<controller>
        public async Task<HttpResponseMessage> GetAsync([FromUri] Sorting sorting = null, [FromUri] Paging paging = null, [FromUri] InternshipFilter filter = null)
        {
            PagedList<Internship> pagedList = await InternshipService.GetAsync(sorting, paging, filter);
            PagedList<InternshipView> pagedListView = new PagedList<InternshipView>
            {
                CurrentPage = pagedList.CurrentPage,
                PageSize = pagedList.PageSize,
                DatabaseRecordsCount = pagedList.DatabaseRecordsCount,
                Data = pagedList.Data.Select(intership => new InternshipView(intership)).ToList(),
                LastPage = pagedList.LastPage
            };

            return Request.CreateResponse(HttpStatusCode.OK, pagedListView);
        }


        [HttpGet]
        [Route("api/Internship/GetCompanyViewInternship")]
        public async Task<HttpResponseMessage> GetCompanyViewInternshipAsync([FromUri] Sorting sorting = null, [FromUri] Paging paging = null, [FromUri] InternshipFilter filter = null)
        {
            PagedList<Internship> pagedList = await InternshipService.GetAsync(sorting, paging, filter);
            PagedList<CompanyHomeInternshipView> pagedListView = new PagedList<CompanyHomeInternshipView>
            {
                CurrentPage = pagedList.CurrentPage,
                PageSize = pagedList.PageSize,
                DatabaseRecordsCount = pagedList.DatabaseRecordsCount,
                Data = pagedList.Data.Select(intership => new CompanyHomeInternshipView(intership)).ToList(),
                LastPage = pagedList.LastPage
            };

            return Request.CreateResponse(HttpStatusCode.OK, pagedListView);
        }

        // GET api/<controller>/5
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            Internship internship = await InternshipService.GetAsync(id);

            if(internship == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found!");
            }
            InternshipView internshipView = new InternshipView {
                StudyAreaId = internship.StudyAreaId,
                StudyArea = new StudyAreaView(internship.StudyArea),
                CompanyId = internship.CompanyId,
                Company = new CompanyView(internship.Company),
                Name = internship.Name,
                Description = internship.Description,
                Address = internship.Address,
                StartDate = internship.StartDate,
                EndDate = internship.EndDate
            };
            
            return Request.CreateResponse(HttpStatusCode.OK, internshipView);
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> PostAsync([FromBody] InternshipUpdate internshipUpdate)
        {
            string currentUserId = User.Identity.GetUserId();
            Internship internship = new Internship
            {
                StudyAreaId = internshipUpdate.StudyAreaId,
                CompanyId = internshipUpdate.CompanyId,
                Name = internshipUpdate.Name,
                Description = internshipUpdate.Description,
                Address = internshipUpdate.Address,
                StartDate = internshipUpdate.StartDate,
                EndDate = internshipUpdate.EndDate
            };

            if(await InternshipService.PostAsync(internship, currentUserId))
            {
                return Request.CreateResponse(HttpStatusCode.OK, internship);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Couldn't create new internship!");
        }

        // PUT api/<controller>/5
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] InternshipUpdate updatedInternship)
        {
            Internship internship = await InternshipService.GetAsync(id);
            string currentUserId = User.Identity.GetUserId();

            if (internship == null) { return Request.CreateResponse(HttpStatusCode.NotFound, "There isn't any internship with that id!"); }

            if (updatedInternship.Name != null) internship.Name = updatedInternship.Name;
            if (updatedInternship.Description != null) internship.Description = updatedInternship.Description;
            if (updatedInternship.Address != null) internship.Address = updatedInternship.Address;
            if (updatedInternship.StartDate != null) internship.StartDate = updatedInternship.StartDate;
            if (updatedInternship.EndDate != null) internship.EndDate = updatedInternship.EndDate;
            if (updatedInternship.CompanyId != null) internship.CompanyId = updatedInternship.CompanyId;
            if (updatedInternship.StudyAreaId != null) internship.StudyAreaId = updatedInternship.StudyAreaId;

            if (await InternshipService.PutAsync(internship, currentUserId) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Couldn't update it!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Updated");
        }

        // DELETE api/<controller>/5
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            Internship existingInternship = await InternshipService.GetInternshipAsync(id);
            string currentUserId = User.Identity?.GetUserId();

            if (existingInternship == null) { return Request.CreateResponse(HttpStatusCode.NotFound, "There isn't any internship with that id!"); }

            if (await InternshipService.DeleteAsync(existingInternship, currentUserId) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Couldn't delete internship!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Internship with id:{id} was deleted!");
        }
    }
}