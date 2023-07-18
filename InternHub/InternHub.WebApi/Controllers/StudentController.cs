using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Service;
using InternHub.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace InternHub.WebApi.Controllers
{
    public class StudentController : ApiController
    {
        IStudentService studentService;
        // GET: Student
        public StudentController(IStudentService _studentService)
        {
            studentService = _studentService;
        }

        [System.Web.Http.HttpGet]
        public async Task<HttpResponseMessage> Get(string orderBy = null, string sortOrder = null, int? pageSize = null, int? pageCount = null, string studyAreas = null, string counties = null, string firstName = null, string lastName = null, bool isActive = true)
        {
            try
            {
                Sorting sorting = new Sorting();
                if(orderBy != null) sorting.SortBy = orderBy;
                if (sortOrder != null) sorting.SortOrder = sortOrder;

                Paging paging = new Paging();
                if (pageSize != null) paging.PageSize = pageSize.Value;
                if(pageCount != null) paging.CurrentPage = pageCount.Value;

                List<Guid> studyAreaIds = new List<Guid>();
                if (studyAreas != null)
                {
                    foreach (string studyArea in studyAreas.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        Guid? id = null;
                        try
                        {
                            id = Guid.Parse(studyArea);
                        }
                        catch { }
                        if (id != null) studyAreaIds.Add(id.Value);
                    }
                }

                List<Guid> countyIds = new List<Guid>();
                if (counties != null)
                {
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

                StudentFilter filter = new StudentFilter(firstName, lastName, studyAreaIds, countyIds, isActive);

                List<StudentView> students = new List<StudentView>();

                PagedList<Student> mappedStudents = await studentService.GetAllAsync(sorting, paging, filter);

                mappedStudents.Data.ForEach(student =>
                {
                    StudentView studentView = new StudentView(student.Id, student.FirstName, student.LastName, student.Email, student.PhoneNumber, student.Address, student.Description, student.DateCreated, student.DateUpdated, student.CountyId, student.IsActive, student.StudyArea);
                    students.Add(studentView);
                });

                return Request.CreateResponse(HttpStatusCode.OK, mappedStudents);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "GRESKA", ex);
            }

        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/students/{id}")]
        public async Task<HttpResponseMessage> GetByIdAsync(string id)
        {
            if (id == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
            Student student = await studentService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There isn't any student with that id!");
            }

            StudentView studentView = new StudentView(student.Id, student.FirstName, student.LastName, student.Email, student.PhoneNumber, student.Address, student.Description, student.DateCreated, student.DateUpdated, student.CountyId, student.IsActive, student.StudyArea);

            return Request.CreateResponse(HttpStatusCode.OK, studentView);
        }

        public async Task<HttpResponseMessage> Post([FromBody] StudentView student)
        {
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Guid idToString = Guid.NewGuid();

            Student mappedStudent = new Student()
            {
                Id = idToString.ToString(),
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                Description = student.Description,
                DateCreated = student.DateCreated,
                DateUpdated = student.DateUpdated,
                CountyId = student.CountyId,
                IsActive = student.IsActive,
                StudyArea = student.StudyArea

            };
            int rowsAffected = await studentService.PostAsync(mappedStudent);

            if (rowsAffected <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Couldn't create new student");
            }
            else
                return Request.CreateResponse(HttpStatusCode.OK, new StudentView(student.Id, student.FirstName, student.LastName, student.Email, student.PhoneNumber, student.Address, student.Description, student.DateCreated, student.DateUpdated, student.CountyId, student.IsActive, student.StudyArea));
        }


    }
}
