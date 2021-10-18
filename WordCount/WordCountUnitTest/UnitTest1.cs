using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Moq;
using NUnit.Framework;
using WordCount.DataAccess;
using WordCount.Models;

namespace WordCountUnitTests
{
    public class SchemaControllerTests
    {

        private static IQueryable<JsonSchema> CorrectData = new List<JsonSchema>
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
        }.AsQueryable();
        


        [Test]
        public void k()
        {
            // Arrange
            var mockSet = new Mock<DbSet<JsonSchema>>();
            mockSet.As<IQueryable<JsonSchema>>().Setup(m => m.Provider).Returns(CorrectData.Provider);
            mockSet.As<IQueryable<JsonSchema>>().Setup(m => m.Expression).Returns(CorrectData.Expression);
            mockSet.As<IQueryable<JsonSchema>>().Setup(m => m.ElementType).Returns(CorrectData.ElementType);
            mockSet.As<IQueryable<JsonSchema>>().Setup(m => m.GetEnumerator()).Returns(CorrectData.GetEnumerator());
                
            // Act
            var repos = new Repository<JsonSchema, string>(mockSet.Object);
            var q =repos.All();
            Assert.IsTrue(false);

        }
    }

}