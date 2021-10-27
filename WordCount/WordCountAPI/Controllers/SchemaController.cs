using System;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WordCount.Data;
using WordCount.Data.Models;
using WordCount.DataAccess;
using WordCount.JsonModels;
using WordCount.Models;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchemaController: ControllerBase
    {
        
        private UnitOfWork unitOfWork;
        
        public SchemaController()
        {
            unitOfWork = new UnitOfWork(new ArticleContext());
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
            return Ok(unitOfWork.SchemaRepository.Find(schema => schema.SchemaName == schemaName));
        }

        /// <summary>
        /// Get all JSON schemas.
        /// </summary>
        [HttpGet]
        [Route("/[controller]")]
        public IActionResult GetAllSchemas()
        {
            return Ok(unitOfWork.SchemaRepository.All());
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
            
            if (unitOfWork.SchemaRepository.GetById(schemaData.SchemaName) != null)
            {
                // 403 forbidden
                return Forbid("Duplicate value.");
            }

            unitOfWork.SchemaRepository.Insert(new JsonSchemaModel()
            {
                SchemaName = schemaData.SchemaName,
                JsonString = schemaData.SchemaBody.GetRawText()
            });
            return Ok();
        }

        private JsonSchemaInputModel CreateJsonModel(JsonElement jsonInput)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };
            string jsonString = String.Empty;
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