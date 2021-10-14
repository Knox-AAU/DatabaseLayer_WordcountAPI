using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WordCount.DataAccess;
using WordCount.JsonModels;
using WordCount.Models;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchemaController : ControllerBase
    {
        private readonly IRepository<JsonSchema, string> repository;

        public SchemaController(IRepository<JsonSchema, string> repos)
        {
            repository = repos;
        }

        /// <summary>
        ///     Method for posting a JSON schema to the database which can then be used later for validation of input.
        /// </summary>
        /// <param name="jsonInput">
        ///     A JSON element consisting of the keys "schemaName" and "schemaBody". <br />
        ///     The value of key "schemaName" is the primary key for the given schema. <br />
        ///     The value of key "schemaBody" is the schema itself.
        /// </param>
        [HttpPost]
        [Route("/[controller]")]
        public IActionResult PostJsonSchema([FromBody] JsonElement jsonInput)
        {
            JsonSerializerOptions options = new() {PropertyNameCaseInsensitive = true};

            string jsonString = jsonInput.GetRawText();
            JsonSchemaInputModel? schemaInput = JsonSerializer.Deserialize<JsonSchemaInputModel>(jsonString, options);

            if (schemaInput == null)
                // 400 Unprocessable entity 
                return new ObjectResult("Wrong body syntax, does not follow schema") {StatusCode = 400};

            JsonSchema model = new()
            {
                SchemaName = schemaInput.SchemaName, JsonString = schemaInput.SchemaBody.GetRawText()
            };

            if (repository.Find(schema => schema.PrimaryKey == model.SchemaName) == null)
                // 403 forbidden
                return new ObjectResult("Duplicate value") {StatusCode = 403};
            repository.Insert(model);

            // status 200 OK
            return new ObjectResult("Ok") {StatusCode = 200};
        }
    }
}