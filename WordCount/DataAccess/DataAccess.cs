using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WordCount.Models;

namespace WordCount.DataAccess
{
    public sealed class DataAccess
    {
        private readonly IConfiguration config;

        public DataAccess(IConfiguration config)
        {
            this.config = config;
        }
        
        public IEnumerable<WordNameModel> GetWords()
        {
            const string connectionStringName = "WordCountDb";

            string connectionString = GetConnectionString(connectionStringName);

            Console.WriteLine(connectionString);
            
            using (NpgsqlConnection connection = new(connectionString))
            {
                IEnumerable<WordNameModel> output = connection.Query<WordNameModel>("SELECT * FROM wordlist");
                return output;
            }
        }

        private string GetConnectionString(string name)
        {
            return config.GetValue<string>($"ConnectionStrings:{name}");
        }
    }
}