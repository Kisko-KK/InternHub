using InternHub.Model;
using InternHub.Service.Common;
using InternHub.WebApi.Models;
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
    //[Authorize(Roles = "Admin")]
    public class CountyController : ApiController
    {
        public ICountyService CountyService { get; }

        public CountyController(ICountyService countyService)
        {
            CountyService = countyService;
        }

        // GET: api/County
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                List<County> counties = await CountyService.GetAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, counties.Select(x => new CountyView() { Name = x.Name }));
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }

        // GET: api/County/5
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            try
            {
                if (id == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

                County county = await CountyService.GetByIdAsync(id);

                if (county == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.OK, new CountyView() { Name = county.Name });
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }

        // POST: api/County
        public async Task<HttpResponseMessage> Post([FromBody] CountyView county)
        {
            try
            {
                if(county == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

                County newCounty = new County
                {
                    Id = Guid.NewGuid(),
                    Name = county.Name
                };

                bool result = await CountyService.Add(newCounty, User.Identity.GetUserId());

                if (!result) return Request.CreateResponse(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }

        // PUT: api/County/5
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] CountyView county)
        {
            try
            {
                if(id == null || county == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

                County oldCounty = await CountyService.GetByIdAsync(id);
                
                if (county.Name != null) oldCounty.Name = county.Name;

                bool result = await CountyService.Update(oldCounty, User.Identity.GetUserId());

                if (!result) return Request.CreateResponse(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }

        // DELETE: api/County/5
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            try
            {
                if(id == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

                bool result = await CountyService.Remove(id);

                if(!result) return Request.CreateResponse(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }
    }
}
