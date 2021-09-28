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
            schema = JSchema.Parse(jsonSchemaString);
        }
        
        public bool IsObjectValid(string jsonString, out T data)
        {
            JObject jsonObject = JObject.Parse(jsonString);

            return IsValid(jsonObject, jsonString, out data);
        }
        
        public bool IsArrayValid(string jsonString, out T data)
        {
            JArray jsonArray = JArray.Parse(jsonString);

            return IsValid(jsonArray, jsonString, out data);
        }

        private bool IsValid(JToken jToken, string jsonString, out T data)
        {
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