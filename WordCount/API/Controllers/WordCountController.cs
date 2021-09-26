using Microsoft.AspNetCore.Mvc;

namespace KnoxDatabaseLayer3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : Controller
    {
        // GET
        [HttpGet]
        public void Get([FromBody] TurtleJson t)
        {
                //Steps: try to convert to json, on fail, log the exception
                    //Create file log class/service
                //insert file data into 'data layer' class
                //save data
        }
    }

}