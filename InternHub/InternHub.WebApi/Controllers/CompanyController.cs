
using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Service;
using InternHub.Service.Common;
using InternHub.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InternHub.WebApi.Controllers
{
    [Authorize]
    public class CompanyController : ApiController
    {
        private ICompanyService CompanyService { get; set; }
        public INotificationService NotificationService { get; set; }
        public UserManager UserManager { get; set; }

        public CompanyController(ICompanyService companyService, UserManager userManager, INotificationService notificationService)
        {
            CompanyService = companyService;
            UserManager = userManager;
            NotificationService = notificationService;
        }

        public async Task<HttpResponseMessage> GetAsync(int? pageSize = null, int? currentPage = null, string sortBy = null, string sortOrder = null, bool? isActive = null, bool? isAccepted = null, string name = null)
        {
            Sorting sorting = new Sorting();
            if(sortBy != null) sorting.SortBy = sortBy;
            if(sortOrder != null) sorting.SortOrder = sortOrder;
            Paging paging = new Paging();
            if(pageSize != null) paging.PageSize = pageSize.Value;
            if(currentPage != null) paging.CurrentPage = currentPage.Value;
            CompanyFilter filter = new CompanyFilter();
            filter.IsActive = isActive;
            filter.IsAccepted = isAccepted;
            filter.Name = name;

            PagedList<Company> pagedList = await CompanyService.GetAsync(sorting, paging, filter);

            PagedList<CompanyView> pagedListView = new PagedList<CompanyView>
            {
                CurrentPage = pagedList.CurrentPage,
                PageSize = pagedList.PageSize,
                DatabaseRecordsCount = pagedList.DatabaseRecordsCount,
                Data = pagedList.Data.Select(company => new CompanyView(company)).ToList(),
                LastPage = pagedList.LastPage
            };

            return Request.CreateResponse(HttpStatusCode.OK, pagedListView);
        }

        // GET api/<controller>/5
        public async Task<HttpResponseMessage> GetAsync(string id)
        {
            Company existingCompany = await CompanyService.GetAsync(id);

            if (existingCompany == null) { return Request.CreateResponse(HttpStatusCode.NotFound, "There isn't any company with that id!"); }

            CompanyView companyView = new CompanyView(existingCompany);

            return Request.CreateResponse(HttpStatusCode.OK, companyView);
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> PostAsync([FromBody] CompanyPost updatedCompany)
        {
            if (updatedCompany == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

            Company company = new Company
            {
                Name = updatedCompany.Name,
                Website = updatedCompany.Website,
                FirstName = updatedCompany.FirstName,
                LastName = updatedCompany.LastName,
                Address = updatedCompany.Address,
                Description = updatedCompany.Description,
                CountyId = updatedCompany.CountyId,
                Email = updatedCompany.Email
            };

            if (await CompanyService.PostAsync(company) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }

            IdentityResult result = await UserManager.AddToRoleAsync(company.Id, "Company");

            return Request.CreateResponse(HttpStatusCode.OK, company);
        }

        // PUT api/<controller>/5
        [Authorize(Roles = "Company,Admin")]
        public async Task<HttpResponseMessage> PutAsync(string id, [FromBody] CompanyPut updatedCompany)
        {
            Company existingCompany = await CompanyService.GetAsync(id);

            if (existingCompany == null) { return Request.CreateResponse(HttpStatusCode.NotFound, "There isn't any company with that id!"); }

            if (updatedCompany.Website != null) existingCompany.Website = updatedCompany.Website;
            if (updatedCompany.Name != null) existingCompany.Name = updatedCompany.Name;
            if (updatedCompany.FirstName != null) existingCompany.FirstName = updatedCompany.FirstName;
            if (updatedCompany.LastName != null) existingCompany.LastName = updatedCompany.LastName;
            if (updatedCompany.Address != null) existingCompany.Address = updatedCompany.Address;
            if (updatedCompany.Description != null) existingCompany.Description = updatedCompany.Description;
            if (updatedCompany.Email != null) existingCompany.Email = updatedCompany.Email;
            if (updatedCompany.CountyId != null) existingCompany.CountyId = updatedCompany.CountyId.Value;

            if (await CompanyService.PutAsync(existingCompany) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Couldn't update it!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Updated");
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = "Company,Admin")]
        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            Company existingCompany = await CompanyService.GetAsync(id);

            if (existingCompany == null) { return Request.CreateResponse(HttpStatusCode.NotFound, "There isn't any company with that id!"); }

            if (await CompanyService.DeleteAsync(existingCompany) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Couldn't delete company!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Company with id: {id} was deleted!");
        }


        [HttpPut, Route("api/Company/Approve/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<HttpResponseMessage> ApproveAsync(string id)
        {
            Company existingCompany = await CompanyService.GetAsync(id);
            if (existingCompany == null) { return Request.CreateResponse(HttpStatusCode.NotFound, "There isn't any company with that id!"); }

            if (await CompanyService.AcceptAsync(existingCompany) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Couldn't approve company!");
            }

            await NotificationService.AddAsync("Vaš račun je prihvaćen", "Poštovani " + existingCompany.GetFullName() + "!\n\nVaša prijava za tvrtku " + existingCompany.Name + " na platformi InternHub je odobrena. Od sada možete neometano objavljivati vaše prakse!" + " \n\nVaša InternHub ekipa", existingCompany);

            return Request.CreateResponse(HttpStatusCode.OK, "Company accepted!");
        }
    }
}