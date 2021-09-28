using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace KnoxDatabaseLayer3.JsonModels
{
    public sealed class JsonValidator<T> where T : class 
    {
        private readonly string jsonSchemaString;
        
        public JsonValidator(string jsonSchemaString)
        {
            this.jsonSchemaString = jsonSchemaString;
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

            JSchema schema = JSchema.Parse(jsonSchemaString);

            if (!jToken.IsValid(schema)) return false;
            
            data = DeserializeJsonString(jsonString);
            
            return true;
        }

        private static T DeserializeJsonString(string jsonString)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}