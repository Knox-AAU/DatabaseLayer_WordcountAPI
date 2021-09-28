using System.Text.Json;
using KnoxDatabaseLayer3.JsonUtility;
using Microsoft.AspNetCore.Mvc;

namespace KnoxDatabaseLayer3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : Controller
    {
        [HttpGet]
        [Route("/[controller]/{id:int}")]
        public ArticleData Get([FromBody] int id)
        {
            return null;
            //Steps: try to convert to json, on fail, log the exception
            //Create file log class/service
            //insert file data into 'data layer' class
            //save data
        }
        
        [HttpGet]
        [Route("/[controller]")]
        public ArticleData GetAll()
        {
            // TODO: Get all word count data and return it
            return null;
        }

        [HttpPost]
        public void Post([FromBody] string jsonInput)
        {
            // TODO: Validate schema here

            ArticleData[] articles = JsonSerializer.Deserialize<WordCountPostRoot>(jsonInput).Articles;
            
            // TODO: Store data in database
        }
    }
}