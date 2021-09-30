using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using WordCount.Models;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WordNameModel> Get()
        {
            IEnumerable<WordNameModel> data = new DataAccess().GetWords();
            return data;
        }
    }
}