using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace KnoxDatabaseLayer3.JsonModels
{
    public sealed class SampleJsonExample
    {
        private const string JsonFileName = "sampleWordCount.json";
        private const string JsonSchemaFileName = "wordCounterSchema.json";
        
        public void Run()
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string jsonString = File.ReadAllText($"{directory}/{JsonFileName}");
            string jsonSchemaString = File.ReadAllText($"{directory}/{JsonSchemaFileName}");
            
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = false
            };

            JSchema schema = JSchema.Parse(jsonSchemaString);
            
            JArray test = JArray.Parse(jsonString);
            bool valid = test.IsValid(schema);

            Console.WriteLine("Valid + " + valid);
        }
    }
}