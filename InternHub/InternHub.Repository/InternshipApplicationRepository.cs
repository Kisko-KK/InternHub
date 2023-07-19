﻿using InternHub.Common;
using InternHub.Common.Filter;
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

                string sortBy = (sorting.SortBy.ToLower() == "Id".ToLower() ? "s" : "ia") + ".\"" + sorting.SortBy + "\"";

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
                    (parameters.Count == 0 ? "" : "WHERE ia.\"IsActive\"=true " + string.Join(" AND ", parameters)) + $" ORDER BY {sortBy} {(sorting.SortOrder.ToLower() == "asc" ? "ASC" : "DESC")} LIMIT @pageSize OFFSET @skip";

                string countQuery = "SELECT COUNT (*) FROM \"InternshipApplication\"" + (parameters.Count == 0 ? "" : " WHERE " + string.Join(" AND ", parameters));

                NpgsqlCommand countCommand = new NpgsqlCommand(countQuery, connection);

                foreach (NpgsqlParameter npgsqlParameter in command.Parameters) countCommand.Parameters.AddWithValue(npgsqlParameter.ParameterName, npgsqlParameter.Value);

                command.CommandText = selectQuery;

                await connection.OpenAsync();

                object countResult = await countCommand.ExecuteScalarAsync();

                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (countResult == null) return pagedList;

                while (reader.HasRows && await reader.ReadAsync())
                {
                    InternshipApplication internshipApplication = ReadInternshipApplication(reader);
                    if(internshipApplication != null) pagedList.Data.Add(internshipApplication);
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
            
            bool success = false;

            using(NpgsqlConnection connection=new NpgsqlConnection(_connectionString.Name))
            {
                connection.Open();

                string applicationQuery = "INSERT INTO \"InternshipApplication\" VALUES (@id,@dateCreated,@dateUpdated,@createdByUserId,@updatedByUserId,@isActive,@stateId" +
                    "@studentId,@internshipId);";

                NpgsqlCommand applicationCommand = new NpgsqlCommand(applicationQuery, connection); 
                applicationCommand.Parameters.AddWithValue("@id",internshipApplication.Id);
                applicationCommand.Parameters.AddWithValue("@dateCreated", internshipApplication.DateCreated);
                applicationCommand.Parameters.AddWithValue("@dateUpdated", internshipApplication.DateUpdated);
                applicationCommand.Parameters.AddWithValue("@createdByUserId", internshipApplication.CreatedByUserId);
                applicationCommand.Parameters.AddWithValue("@updatedByUserId", internshipApplication.UpdatedByUserId);
                applicationCommand.Parameters.AddWithValue("@isActive", internshipApplication.IsActive);
                applicationCommand.Parameters.AddWithValue("@stateId", internshipApplication.StateId);
                applicationCommand.Parameters.AddWithValue("@studentId", internshipApplication.StudentId);
                applicationCommand.Parameters.AddWithValue("@internshipId", internshipApplication.InternshipId);

                int applicationResult = await applicationCommand.ExecuteNonQueryAsync();

                success = applicationResult != 0;
            }
            return success;
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
                    InternshipId = (Guid)reader["InternshipId"],
                };
            }
            catch { return null; }
        }
    }
}