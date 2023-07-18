using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Model.Common;
using InternHub.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Repository
{
    public class StudentRepository : IStudentRepository
    {
        IConnectionString _connectionString;
        
        public StudentRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<PagedList<Student>> GetStudentsAsync(Sorting sorting, Paging paging, StudentFilter filter)
        {
            PagedList<Student> pagedStudents = new PagedList<Student>()
            {
                CurrentPage = paging.CurrentPage,
                PageSize = paging.PageSize,
            };

            StringBuilder selectQueryBuilder = new StringBuilder();
            StringBuilder countQueryBuilder = new StringBuilder();
            StringBuilder filterQueryBuilder = new StringBuilder();

            List<Student> students = new List<Student>();

            int totalCount;


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.Name.Replace("\"", "")))
            {
                await connection.OpenAsync();

                string selectQuery = "SELECT * FROM public.\"Student\" s inner join dbo.\"AspNetUsers\" u on u.\"Id\" = s.\"Id\" ";
                string countQuery = "SELECT COUNT(*) FROM public.\"Student\" s inner join dbo.\"AspNetUsers\" u on u.\"Id\" = s.\"Id\" ";
                string initialFilterCondition = " WHERE u.\"IsActive\" = true ";

                selectQueryBuilder.Append(selectQuery);
                selectQueryBuilder.Append(initialFilterCondition);

                countQueryBuilder.Append(countQuery);
                countQueryBuilder.Append(initialFilterCondition);
                

                if (string.IsNullOrEmpty(filter.FirstName) == false)
                {
                    filterQueryBuilder.Append($" AND u.\"FirstName\" LIKE '{filter.FirstName}%'");
                }

                if (string.IsNullOrEmpty(filter.LastName) == false)
                {
                    filterQueryBuilder.Append($" AND u.\"LastName\" LIKE '{filter.LastName}%'");
                }


                if (filter.Counties.Count != 0)
                {
                    filterQueryBuilder.Append("AND u.\"CountyId\" IN (");

                    for (int i = 0; i < filter.Counties.Count; i++)
                    {
                        filterQueryBuilder.Append($"'{filter.Counties[i]}'");

                        if (i != filter.Counties.Count - 1)
                        {
                            filterQueryBuilder.Append(",");
                        }
                    }

                    filterQueryBuilder.Append(") ");
                }

                if (filter.StudyAreas.Count != 0)
                {
                    filterQueryBuilder.Append("AND s.\"StudyAreaId\" IN (");

                    for (int counter = 0; counter < filter.StudyAreas.Count; counter++)
                    {
                        filterQueryBuilder.Append($"'{filter.StudyAreas[counter]}'");

                        if (counter != filter.StudyAreas.Count - 1)
                        {
                            filterQueryBuilder.Append(",");
                        }

                    }
                    filterQueryBuilder.Append(") ");

                }



                string sortingQuery = $" ORDER BY u.\"{sorting.SortBy}\" {sorting.SortOrder} ";
                string pagingQuery = $" LIMIT {paging.PageSize} OFFSET {(paging.CurrentPage - 1) * paging.PageSize}";

                selectQueryBuilder.Append(filterQueryBuilder.ToString());
                selectQueryBuilder.Append(sortingQuery);
                selectQueryBuilder.Append(pagingQuery);


                string sql = selectQueryBuilder.ToString();

                NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);

                NpgsqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        Student student = ReadStudent(dr);
                        students.Add(student);
                    }
                }
                dr.Close();
                countQueryBuilder.Append(filterQueryBuilder.ToString());
                string s = countQueryBuilder.ToString();
                NpgsqlCommand countCommand = new NpgsqlCommand(countQueryBuilder.ToString(), connection);
                var countScalar = await countCommand.ExecuteScalarAsync();
                totalCount = Convert.ToInt32(countScalar);
                
                connection.Close();
            }

            pagedStudents.Data = students;
            pagedStudents.DatabaseRecordsCount = totalCount;
            pagedStudents.LastPage = Convert.ToInt32(pagedStudents.DatabaseRecordsCount / paging.PageSize) + (pagedStudents.DatabaseRecordsCount % paging.PageSize != 0 ? 1 : 0);

            return pagedStudents;
        }
        
        private Student ReadStudent(NpgsqlDataReader dr)
        {
            string id = dr.GetString(dr.GetOrdinal("Id"));
            string firstName = dr.GetString(dr.GetOrdinal("FirstName"));
            string lastName = dr.GetString(dr.GetOrdinal("LastName"));
            string email = dr.GetString(dr.GetOrdinal("Email"));
            string phoneNumber = dr.GetString(dr.GetOrdinal("PhoneNumber"));
            string address = dr.GetString(dr.GetOrdinal("Address"));
            string description = dr.GetString(dr.GetOrdinal("Description"));
            DateTime dateCreated = dr.GetDateTime(dr.GetOrdinal("DateCreated"));
            DateTime dateUpdated = dr.GetDateTime(dr.GetOrdinal("DateUpdated"));
            Guid countyId = dr.GetGuid(dr.GetOrdinal("CountyId"));
            bool isActive = dr.GetBoolean(dr.GetOrdinal("IsActive"));
            StudyArea studyArea = new StudyArea();

            return new Student(id, firstName, lastName, email, phoneNumber, address, description, dateCreated, dateUpdated, countyId, isActive, studyArea);
        }

        public async Task<Student> GetStudentByIdAsync(string id)
        {
            Student student = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.Name))
            {
                string query = @"
                SELECT s.*, u.*, sa.""Name"" AS StudyAreaName
                FROM public.""Student"" s
                INNER JOIN public.""StudyArea"" sa ON s.""StudyAreaId"" = sa.""Id"" inner join dbo.""AspNetUsers"" u on s.""Id"" = u.""Id""
                WHERE s.""Id"" = @Id";


                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();

                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows && await reader.ReadAsync())
                {
                    student = ReadStudent(reader);
                }
            }
            return student;
        }

        public async Task<int> PostAsync(Student student)
        {
            return 1;
        }

        public async Task<int> DeleteAsync(string id)
        {
            return 1;
        }
        public async Task<int> PutAsync(string id,Student student)
        {
            return 1;
        }



    }


}
