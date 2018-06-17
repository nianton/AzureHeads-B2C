﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GraphLite;

namespace UserMigrationApp
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection dbConnection;
        internal const string DefaultPassword = "123abC!!";

        public UserRepository(string connectionString)
        {
            dbConnection = new SqlConnection(connectionString);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            // This query is intented to run against an instance of WorldWideImporters
            // which is a MS SQL Server sample database. Replace with different logic to
            // access local users.

            var query = @"
            SELECT *, JSON_VALUE([CustomFields],'$.Title') AS Title 
            FROM [Application].[People] 
            WHERE LogonName NOT IN (
                SELECT LogonName
                FROM [Application].[People] 
                GROUP BY LogonName
                HAVING COUNT(*) > 1)";

            var persons = await dbConnection.QueryAsync<Person>(query);
            var users = persons.Select(p =>
            {
                var user = new User
                {
                    SignInNames = new List<SignInName>
                    {
                         new SignInName()
                         {
                             Type = "emailAddress",
                             Value = p.LogonName
                         }
                    },
                    JobTitle = p.Title,
                    DisplayName = p.FullName,
                    CreationType = "LocalAccount",
                    AccountEnabled = true,
                    PasswordProfile = new PasswordProfile
                    {
                        EnforceChangePasswordPolicy = false,
                        ForceChangePasswordNextLogin = false,
                        Password = DefaultPassword
                    }
                };

                // TODO: Uncomment to set custom property already created on your AAD B2C tenant
                // var taxNumber = $"{DateTime.Now:FFFssmmHH}";
                //user.SetExtendedProperty("TaxRegistrationNumber", taxNumber);

                return user;
            }).ToList();


            return users;
        }
    }
}
