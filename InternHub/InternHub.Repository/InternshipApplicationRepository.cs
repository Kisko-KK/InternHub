using InternHub.Common;
using InternHub.Model;
using InternHub.Model.Common;
using InternHub.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Threading.Tasks;

namespace InternHub.Repository
{
    public class InternshipApplicationRepository : IInternshipApplicationRepository
    {
        IConnectionString _connectionString;

        public InternshipApplicationRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<PagedList<InternshipApplication>> GetAllInternshipApplicationsAsync(Paging paging, Sorting sorting, InternshipApplicationFilter internshipApplicationFilter)
        {
            PagedList<InternshipApplication> pagedList = new PagedList<InternshipApplication>();
            //umjesto where 1=1 isactive=1
            if (paging == null) paging = new Paging();
            if (sorting == null) sorting = new Sorting();

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.Name))
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.Parameters.AddWithValue("@pageSize", paging.PageSize);
                command.Parameters.AddWithValue("@skip", (paging.CurrentPage - 1) * paging.PageSize);

                string sortBy = (sorting.SortBy.ToLower() == "Id".ToLower() ? "" : "ia.") + "\"" + sorting.SortBy + "\"";

                List<string> parameters = new List<string>();

                if (internshipApplicationFilter != null)
                {
                    if (internshipApplicationFilter.StateName != null)
                    {
                        parameters.Add("(sta.\"Name\") LIKE @name");
                        command.Parameters.AddWithValue("@name", "%" + internshipApplicationFilter.StateName.ToLower() + "%");
                    }
                    if (internshipApplicationFilter.InternshipName != null)
                    {
                        command.Parameters.AddWithValue("(i.\"Name\") LIKE @name");
                        command.Parameters.AddWithValue("@name", "%" + internshipApplicationFilter.StateName.ToLower() + "%");
                    }


                }
                string selectQuery = "SELECT * FROM \"InternshipApplication\" ia INNER JOIN \"Student\" s ON ia.\"StudentId\" = s.\"Id\" INNER JOIN \"State\" sta ON ia.\"StateId\" = sta.\"Id\" inner join \"Internship\" i on i.\"Id\"=ia.\"InternshipId\"" +
                    (parameters.Count == 0 ? "" : "WHERE ia.\"IsActive\"=true " + string.Join(" AND ", parameters)) + $" ORDER BY ia.{sortBy} {(sorting.SortOrder.ToLower() == "asc" ? "ASC" : "DESC")} LIMIT @pageSize OFFSET @skip";

                string countQuery = "SELECT COUNT (*) FROM \"InternshipApplication\"" + (parameters.Count == 0 ? "" : " WHERE " + string.Join(" AND ", parameters));

                NpgsqlCommand countCommand = new NpgsqlCommand(countQuery, connection);

                foreach (NpgsqlParameter npgsqlParameter in command.Parameters) countCommand.Parameters.AddWithValue(npgsqlParameter.ParameterName, npgsqlParameter.Value);

                command.CommandText = selectQuery;

                await connection.OpenAsync();

                object countResult = await countCommand.ExecuteScalarAsync();

                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (countResult == null || !reader.HasRows) return null;

                while (reader.HasRows && await reader.ReadAsync())
                {
                    pagedList.Data.Add(ReadInternshipApplication(reader));
                }
                pagedList.CurrentPage = paging.CurrentPage;
                pagedList.PageSize = paging.PageSize;
                pagedList.DatabaseRecordsCount = Convert.ToInt32(countResult);
                pagedList.LastPage = Convert.ToInt32(pagedList.DatabaseRecordsCount / paging.PageSize) + (pagedList.DatabaseRecordsCount % paging.PageSize != 0 ? 1 : 0);



            }


            return pagedList;

        }

        public async Task<InternshipApplication> GetInternshipApplicationByIdAsync(Guid id)
        {

            InternshipApplication internshipApplication = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.Name))
            {
                //student, state i internship
                string query = "SELECT * FROM \"InternshipApplication\" ia INNER JOIN \"State\" sta ON ia.\"StateId\" =sta.\"Id\"  INNER JOIN \"Student\" s ON ia.\"StudentId\" =s.\"Id\"  " +
                    " INNER JOIN \"Internship\" i ON ia.\"InternshipId\"=i.\"Id\" WHERE ia.\"Id\" = @id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if(reader.HasRows && await reader.ReadAsync())
                {
                    internshipApplication = ReadInternshipApplication(reader);
                }
                return internshipApplication;

            }

        }

        public async Task<bool> PostInternshipApplicationAsync(InternshipApplication internshipApplication)
        {
            throw new NotImplementedException();
            /*bool success = false;

            using(NpgsqlConnection connection=new NpgsqlConnection(_connectionString.Name))
            {
                string applicationQuery="INSERT INTO \"InternshipApplication\" VALUES (@id,)"
            }*/
        }

        public async Task<InternshipApplication> PutInternshipApplicationAsync(Guid id, InternshipApplication internshipApplication)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteInternshipApplicationAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public InternshipApplication ReadInternshipApplication(NpgsqlDataReader reader)
        {
            try
            {
                return new InternshipApplication
                {
                    Id = (Guid)reader["Id"],
                    DateCreated = (DateTime)reader["DateCreated"],
                    DateUpdated = (DateTime)reader["DateUpdated"],
                    CreatedByUserId = (string)reader["CreatedByUserId"],
                    UpdatedByUserId = (string)reader["UpdatedByUserId"],
                    IsActive = (bool)reader["IsActive"],
                    StateId = (Guid)reader["StateId"],
                    StudentId = (string)reader["StudentId"],
                    InternshipId = (Guid)reader["InternshipId"]

                };

            }
            catch { return null; }



        }

    }
}
