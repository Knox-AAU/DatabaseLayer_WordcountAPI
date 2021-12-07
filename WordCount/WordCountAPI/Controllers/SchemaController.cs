using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WordCount.Controllers.JsonInputModels;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchemaController : ControllerBase
    {
        private wordcountContext ArticleContext { get; set; }

        public SchemaController()
        {
            ArticleContext = new wordcountContext();
        }
        
        /// <summary>
        /// Get JSON schema with given name.
        /// </summary>
        /// <param name="schemaName">Name of the JSON schema.</param>
        /// <returns>The schema.</returns>
        [HttpGet]
        [Route("/[controller]/{schemaName}")]
        public IActionResult GetSchema(string schemaName)
        {
            return Ok(ArticleContext.JsonSchemas.First(schema => schema.SchemaName == schemaName));
        }

        /// <summary>
        /// Get all JSON schemas.
        /// </summary>
        [HttpGet]
        [Route("/[controller]")]
        public IActionResult GetAllSchemas()
        {
            return Ok(ArticleContext.JsonSchemas);
        }

        /// <summary>
        /// Method for posting a JSON schema to the database which can then be used later for validation of input.
        /// </summary>
        /// <param name="jsonInput">
        /// A JSON element consisting of the keys "schemaName" and "schemaBody". <br/>
        /// The value of key "schemaName" is the primary key for the given schema. <br/>
        /// The value of key "schemaBody" is the schema itself.
        /// </param>
        [HttpPost]
        [Route("/[controller]")]
        public IActionResult PostJsonSchema([FromBody] JsonElement jsonInput)
        {
            JsonSchemaInputModel schemaData = CreateJsonModel(jsonInput);

            if (schemaData == null)
            {
                // 400 Unprocessable entity
                return BadRequest("Wrong body syntax, cannot parse to JSON.");
            }

            if (ArticleContext.JsonSchemas.Find(schemaData.SchemaName) != null)
            {
                // 403 forbidden
                return Forbid("Duplicate value.");
            }

            ArticleContext.JsonSchemas.Add(new JsonSchema
            {
                SchemaName = schemaData.SchemaName,
                JsonString = schemaData.SchemaBody.GetRawText()
            });

            ArticleContext.SaveChanges();

            return Ok();
        }

        private JsonSchemaInputModel CreateJsonModel(JsonElement jsonInput)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };
            string jsonString = string.Empty;
            JsonSchemaInputModel schemaData = null;

            try
            {
                jsonString = jsonInput.GetRawText();
                schemaData = JsonSerializer.Deserialize<JsonSchemaInputModel>(jsonString, options);
            }
            catch (JsonException)
            {
            }

            return schemaData;
        }
    }
}