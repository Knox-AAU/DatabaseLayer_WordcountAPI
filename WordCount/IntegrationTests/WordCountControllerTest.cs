using System.Collections.Generic;
using System.Data.Entity;
using EntityFramework.Testing.Moq.Ninject;
using Moq;
using Ninject;
using Ninject.MockingKernel.Moq;
using NUnit.Framework;
using WordCount;
using WordCount.Controllers;

namespace IntegrationTests
{
    public sealed class WordCountControllerTest
    {
       

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void IdGivesCorrectFilePath()
        {
            using var kernel = new MoqMockingKernel();
            kernel.Load(new EntityFrameworkTestingMoqModule());

            // Create some test data
            var data = new List<Article>
            {
                new()
                {
                    Title = "aaa", Id = 1, FilePath = "CorrectPath",
                    OccursIns = new List<OccursIn>(), TotalWords = 0, PublisherName = "P"
                },
                new()
                {
                    Title = "BBB", Id = 2, FilePath = "",
                    OccursIns = new List<OccursIn>(), TotalWords = 0, PublisherName = "P"
                }
            };

            // Setup mock set
            kernel.GetMock<DbSet<Article>>()
                .SetupData(data);

            // Get a BlogsController and invoke the Index action
            var controller = kernel.Get<WordCountController>();
            var result = controller.GetFilepath(1);
            // Check the results

            Assert.AreEqual("CorrectPath", data[0].FilePath);
        }
    }
}