
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Microsoft.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using WordCount.DataAccess;
using WordCount.Models;

namespace WordCountUnitTests
{
    public class SchemaControllerTests
    {



        [Test]
        public void k()
        {
            List<JsonSchema> CorrectData = new List<JsonSchema>
            {
                new JsonSchema()
                {
                    SchemaName = "name1",
                    JsonString =
                        "\"properties\":{\"firstName\":{\"type\":\"string},\"lastName\":{\"type\":\"string\"},\"age\":{\"type\":\"integer\",\"minimum\":0}}"
                },
                new JsonSchema()
                {
                    SchemaName = "Person",
                    JsonString = "{\"firstName\":\"John\",\"lastName\":\"Doe\",\"age\":21}"
                }
            };

            // Act
            var repos = new Repository<JsonSchema, string>(CorrectData);
            var q  =repos.All();
            Assert.NotNull(q);
            Assert.AreEqual(q.Count(), CorrectData);

        }
    }

}