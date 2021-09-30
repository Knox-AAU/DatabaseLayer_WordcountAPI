using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WordCount.JsonModels;

namespace KnoxDatabaseLayer3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : Controller
    {
        [HttpGet]
        [Route("/[controller]/{id:int}")]
        public Article Get([FromBody] int id)
        {
            return null;
            //Steps: try to convert to json, on fail, log the exception
            //Create file log class/service
            //insert file data into 'data layer' class
            //save data
        }
        
        [HttpGet]
        [Route("/[controller]")]
        public Article GetAll()
        {
            // TODO: Get all word count data and return it
            return null;
        }

        [HttpPost]
        public void Post([FromBody] string jsonInput)
        {
            // TODO: Query schema from database and use it when validating jsonInput 
            
            if (new JsonValidator<Article[]>("").IsValid(jsonInput, out Article[] articles))
            {
                // TODO: Store articles in database
            }
        }
    }
}