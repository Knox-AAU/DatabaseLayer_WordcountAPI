using System;
using System.Collections.Generic;
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
    public class WordCountController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] JsonElement jsonElement)
        {
            string maut =
                "{ \"schemaName\": \"wordcount\", \"schemaBody\": { \"$schema\": \"http://json-schema.org/draft-07/schema\", \"default\": [], \"description\": \"The root schema comprises the entire JSON document.\", \"title\": \"The root schema\", \"type\": \"array\", \"additionalItems\": true, \"items\": { \"anyOf\": [ { \"type\": \"object\", \"required\": [ \"articletitle\", \"filepath\", \"totalwordsinarticle\", \"words\" ], \"properties\": { \"articletitle\": { \"default\": \"\", \"description\": \"The title of the article which this refers to.\", \"examples\": [ \"Stigning i underretninger\" ], \"title\": \"The articletitle schema\", \"type\": \"string\" }, \"publication\": { \"type\": \"string\", \"title\": \"The publication schema\", \"default\": \"\", \"examples\": [ \"Publication1\" ] }, \"filepath\": { \"default\": \"\", \"description\": \"Where to find this article.\", \"examples\": [ \"newsarchive/2020-08-20/01_02_thisted_dagblad_tor_s002_12_lokal____2008_202008200000_1014466314.xml\" ], \"title\": \"The filepath schema\", \"type\": \"string\" }, \"totalwordsinarticle\": { \"default\": 0, \"description\": \"Amount of tokens in \'words\'.\", \"examples\": [ 176 ], \"title\": \"The totalwordsinarticle schema\", \"type\": \"integer\" }, \"words\": { \"default\": [], \"description\": \"The BoW(bag-of-words) list of tokens and number of occurrences.\", \"title\": \"The words schema\", \"type\": \"array\", \"additionalItems\": true, \"items\": { \"anyOf\": [ { \"description\": \"A token and an associated number of occurences in the article.\", \"required\": [ \"amount\", \"word\" ], \"type\": \"object\", \"properties\": { \"amount\": { \"type\": \"integer\", \"title\": \"The amount schema\", \"examples\": [ 1 ] }, \"word\": { \"type\": \"string\", \"title\": \"The word schema\", \"examples\": [ \"THYMORS\" ] } }, \"additionalProperties\": true } ] } } }, \"additionalProperties\": true } ] } } }";
            WordCountDbContext dbContext = new();
            //var schema = dbContext.JsonSchemas.ToList().Find(s => s.SchemaName == "wordcount");

            string jsonInput = jsonElement.GetRawText();

            // Get schema and use for validating
            if (new JsonValidator<Article[]>(maut).IsValid(jsonInput, out Article[] articles))
            {
                foreach (Article article in articles)
                {
                    FileListModel fileListModel = new()
                    {
                        SourceId = 0,
                        ArticleTitle = article.ArticleTitle,
                        FilePath = article.FilePath,
                        TotalWordsInArticle = article.TotalWordsInArticle
                    };
                    
                    List<WordListModel> words = new();
                    List<AppearsInModel> appearsInModels = new();

                    foreach (WordData articleWord in article.Words)
                    {
                        WordListModel wordListModel = new()
                        {
                            WordName = articleWord.Word,
                        };

                        AppearsInModel appearsInModel = new()
                        {
                            Amount = articleWord.Amount,
                            WordName = articleWord.Word,
                            ArticleTitle = article.ArticleTitle,
                            FilePath = article.FilePath
                        };

                        if (!words.Exists(w => w.WordName == wordListModel.WordName))
                        {
                            words.Add(wordListModel);
                        }

                        appearsInModels.Add(appearsInModel);
                    }

                    dbContext.Wordlist.AddRange(words);
                    dbContext.FileList.Add(fileListModel);
                    dbContext.SaveChanges();
                    dbContext.AppearsIn.AddRange(appearsInModels);
                    dbContext.SaveChanges();
                }

                Response.StatusCode = 200;
                Console.WriteLine($"Added {articles.Length} entries.");
            }
            else
            {
                Response.StatusCode = 400;
            }

        }
        
        
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            // Get all words
            List<string> words = new();
            new WordCountDbContext().Wordlist.Take(100).ToList().ForEach(wordList => words.Add(wordList.WordName));
            return words;
        }
        
        [HttpGet]
        [Route("/[controller]/{id:int}")]
        public WordListModel Get(int id)
        {
            WordListModel entity = new WordCountDbContext().Wordlist.Find(id);

            return entity;
        }
        
        [HttpGet]
        [Route("/[controller]/WordRatios")]
        public string GetWordRatios()
        {
            var dbContext = new WordCountDbContext();

            return string.Empty;
        }
        
        
    }
}