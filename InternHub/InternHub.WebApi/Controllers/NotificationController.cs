using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Service;
using InternHub.Service.Common;
using InternHub.WebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InternHub.WebApi.Controllers
{
    public class NotificationController : ApiController
    {
        public INotificationService NotificationService { get; }
        public UserManager UserManager { get; }

        public NotificationController(INotificationService notificationService, UserManager userManager)
        {
            NotificationService = notificationService;
            UserManager = userManager;
        }

        public async Task<HttpResponseMessage> GetAsync(string sortBy = null, string sortOrder = null, int? currentPage = null, int? pageSize = null)
        {
            try
            {
                Sorting sorting = new Sorting();
                if (sortBy != null) sorting.SortBy = sortBy;
                if (sortOrder != null) sorting.SortOrder = sortOrder;

                Paging paging = new Paging();
                if (pageSize != null) paging.PageSize = pageSize.Value;
                if (currentPage != null) paging.CurrentPage = currentPage.Value;

                NotificationFilter filter = new NotificationFilter();

                PagedList<Notification> pagedList = await NotificationService.GetAllAsync(sorting, paging, filter);


                PagedList<NotificationView> notifications = new PagedList<NotificationView>()
                {
                    CurrentPage = pagedList.CurrentPage,
                    DatabaseRecordsCount = pagedList.DatabaseRecordsCount,
                    LastPage = pagedList.LastPage,
                    PageSize = pagedList.PageSize
                };
                foreach (Notification notification in pagedList.Data) notifications.Data.Add(new NotificationView(notification));

                return Request.CreateResponse(HttpStatusCode.OK, notifications);
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }

        public async Task<HttpResponseMessage> PostAsync([FromBody] NotificationPut notification)
        {
            try
            {
                if (notification == null || string.IsNullOrEmpty(notification.Body) || string.IsNullOrEmpty(notification.Subject) || notification.UserIds == null || notification.UserIds.Count == 0) return Request.CreateResponse(HttpStatusCode.BadRequest);

                List<User> users = new List<User>();
                foreach (string userId in notification.UserIds)
                {
                    User user = UserManager.FindById(userId);
                    if (user != null) users.Add(user);
                }

                if (users.Count == 0) return Request.CreateResponse(HttpStatusCode.BadRequest);

                bool result = await NotificationService.AddRangeAsync(notification.Subject, notification.Body, users);

                if (!result) return Request.CreateResponse(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Code crash"); }
        }
    }
}
