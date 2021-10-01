using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WordCount.Models;

namespace WordCount.Data
{
    public sealed class DataAccess
    {
        private readonly IConfiguration config;

        public DataAccess(IConfiguration config)
        {
            this.config = config;
        }
        
        public List<string> GetWords()
        {
            const string connectionStringName = "WordCountDb";

            string connectionString = GetConnectionString(connectionStringName);

            var lst = new List<string>();
            new WordCountContext().wordlist.Take(100).ToList().ForEach(el => lst.Add(el.wordname));
            return lst;
            
            
        }

        private string GetConnectionString(string name)
        {
            string database = config.GetValue<string>("database:database");
            string host = config.GetValue<string>("database:host");
            string password = config.GetValue<string>("database:password");
            string username = config.GetValue<string>("database:username");
            string port = config.GetValue<string>("database:port");

            return $"";
        }
    }
}