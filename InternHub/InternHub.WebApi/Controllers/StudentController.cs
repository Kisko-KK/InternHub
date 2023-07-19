using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Service;
using InternHub.Service.Common;
using InternHub.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace InternHub.WebApi.Controllers
{
    [Authorize]
    public class StudentController : ApiController
    {
        private IStudentService StudentService { get; }
        private UserManager UserManager { get; set; }

        // GET: Student
        public StudentController(IStudentService studentService, UserManager userManager)
        {
            StudentService = studentService;
            UserManager = userManager;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get(string orderBy = null, string sortOrder = null, int? pageSize = null, int? pageCount = null, string studyAreas = null, string counties = null, string firstName = null, string lastName = null, bool isActive = true)
        {
            try
            {
                Sorting sorting = new Sorting();
                if (orderBy != null) sorting.SortBy = orderBy;
                if (sortOrder != null) sorting.SortOrder = sortOrder;

                Paging paging = new Paging();
                if (pageSize != null) paging.PageSize = pageSize.Value;
                if (pageCount != null) paging.CurrentPage = pageCount.Value;

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

                PagedList<Student> mappedStudents = await StudentService.GetAllAsync(sorting, paging, filter);

                mappedStudents.Data.ForEach(student =>
                {
                    StudentView studentView = new StudentView(student);
                    students.Add(studentView);
                });

                PagedList<StudentView> pagedStudents = new PagedList<StudentView>()
                {
                    CurrentPage = mappedStudents.CurrentPage,
                    DatabaseRecordsCount = mappedStudents.DatabaseRecordsCount,
                    LastPage = mappedStudents.LastPage,
                    PageSize = mappedStudents.PageSize,
                    Data = students
                };

                return Request.CreateResponse(HttpStatusCode.OK, mappedStudents);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "GRESKA", ex);
            }

        }

        [HttpGet]
        [Route("api/students/{id}")]
        public async Task<HttpResponseMessage> GetByIdAsync(string id)
        {
            if (id == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
            Student student = await StudentService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There isn't any student with that id!");
            }

            StudentView studentView = new StudentView(student);

            return Request.CreateResponse(HttpStatusCode.OK, studentView);
        }

        [AllowAnonymous]
        public async Task<HttpResponseMessage> PostAsync([FromBody] PostStudent student)
        {
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }


            Guid generatedId = Guid.NewGuid();

            Student mappedStudent = new Student()
            {
                Id = generatedId.ToString(),
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                Description = student.Description,
                CountyId = student.CountyId,
                StudyAreaId = student.StudyAreaId,
            };

            int rowsAffected = await StudentService.PostAsync(mappedStudent);

            if (rowsAffected <= 0) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Couldn't create new student");

            UserManager.AddToRole(mappedStudent.Id, "Student");

            return Request.CreateResponse(HttpStatusCode.OK, mappedStudent);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {

            Student existingStudent = await StudentService.GetStudentByIdAsync(id);


            if (existingStudent == null) { return Request.CreateResponse(HttpStatusCode.NotFound, "There isn't any student with that id! "); }

            if (await StudentService.DeleteAsync(existingStudent) != 1)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to delete student!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, $"Student with id : {id} is deleted! ");

        }

        public async Task<HttpResponseMessage> PutAsync(string id, [FromBody] StudentPut student)
        {
            try
            {
                Student existingStudent = await StudentService.GetStudentByIdAsync(id);

                if (existingStudent == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Student with id: {id} is not found");
                }


                if (!string.IsNullOrEmpty(student.FirstName))
                {
                    existingStudent.FirstName = student.FirstName;

                }
                if (!string.IsNullOrEmpty(student.LastName))
                {
                    existingStudent.LastName = student.LastName;

                }
                if (!string.IsNullOrEmpty(student.Email))
                {
                    existingStudent.Email = student.Email;

                }
                if (!string.IsNullOrEmpty(student.PhoneNumber))
                {
                    existingStudent.PhoneNumber = student.PhoneNumber;

                }
                if (!string.IsNullOrEmpty(student.Address))
                {
                    existingStudent.Address = student.Address;

                }
                if (!string.IsNullOrEmpty(student.Description))
                {
                    existingStudent.Description = student.Description;

                }
                if (student.StudyAreaId != null)
                {
                    existingStudent.StudyAreaId = student.StudyAreaId.Value;
                }
                if (student.CountyId != null)
                {
                    existingStudent.CountyId = student.CountyId.Value;
                }

                int rowsAffected = await StudentService.PutAsync(existingStudent);

                if (rowsAffected <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Couldn't update student");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, existingStudent);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
