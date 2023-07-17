using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Model.Common;
using InternHub.Repository.Common;
using Npgsql;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InternHub.Repository
{
    public class InternshipRepository : IInternshipRepository
    {

        private readonly IConnectionString connectionString;
        public InternshipRepository(IConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString.Name))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    string companyQuery = $"UPDATE public.\"Internship\" SET \"IsActive\" = false WHERE \"Id\" = '{id}'";

                    NpgsqlCommand command = new NpgsqlCommand(companyQuery, connection);

                    int rowsUpdated = await command.ExecuteNonQueryAsync();

                    transaction.Commit();
                    return rowsUpdated > 0;
                }
            }
        }

        public Task<PagedList<Internship>> GetAsync(Sorting sorting, Paging paging, CompanyFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Internship> GetAsync(Guid id)
        {
            return await GetInternshipAsync(id);
        }

        public async Task<Internship> GetInternshipAsync(Guid id)
        {
            Internship internship = new Internship();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString.Name))
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand("SELECT i.\"Id\" AS \"InternshipId\", i.\"Name\" AS \"InternshipName\", i.\"Description\", i.\"Address\", i.\"StartDate\", i.\"EndDate\", i.\"CreatedByUserId\" AS \"InternshipCreatedByUserId\", i.\"UpdatedByUserId\" AS \"InternshipUpdatedByUserId\", i.\"DateCreated\" AS \"InternshipDateCreated\", i.\"DateUpdated\" AS \"InternshipDateUpdated\", i.\"IsActive\" AS \"InternshipIsActive\", c.\"Id\" AS \"CompanyId\", c.\"Name\" AS \"CompanyName\", c.\"Website\",  c.\"IsAccepted\", c.\"DateCreated\" AS \"CompanyDateCreated\", c.\"DateUpdated\" AS \"CompanyDateUpdated\", c.\"IsActive\" AS \"CompanyIsActive\", sa.\"Id\" AS \"StudyAreaId\", sa.\"Name\" AS \"StudyAreaName\", sa.\"DateCreated\" AS \"StudyAreaDateCreated\", sa.\"DateUpdated\" AS \"StudyAreaDateUpdated\", sa.\"CreatedByUserId\" AS \"StudyAreaCreatedByUserId\", sa.\"UpdatedByUserId\" AS \"StudyAreaUpdatedByUserId\", sa.\"IsActive\" AS \"StudyAreaIsActive\" FROM public.\"Internship\" i INNER JOIN public.\"StudyArea\" sa ON i.\"StudyAreaId\" = sa.\"Id\" INNER JOIN public.\"Company\" c ON i.\"CompanyId\" = c.\"Id\" where i.\"Id\" = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    reader.Read();

                    internship.StudyAreaId = (Guid)reader["StudyAreaId"];
                    internship.Name = (string)reader["InternshipName"];
                    internship.Address = (string)reader["Address"];
                    internship.Description = (string)reader["Description"];

                    internship.StudyArea = new StudyArea();
                    internship.StudyArea.Id = (Guid)reader["StudyAreaId"];
                    internship.StudyArea.Name = (string)reader["StudyAreaName"];

                    internship.Company = new Company();
                    internship.Company.Id = (string)reader["CompanyId"];
                    internship.Company.Name = (string)reader["CompanyName"];
                    internship.Company.Website = (string)reader["Website"];

                    reader.Close();
                }
                return internship;
            }
            catch (Exception) { return null; }
        }

        public async Task<bool> PostAsync(Internship internship)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString.Name))
            {
                connection.Open();

                string insertQuery = "INSERT INTO public.\"Internship\" " +
                     "(\"Id\", \"StudyAreaId\", \"CompanyId\", \"Name\", \"Description\", \"Address\", \"StartDate\", \"EndDate\", \"CreatedByUserId\", \"UpdatedByUserId\", \"DateCreated\", \"DateUpdated\", \"IsActive\") " +
                     "VALUES (@Id, @StudyAreaId, @CompanyId, @Name, @Description, @Address, @StartDate, @EndDate, @CreatedByUserId, @UpdatedByUserId, @DateCreated, @DateUpdated, @IsActive)";

                NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Id", internship.Id);
                command.Parameters.AddWithValue("@StudyAreaId", internship.StudyAreaId);
                command.Parameters.AddWithValue("@CompanyId", internship.CompanyId);
                command.Parameters.AddWithValue("@Name", internship.Name);
                command.Parameters.AddWithValue("@Description", internship.Description);
                command.Parameters.AddWithValue("@Address", internship.Address);
                command.Parameters.AddWithValue("@StartDate", internship.StartDate);
                command.Parameters.AddWithValue("@EndDate", internship.EndDate);
                command.Parameters.AddWithValue("@CreatedByUserId", internship.CreatedByUserId);
                command.Parameters.AddWithValue("@UpdatedByUserId", internship.UpdatedByUserId);
                command.Parameters.AddWithValue("@DateCreated", internship.DateCreated);
                command.Parameters.AddWithValue("@DateUpdated", internship.DateUpdated);
                command.Parameters.AddWithValue("@IsActive", true);

                await command.ExecuteNonQueryAsync();
            }
            return true;
        }

        public Task<bool> PutAsync(Internship internship)
        {
            throw new NotImplementedException();
        }
    }
}
