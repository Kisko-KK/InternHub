﻿using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Model.Common;
using InternHub.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IConnectionString connectionString;
        public CompanyRepository(IConnectionString connectionString) {
            this.connectionString = connectionString;
        }
        public async Task<bool> DeleteAsync(string id)
        {

            using(NpgsqlConnection connection = new NpgsqlConnection(connectionString.Name))
            {
                connection.Open();

                using(NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    string companyQuery = $"UPDATE public.\"Company\" SET \"IsActive\" = false WHERE \"Id\" = '{id}'";
                    string userQuery = $"UPDATE dbo.\"AspNetUsers\" SET \"IsActive\" = false WHERE \"Id\" = '{id}'";

                    NpgsqlCommand companyCommand = new NpgsqlCommand(companyQuery, connection);
                    NpgsqlCommand userCommand = new NpgsqlCommand(userQuery, connection);

                    int companyRowsUpdated = await companyCommand.ExecuteNonQueryAsync();
                    int userRowsUpdated = await userCommand.ExecuteNonQueryAsync();

                    transaction.Commit();
                    return companyRowsUpdated > 0 || userRowsUpdated > 0;
                }
            }
        }

        public async Task<PagedList<Company>> GetAsync(Sorting sorting, Paging paging, CompanyFilter filter)
        {
            List<Company> companies = new List<Company>();
            int recordsCount = 0;

            StringBuilder selectQueryBuilder = new StringBuilder();
            StringBuilder countQueryBuilder = new StringBuilder();
            StringBuilder filterQueryBuilder = new StringBuilder();


            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString.Name))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM public.\"Company\" INNER JOIN dbo.\"AspNetUsers\" ON public.\"Company\".\"Id\" = dbo.\"AspNetUsers\".\"Id\" ";
                string countQuery = "SELECT COUNT(*) FROM public.\"Company\"";

                string filterQuery = " WHERE 1=1 ";
                string sortingQuery = $" ORDER BY public.\"Company\".\"{sorting.SortBy}\" {sorting.SortOrder} ";
                string pagingQuery = $" LIMIT {paging.PageSize} OFFSET {(paging.CurrentPage - 1) * paging.PageSize}";

                
                filterQueryBuilder.Append(filterQuery);
                if (filter.IsActive == true)
                {
                    filterQueryBuilder.Append($" AND public.\"Company\".\"IsActive\" = true");
                }
                else
                {
                    filterQueryBuilder.Append($" AND public.\"Company\".\"IsActive\" = false");
                }
                if (filter.IsAccepted == true)
                {
                    filterQueryBuilder.Append($" AND public.\"Company\".\"IsAccepted\" = true");
                }
                else
                {
                    filterQueryBuilder.Append($" AND public.\"Company\".\"IsAccepted\" = false");
                }


                selectQueryBuilder.Append(selectQuery);
                selectQueryBuilder.Append(filterQueryBuilder.ToString());
                selectQueryBuilder.Append(sortingQuery);
                selectQueryBuilder.Append(pagingQuery);

                NpgsqlCommand selectCommand = new NpgsqlCommand(selectQueryBuilder.ToString(), connection);
                NpgsqlDataReader selectReader = await selectCommand.ExecuteReaderAsync();
                while (selectReader.Read())
                {
                    Company company = new Company();
                    company.Name = (string)selectReader["Name"];
                    company.Address = (string)selectReader["Address"];
                    company.Website = (string)selectReader["Website"];
                    company.Id = (string)selectReader["Id"];
                    companies.Add(company);
                }
                selectReader.Close();

                countQueryBuilder.Append(countQuery);
                countQueryBuilder.Append(filterQueryBuilder.ToString());

                NpgsqlCommand countCommand = new NpgsqlCommand(countQueryBuilder.ToString(), connection);
                var countScalar = await countCommand.ExecuteScalarAsync();
                recordsCount = Convert.ToInt32(countScalar);

            }
            PagedList<Company> pagedList = new PagedList<Company>
            {
                Data = companies,
                CurrentPage = paging.CurrentPage,
                DatabaseRecordsCount = recordsCount,
                LastPage = Convert.ToInt32(recordsCount / paging.PageSize) + (recordsCount % paging.PageSize != 0 ? 1 : 0),
                PageSize = paging.PageSize
            };
            
            return pagedList;
        }

        public async Task<Company> GetAsync(string id)
        {
            return await GetCompanyAsync(id);
        }

        

        public async Task<bool> PostAsync(Company company)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString.Name))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertUserQuery = "INSERT INTO dbo.\"AspNetUsers\" (\"Id\", \"FirstName\", \"LastName\", \"Address\", \"Description\", \"CountyId\", \"IsActive\", \"DateCreated\", \"DateUpdated\", \"Email\", \"EmailConfirmed\", \"PasswordHash\", \"SecurityStamp\", \"PhoneNumber\", \"PhoneNumberConfirmed\", \"TwoFactorEnabled\", \"LockoutEndDateUtc\", \"LockoutEnabled\", \"AccessFailedCount\", \"UserName\") VALUES (@id, @firstname, @lastname, @address, @description, @countyId, true, @dateCreated, @dateUpdated, '@email', false, 'hashed_password', 'security_stamp', '', false, false, null, false, 0, @username);";
                        string insertCompanyQuery = "INSERT INTO public.\"Company\" (\"Id\", \"Name\", \"Website\", \"IsAccepted\", \"DateCreated\", \"DateUpdated\", \"IsActive\") VALUES(@id, @name, @website, false, @dateCreated, @dateUpdated, true);";

                        NpgsqlCommand userCommand = new NpgsqlCommand(insertUserQuery, connection);
                        NpgsqlCommand companyCommand = new NpgsqlCommand(insertCompanyQuery, connection);

                        NpgsqlCommand companyInsertCommand = new NpgsqlCommand(insertCompanyQuery, connection, transaction);
                        NpgsqlCommand userInsertCommand = new NpgsqlCommand(insertUserQuery, connection, transaction);
                        
                        userInsertCommand.Parameters.AddWithValue("@id", company.Id);
                        userInsertCommand.Parameters.AddWithValue("@firstname", company.FirstName);
                        userInsertCommand.Parameters.AddWithValue("@lastname", company.LastName);
                        userInsertCommand.Parameters.AddWithValue("@address", company.Address);
                        userInsertCommand.Parameters.AddWithValue("@description", company.Description);
                        userInsertCommand.Parameters.AddWithValue("@countyId", company.CountyId);
                        userInsertCommand.Parameters.AddWithValue("@dateCreated", company.DateCreated);
                        userInsertCommand.Parameters.AddWithValue("@dateUpdated", company.DateUpdated);
                        userInsertCommand.Parameters.AddWithValue("@email", company.Email);
                        userInsertCommand.Parameters.AddWithValue("@username", company.Email);

                        
                        companyInsertCommand.Parameters.AddWithValue("@id", company.Id);
                        companyInsertCommand.Parameters.AddWithValue("@name", company.Name);
                        companyInsertCommand.Parameters.AddWithValue("@website", company.Website);
                        companyInsertCommand.Parameters.AddWithValue("@dateCreated", company.DateCreated);
                        companyInsertCommand.Parameters.AddWithValue("@dateUpdated", company.DateUpdated);

                        await userInsertCommand.ExecuteNonQueryAsync();
                        await companyInsertCommand.ExecuteNonQueryAsync();

                        transaction.Commit();
                        return true;
                        
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

                
            }

        }

        public async Task<bool> PutAsync(Company company)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString.Name))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string updateCompanySql = "UPDATE \"Company\" SET \"Website\" = @website, \"Name\" = @name WHERE \"Id\" = @id";

                        string updateUserSql = "UPDATE dbo.\"AspNetUsers\" SET \"FirstName\" = @firstname, \"LastName\" = @lastname, \"Address\" = @address, \"Description\" = @description, \"Email\" = @email WHERE \"Id\" = @id";

                        using (NpgsqlCommand updateCompanyCommand = new NpgsqlCommand(updateCompanySql, connection, transaction))
                        using (NpgsqlCommand updateUserCommand = new NpgsqlCommand(updateUserSql, connection, transaction))
                        {
                            updateCompanyCommand.Parameters.AddWithValue("@website", company.Website);
                            updateCompanyCommand.Parameters.AddWithValue("@name", company.Name);
                            updateCompanyCommand.Parameters.AddWithValue("@id", company.Id);

                            updateUserCommand.Parameters.AddWithValue("@firstName", company.FirstName);
                            updateUserCommand.Parameters.AddWithValue("@lastName", company.LastName);
                            updateUserCommand.Parameters.AddWithValue("@id", company.Id);
                            updateUserCommand.Parameters.AddWithValue("@description", company.Description);
                            updateUserCommand.Parameters.AddWithValue("@address", company.Address);
                            updateUserCommand.Parameters.AddWithValue("@email", company.Email);

                            await updateCompanyCommand.ExecuteNonQueryAsync();
                            await updateUserCommand.ExecuteNonQueryAsync();

                            transaction.Commit();

                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }



        public async Task<Company> GetCompanyAsync(string id)
        {
            Company company = new Company();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString.Name))
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.\"Company\" INNER JOIN dbo.\"AspNetUsers\" ON public.\"Company\".\"Id\" = dbo.\"AspNetUsers\".\"Id\" WHERE \"Company\".\"Id\" = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    reader.Read();

                    company.Name = (string)reader["Name"];
                    company.Address = (string)reader["Address"];
                    company.Website = (string)reader["Website"];
                    company.Id = (string)reader["Id"];

                    reader.Close();
                }
                return company;
            }catch (Exception) { return null; }
        }

        public async Task<bool> AcceptAsync(string id)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString.Name))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand("UPDATE \"Company\" SET \"IsAccepted\" = true WHERE \"Id\" = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            
        }

        
    }
}
