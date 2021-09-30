﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WordCount.Models;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : ControllerBase
    {
        private readonly IConfiguration config;
        
        public WordCountController(IConfiguration config)
        {
            this.config = config;
        }
        
        [HttpGet]
        public IEnumerable<WordNameModel> Get()
        {
            IEnumerable<WordNameModel> data = new DataAccess.DataAccess(config).GetWords();
            return data;
        }
    }
}