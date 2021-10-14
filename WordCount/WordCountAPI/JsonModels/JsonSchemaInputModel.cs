using System.Text.Json;

namespace WordCount.JsonModels
{
    public sealed class JsonSchemaInputModel
    {
        public string SchemaName { get; set; }
        public JsonElement SchemaBody { get; set; }
    }
}