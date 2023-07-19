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
using System.Web.Mvc;

namespace InternHub.WebApi.Controllers
{
    public class InternshipController : ApiController
    {
        private IInternshipService InternshipService { get; set; }
        public InternshipController(IInternshipService internshipService) {
            InternshipService = internshipService;
        }
        // GET api/<controller>
        public async Task<HttpResponseMessage> Get(string sortBy = "Name", string orderBy = "Asc", int currentPage = 1, int pageSize = 10, bool? isActive = null, string counties = null, string name = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            Sorting sorting = new Sorting();
            sorting.SortBy = sortBy;
            sorting.SortOrder = orderBy;

            Paging paging = new Paging();
            paging.PageSize = pageSize;
            paging.CurrentPage = currentPage;

            List<Guid> countyIds = null;
            if(counties != null)
            {
                countyIds = new List<Guid>();
                foreach (string county in counties.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Guid? id = null;
                    try
                    {
                        id = Guid.Parse(county);
                    }
                    catch { }
                    if (id != null) countyIds.Add(id.Value);
                }
            }
            

            InternshipFilter filter = new InternshipFilter(countyIds, startDate, endDate, name, isActive);

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

        // GET api/<controller>/5
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            Internship internship = await InternshipService.GetAsync(id);

            if(internship == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found!");
            }
            CompanyView companyView = new CompanyView
            {
                Name = internship.Company.Name
            };
            InternshipView internshipView = new InternshipView {
                StudyAreaId = internship.StudyAreaId,
                StudyArea = internship.StudyArea,
                CompanyId = internship.CompanyId,
                CompanyView = companyView,
                Name = internship.Name,
                Description = internship.Description,
                Address = internship.Address,
                StartDate = internship.StartDate,
                EndDate = internship.EndDate
            };
            
            return Request.CreateResponse(HttpStatusCode.OK, internshipView);
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post([FromBody] InternshipUpdate internshipUpdate)
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
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] InternshipUpdate updatedInternship)
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
        public async Task<HttpResponseMessage> Delete(Guid id)
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