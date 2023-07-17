using InternHub.Common;
using InternHub.Model;
using InternHub.Service.Common;
using InternHub.WebApi.Models;
using Microsoft.Owin.Security.Provider;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InternHub.WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private ICompanyService CompanyService { get; set; }

        public CompanyController(ICompanyService companyService) {
            CompanyService = companyService;
        }
        public async Task<HttpResponseMessage> Get(int pageSize = 2, int currentPage = 1, string sortBy = "Id", string sortOrder = "ASC")
        {
            Sorting sorting = new Sorting();
            sorting.SortBy = sortBy;
            sorting.SortOrder = sortOrder;
            Paging paging = new Paging();
            paging.PageSize = pageSize;
            paging.CurrentPage = currentPage;

            PagedList<Company> pagedList = await CompanyService.GetAsync(sorting, paging);

            PagedList<CompanyView> pagedListView = new PagedList<CompanyView> { 
                CurrentPage = pagedList.CurrentPage,
                PageSize = pagedList.PageSize,
                DatabaseRecordsCount = pagedList.DatabaseRecordsCount,
                Data = pagedList.Data.Select(company => new CompanyView(company)).ToList(),
                LastPage = pagedList.LastPage
            };

            return Request.CreateResponse(HttpStatusCode.OK, pagedListView);
        }

        // GET api/<controller>/5
        public async Task<HttpResponseMessage> Get(string id)
        {
            Company company = await CompanyService.GetCompanyAsync(id);

            if (company == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There isn't any company with that id!");
            }

            CompanyView companyView = new CompanyView(company);

            return Request.CreateResponse(HttpStatusCode.OK, companyView);
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post([FromBody] CompanyUpdate updatedCompany)
        {
            Company company = new Company
            {
                Name = updatedCompany.Name,
                Website = updatedCompany.Website,
                FirstName = updatedCompany.FirstName,
                LastName = updatedCompany?.LastName,
                Address = updatedCompany.Address,
                Description = updatedCompany.Description,
                CountyId = updatedCompany.CountyId,
                Email = updatedCompany.Email
    };
            int num = await CompanyService.PostAsync(company);

            if (num <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }
            return Request.CreateResponse(HttpStatusCode.OK, company);
        }

        // PUT api/<controller>/5
        public async Task<HttpResponseMessage> Put(string id, [FromBody] Company updatedCompany)
        {
            Company existingCompany = await CompanyService.GetCompanyAsync(id);

            if (existingCompany == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There isn't any company with that id!");
            }

            if(updatedCompany.Website != null) existingCompany.Website = updatedCompany.Website;
            if (updatedCompany.Name != null) existingCompany.Name = updatedCompany.Name;

            if (updatedCompany.FirstName != null) existingCompany.FirstName = updatedCompany.FirstName;
            if (updatedCompany.LastName != null) existingCompany.LastName = updatedCompany.LastName;
            if (updatedCompany.Address != null) existingCompany.Address = updatedCompany.Address;
            if (updatedCompany.Description != null) existingCompany.Description = updatedCompany.Description;
            if (updatedCompany.Email != null) existingCompany.Email = updatedCompany.Email;

            int rowsAffected = await CompanyService.PutAsync(existingCompany);

            if (rowsAffected == -3)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "There are no specified parameters!");
            }
            if (rowsAffected <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Couldn't update it!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Updated");


        }

        // DELETE api/<controller>/5
        public async Task<HttpResponseMessage> Delete(string id)
        {
            int rowsAffected = await CompanyService.DeleteAsync(id);

            if (rowsAffected == -2)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Hall with this id doesn't exist.");
            }
            if (rowsAffected <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Couldn't delete hall!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Hall with id:{id} was deleted!");
        }


        [HttpGet, Route("api/Company/Approve/{id}")]
        public int ApproveCompany(string id)
        {
            return 1;
        }
    }
}