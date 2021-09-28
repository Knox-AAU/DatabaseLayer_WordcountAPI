using System;
using KnoxDatabaseLayer3.JsonModels;
using NUnit.Framework;

namespace WordCountUnitTests
{
    public class JsonValidatorTests
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine();
        }

        [Test]
        public void Test()
        {
            string schema = @"{
              'type': 'object',
              'properties': {
                'name': {'type':'string'},
                'roles': {'type': 'array'}
              }
            }";

            string json = @"{
              'name': 'Arnie Admin',
              'roles': ['Developer', 'Administrator']
            }";

            Assert.True(new JsonValidator<UserModel>(schema).IsObjectValid(json, out UserModel userModel));
            Assert.That(userModel.Name == "Arnie Admin");
        }

        private sealed class UserModel
        {
            public string Name { get; set; }
            public string[] Roles { get; set; } 
        }
    }
}