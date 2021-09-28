using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Npgsql;

namespace WordCount
{
    public class DataAccess
    {
        
        public IEnumerable<WordNameModel> GetWords()
        {
            
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=1234;" +
                                                                      "Host=localhost;Port=5432;Database=wordcount;"))
            {
                IEnumerable<WordNameModel> output = connection.Query<WordNameModel>("SELECT * FROM wordlist");
                return output;
            }
        }
    }
}