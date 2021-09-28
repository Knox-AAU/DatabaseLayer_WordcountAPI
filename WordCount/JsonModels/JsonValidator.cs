using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace KnoxDatabaseLayer3.JsonModels
{
    public sealed class JsonValidator<TIn, TOut> where TIn : IPostRoot<TOut> where TOut : class
    {
        private const string JsonSchemaFileName = "wordCounterSchema.json";
        
        public bool IsValid(string jsonString, out IEnumerable<TOut> articleData)
        {
            articleData = null;

            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string jsonSchemaString = File.ReadAllText($"{directory}/{JsonSchemaFileName}");
            
            JSchema schema = JSchema.Parse(jsonSchemaString);
            JArray test = JArray.Parse(jsonString);

            if (!test.IsValid(schema)) return false;
            
            articleData = DeserializeJsonString(jsonString);
            
            return true;
        }

        private static IEnumerable<TOut> DeserializeJsonString(string jsonString)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = false
            };
            
            IEnumerable<TOut> root = JsonSerializer.Deserialize<TIn>(jsonString).Data;

            return root;
        }
    }
}