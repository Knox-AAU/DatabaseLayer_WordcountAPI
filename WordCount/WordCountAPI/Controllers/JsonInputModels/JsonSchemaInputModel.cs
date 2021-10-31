using System.Text.Json;

namespace WordCount.Controllers.JsonInputModels
{
    public sealed class JsonSchemaInputModel
    {
        public string SchemaName { get; set; }
        public JsonElement SchemaBody { get; set; }
    }
}