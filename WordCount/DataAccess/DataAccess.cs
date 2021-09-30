using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WordCount.Models;

namespace WordCount
{
    public class DataAccess
    {
        private readonly IConfiguration config;

        
        public IEnumerable<WordNameModel> GetWords()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Helper.CnnVal("WordCountDb")))
            {
                IEnumerable<WordNameModel> output = connection.Query<WordNameModel>("SELECT * FROM wordlist");
                return output;
            }
        }
    }
}