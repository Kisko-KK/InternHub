using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Model.Common;
using InternHub.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Repository
{
    internal class StudyAreaRepository : IStudyAreaRepository
    {
        IConnectionString _connectionString;

        public StudyAreaRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<PagedList<StudyArea>> GetAllStudyAreasAsync(Paging paging, Sorting sorting, StudyAreaFilter studyAreaFilter)
        {
            PagedList<StudyArea> pagedList = new PagedList<StudyArea>();

            if (paging == null) paging = new Paging();
            if (sorting == null) sorting = new Sorting();

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.Name))
            {
                NpgsqlCommand command = new NpgsqlCommand();

                command.Connection = connection;
                command.Parameters.AddWithValue("@pageSize", paging.PageSize);
                command.Parameters.AddWithValue("@skip", (paging.CurrentPage - 1) * paging.PageSize);
                string sortBy = (sorting.SortBy.ToLower() == "Id".ToLower() ? "" : "sa.") + "\"" + sorting.SortBy + "\"";

                List<string> parameters = new List<string>();

                if (studyAreaFilter != null)
                {
                    if (studyAreaFilter.Name != null)
                    {
                        parameters.Add("(sa.\"Name\") LIKE @name");
                        command.Parameters.AddWithValue("@name", "%" + studyAreaFilter.Name.ToLower() + "%");
                    }


                }
                string selectQuery= "SELECT * FROM \"StudyArea\" sa " + (parameters.Count==0? "": "WHERE sa.\"IsActive\"=true " + string.Join("AND",parameters)) + $"ORDER BY ia.{sortBy} {(sorting.SortOrder.ToLower() == "asc" ? "ASC" : "DESC")} LIMIT @pageSize OFFSET @skip";
                string countQuery = "SELECT COUNT (*) FROM \"StudyArea\"" + (parameters.Count == 0 ? "" : " WHERE " + string.Join(" AND ", parameters));

                NpgsqlCommand countCommand = new NpgsqlCommand(countQuery, connection);

                foreach (NpgsqlParameter npgsqlParameter in command.Parameters) countCommand.Parameters.AddWithValue(npgsqlParameter.ParameterName, npgsqlParameter.Value);

                command.CommandText = selectQuery;

                await connection.OpenAsync();

                object countResult = await countCommand.ExecuteScalarAsync();

                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (countResult == null || !reader.HasRows) return null;

                while (reader.HasRows && await reader.ReadAsync())
                {
                    pagedList.Data.Add(ReadStudyArea(reader));
                }
                pagedList.CurrentPage = paging.CurrentPage;
                pagedList.PageSize = paging.PageSize;
                pagedList.DatabaseRecordsCount = Convert.ToInt32(countResult);
                pagedList.LastPage = Convert.ToInt32(pagedList.DatabaseRecordsCount / paging.PageSize) + (pagedList.DatabaseRecordsCount % paging.PageSize != 0 ? 1 : 0);


            }
            return pagedList;
        }

      

        public async Task<StudyArea> GetStudyAreaByIdAsync(Guid id)
        {
            StudyArea studyArea = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.Name))
            {
                //student, state i internship
                string query = "SELECT * FROM \"StudyArea\" sa WHERE ia.\"Id\" = @id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows && await reader.ReadAsync())
                {
                    studyArea = ReadStudyArea(reader);
                }
                return studyArea;

            }
        }

   
        public async Task<bool> PostStudyAreaByAsync(StudyArea studyArea)
        {
            bool success = false;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.Name))
            {
                connection.Open();




                string studyAreaQuery = "INSERT INTO \"StudyArea\" VALUES (@id,@name,@dateCreated,@dateUpdated,@createdByUserId,@updatedByUserId,@isActive";

                NpgsqlCommand studyAreaCommand = new NpgsqlCommand(studyAreaQuery, connection);
                studyAreaCommand.Parameters.AddWithValue("@id", studyArea.Id);
                studyAreaCommand.Parameters.AddWithValue("@name", studyArea.Name);
                studyAreaCommand.Parameters.AddWithValue("@dateCreated", studyArea.DateCreated);
                studyAreaCommand.Parameters.AddWithValue("@dateUpdated", studyArea.DateUpdated);
                studyAreaCommand.Parameters.AddWithValue("@createdByUserId", studyArea.CreatedByUserId);
                studyAreaCommand.Parameters.AddWithValue("@updatedByUserId", studyArea.UpdatedByUserId);
                studyAreaCommand.Parameters.AddWithValue("@isActive", studyArea.IsActive);
                
           

                NpgsqlTransaction transaction = connection.BeginTransaction();
                studyAreaCommand.Transaction = transaction;

                int applicationResult = await studyAreaCommand.ExecuteNonQueryAsync();

                if (applicationResult != 0)
                {
                    success = true;
                    transaction.Commit();
                }
                else { success = false; transaction.Rollback(); }

            }
            return success;
        }

       

        public async Task<bool> PutStudyAreaByAsync( StudyArea studyArea)
        {
            int numberOfAffectedRows = 0;
            using (NpgsqlConnection connection= new NpgsqlConnection (_connectionString.Name)) 
            {
                NpgsqlCommand command = new NpgsqlCommand();
                List<string> updatedValues= new List<string>();
                command.Connection= connection;
                command.Parameters.AddWithValue("@id", studyArea.Id);
                command.CommandText = "UPDATE \"StudyArea\" SET \"Name\" = @name WHERE \"Id\" = @id";

                command.Parameters.AddWithValue("@name", studyArea.Name);

                await connection.OpenAsync();
                numberOfAffectedRows=await command.ExecuteNonQueryAsync();
            }
            return numberOfAffectedRows != 0;
        }
        public async Task<bool> DeleteStudyAreaByAsync(Guid id)
        {
            bool success = false;   
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.Name))
            {
                string studyAreaQuery = "DELETE FROM \"StudyArea\" WHERE \"Id\" =@id;";
                NpgsqlCommand command= new NpgsqlCommand(studyAreaQuery,connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                NpgsqlTransaction transaction = connection.BeginTransaction();  
                command.Transaction= transaction;   
                int studyAreaResult= await command.ExecuteNonQueryAsync();
                

                if(studyAreaResult != 0) 
                {
                    success= true;  
                    await transaction.CommitAsync();
                }else { success= false; transaction.RollbackAsync(); }

            }
            return success;
        }

        public StudyArea ReadStudyArea(NpgsqlDataReader reader)
        {
            try
            {
                return new StudyArea
                {
                    Id = (Guid)reader["Id"],
                    DateCreated = (DateTime)reader["DateCreated"],
                    DateUpdated = (DateTime)reader["DateUpdated"],
                    CreatedByUserId = (string)reader["CreatedByUserId"],
                    UpdatedByUserId = (string)reader["UpdatedByUserId"],
                    IsActive = (bool)reader["IsActive"],
                    Name = (string)reader["Name"],
                    

                };

            }
            catch { return null; }



        }
    }
}
