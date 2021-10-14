using System.Text.Json;

namespace WordCount.JsonModels
{
    public sealed class JsonSchemaDataModel
    {
        public string SchemaName { get; set; }
        public JsonElement SchemaBody { get; set; }
    }
}