﻿using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Service.Common;
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
    public class InternshipApplicationController : ApiController
    {
        protected IInternshipApplicationService _internshipApplicationService { get; set; }  

        public InternshipApplicationController(IInternshipApplicationService internshipApplicationService) 
        {
            _internshipApplicationService = internshipApplicationService;   
        }

        public async Task<HttpResponseMessage> GetAll(int? currentPage=null, int? pageSize=null, string sortBy=null, string sortOrder=null, string stateName=null, string internshipName=null)
        {
            try
            {
                Sorting sorting = new Sorting();
                if (sortBy != null)
                {
                    sorting.SortBy = sortBy;
                }
                if(sortOrder != null)
                {
                    sorting.SortOrder = sortOrder;  
                }
                Paging paging = new Paging();

                if(currentPage != null) {
                    paging.CurrentPage = (int)currentPage;
                }
                if(pageSize != null) {
                    paging.PageSize = (int)pageSize;
                }
              
                InternshipApplicationFilter filter = new InternshipApplicationFilter();
                if (stateName != null)
                {
                    filter.StateName = stateName;   
                }
                if(internshipName!=null) {
                    filter.InternshipName=internshipName;   
                }    

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
            catch (Exception ex)
            {
                // Log the exception here
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while processing the request.", ex);
            }
        }

        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            try
            {
                
                if(id==null) return Request.CreateResponse(HttpStatusCode.BadRequest);
                InternshipApplication internshipApplication = await _internshipApplicationService.GetInternshipApplicationByIdAsync(id);

                if (internshipApplication == null) return Request.CreateResponse(HttpStatusCode.NotFound);
                return Request.CreateResponse(HttpStatusCode.OK, new InternshipApplicationView(internshipApplication));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //za post metodu
        //[Login]
        //User.Identity.GetUserId();


    }
}
