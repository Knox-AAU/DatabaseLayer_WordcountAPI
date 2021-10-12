using Microsoft.AspNetCore.Mvc;
using WordCount.JsonModels;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : ControllerBase
    {
        private readonly GetController getController = new GetController();

        [HttpPost]
        public void Post([FromBody] string jsonInput)
        {
            // Get schema and use for validating
            if (new JsonValidator<Article[]>("").IsValid(jsonInput, out Article[] articles))
            {
                // Store in DB
            }
        }
    }
}