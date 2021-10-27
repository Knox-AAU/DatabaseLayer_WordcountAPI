using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace WordCount.JsonModels
{
    public sealed class JsonValidator<T> where T : class 
    {
        private readonly JSchema schema;
        public JsonValidator(string jsonSchemaString)
        {
            Console.WriteLine(jsonSchemaString);
            schema = JSchema.Parse(jsonSchemaString);
        }
        
        public bool IsValid(string jsonString, out T data)
        {
            JToken jToken = JToken.Parse(jsonString);

            data = null;
            
            if (!jToken.IsValid(schema)) return false;
            
            data = DeserializeJsonString(jsonString);
            
            return true;
        }
        
        private static T DeserializeJsonString(string jsonString)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };
            
            return JsonSerializer.Deserialize<T>(jsonString, options);
        }
    }
}