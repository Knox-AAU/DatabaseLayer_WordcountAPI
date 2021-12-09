using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Testing.Moq.Ninject;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninject;
using Ninject.MockingKernel.Moq;
using NUnit.Framework;
using WordCount;
using WordCount.Controllers;
using WordCount.Controllers.JsonInputModels;
namespace IntegrationTests
{
    public class WordRatioControllerTest
    {
        [Test]
        public void Gives_Same_Type_Result_And_Status_OK()
        {
            using var kernel = new MoqMockingKernel();
            kernel.Load(new EntityFrameworkTestingMoqModule());

            // Create some test data
            var data = new List<WordRatio>
            {
                new()
                {
                    ArticleId = 2, Percent = (decimal?) 0.4, Count = 123, FilePath = "", PublisherName = "NJ",
                    Title = "sad", TotalWords = 5985498, Word = "hello"
                },
            };

            // Setup mock set
            kernel.GetMock<DbSet<WordRatio>>()
                .SetupData(data);
            
            var controller = kernel.Get<WordCount.Controllers.WordRatioController>();

            
            var expected = controller.Ok(data);
            var expectedTypeAsString = expected.Value.ToString();
            Assert.AreEqual(expected.StatusCode, 200);
            Assert.AreEqual(data.GetType(), expected.Value.GetType());
        }
        
        [Test]
        public void NoTermsGiven_Gives_BadRequestStatus()
        {
            using var kernel = new MoqMockingKernel();
            kernel.Load(new EntityFrameworkTestingMoqModule());

            // Create some test data
            var data = new List<WordRatio>
            {
                new()
                {
                    ArticleId = 2, Percent = (decimal?) 0.4, Count = 123, FilePath = "", PublisherName = "NJ",
                    Title = "sad", TotalWords = 5985498, Word = "hello"
                },
            };

            // Setup mock set
            kernel.GetMock<DbSet<WordRatio>>()
                .SetupData(data);
            
            var controller = kernel.Get<WordCount.Controllers.WordRatioController>();
            controller.GetMatches(new[] {""}, new[] {"NJ"});
            // Check the result
            
            
            var expected = controller.Ok(data);
            var expectedTypeAsString = expected.Value.ToString();
            Assert.AreEqual(data.ToString(), expectedTypeAsString);
        }
    }
}