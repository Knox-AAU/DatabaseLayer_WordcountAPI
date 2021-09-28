using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Npgsql;

namespace WordCount
{
    public class DataAccess
    {
        public IEnumerable<WordList> GetWords()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Helper.CnnVal("wordCount")))
            {
                IEnumerable<WordList> output = connection.Query<WordList>("SELECT * FROM wordlist");
                return output;
            }
        }
    }
}