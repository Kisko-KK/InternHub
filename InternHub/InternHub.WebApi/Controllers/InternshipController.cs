using InternHub.Model;
using InternHub.Service;
using InternHub.Service.Common;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InternHub.WebApi.Controllers
{
    public class InternshipController : ApiController
    {
        private IInternshipService InternshipService { get; set; }
        public InternshipController(IInternshipService internshipService) {
            InternshipService = internshipService;
        }
        // GET api/<controller>
        public async Task<HttpResponseMessage> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/<controller>/5
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            Internship internship = await InternshipService.GetAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK, internship);
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post([FromBody] Internship internship)
        {
            string currentUserId = User.Identity.GetUserId();

            await InternshipService.PostAsync(internship, currentUserId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            Internship existingInternship = await InternshipService.GetInternshipAsync(id);

            if (existingInternship == null) { return Request.CreateResponse(HttpStatusCode.NotFound, "There isn't any internship with that id!"); }

            if (await InternshipService.DeleteAsync(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Couldn't delete internship!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Internship with id:{id} was deleted!");
        }
    }
}