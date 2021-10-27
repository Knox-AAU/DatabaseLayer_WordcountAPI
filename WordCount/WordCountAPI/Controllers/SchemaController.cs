using System.Linq;
using System.Net;
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

        /// <summary>
        /// Get JSON schema with given name.
        /// </summary>
        /// <param name="schemaName">Name of the JSON schema.</param>
        /// <returns>The schema.</returns>
        [HttpGet]
        [Route("/[controller]/{schemaName}")]
        public IActionResult GetSchema(string schemaName)
        {
            /*
            WordCountDbContext dbContext = new();
            return Ok(dbContext.JsonSchemas.First(schema => schema.SchemaName == schemaName));*/
            return null;
        }

        /// <summary>
        /// Get all JSON schemas.
        /// </summary>
        [HttpGet]
        [Route("/[controller]")]
        public IActionResult GetAllSchemas()
        {
            /*
            WordCountDbContext dbContext = new();
            return Ok(dbContext.JsonSchemas);*/
            return null;

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

            ArticleContext dbContext = new();
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            string jsonString = jsonInput.GetRawText();
            JsonSchemaDataModel schemaData = JsonSerializer.Deserialize<JsonSchemaDataModel>(jsonString, options);

            if (schemaData == null)
            {
                // 400 Unprocessable entity 
                return BadRequest("Wrong body syntax, does not follow schema.");
            }
            
            JsonSchemaModel model = new()
            {
                SchemaName = schemaData.SchemaName,
                JsonString = schemaData.SchemaBody.GetRawText()
            };

            if (dbContext.JsonSchemas.Find(model.SchemaName) != null)
            {
                // 403 forbidden
                return Forbid("Duplicate value.");
            }
            
            dbContext.JsonSchemas.Add(model);                
            dbContext.SaveChanges();
    
            // status 200 OK
            return Ok();
        }

    }
}