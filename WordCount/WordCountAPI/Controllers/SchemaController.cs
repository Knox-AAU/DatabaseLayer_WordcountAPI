using System;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WordCount.Data;
using WordCount.JsonModels;
using WordCount.Models;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchemaController: ControllerBase
    {
        
        private readonly ArticleContext context;

        public SchemaController()
        {
            context = new ArticleContext();
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
            return Ok(context.JsonSchemas.First(schema => schema.SchemaName == schemaName));

        }

        /// <summary>
        /// Get all JSON schemas.
        /// </summary>
        [HttpGet]
        [Route("/[controller]")]
        public IActionResult GetAllSchemas()
        {
            return Ok(context.JsonSchemas);
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

            JsonSchemaDataModel schemaData = CreateJsonModel(jsonInput);

            if (schemaData == null)
            {
                // 400 Unprocessable entity 
                return BadRequest("Wrong body syntax, cannot parse to JSON.");
            }
            
            if (context.JsonSchemas.Find(schemaData.SchemaName) != null)
            {
                // 403 forbidden
                return Forbid("Duplicate value.");
            }

            context.JsonSchemas.Add(new JsonSchemaModel()
            {
                SchemaName = schemaData.SchemaName,
                JsonString = schemaData.SchemaBody.GetRawText()
            });                
            context.SaveChanges();

            return Ok();
        }

        private JsonSchemaDataModel CreateJsonModel(JsonElement jsonInput)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };
            string jsonString = String.Empty;
            JsonSchemaDataModel schemaData = null;
            
            try
            {
                jsonString = jsonInput.GetRawText();
                schemaData = JsonSerializer.Deserialize<JsonSchemaDataModel>(jsonString, options);
            }
            catch (JsonException)
            {
            }

            return schemaData;
        }
    }
}