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
    [Authorize(Roles = "Admin")]
    public class StateController : ApiController
    {
        public IStateService StateService { get; set; }

        public StateController(IStateService stateService)
        {
            StateService = stateService;
        }

        // GET: api/State
        [Authorize]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                List<State> states = await StateService.GetAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, states.Select(x => new StateView(x)));
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }

        // GET: api/State/5
        [Authorize]
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            try
            {
                if (id == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
                State state = await StateService.GetByIdAsync(id);
                if (state == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.OK, new StateView(state));
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }

        // POST: api/State
        public async Task<HttpResponseMessage> Post([FromBody] StateView state)
        {
            try
            {
                if (state == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
                State newState = new State
                {
                    Id = Guid.NewGuid(),
                    Name = state.Name
                };
                bool result = await StateService.Add(newState, User.Identity.GetUserId());
                if (!result) return Request.CreateResponse(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }

        // PUT: api/State/5
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] StateView state)
        {
            try
            {
                if (id == null || state == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
                State oldState = await StateService.GetByIdAsync(id);

                if (state.Name != null) oldState.Name = state.Name;

                bool result = await StateService.Update(oldState, User.Identity.GetUserId());
                if (!result) return Request.CreateResponse(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }

        // DELETE: api/State/5
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            try
            {
                if (id == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

                bool result = await StateService.Remove(id);

                if (!result) return Request.CreateResponse(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }
    }
}
