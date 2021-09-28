using System;
using NUnit.Framework;
using WordCount.JsonModels;

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
        public void TestJsonObject()
        {
            const string schema = @"{
              ""type"": ""object"",
              ""properties"": {
                ""name"": {""type"": ""string""},
                ""roles"": {""type"": ""array""}
              }
            }";

            const string json = @"{
              ""name"": ""Arnie Admin"",
              ""roles"": [""Developer"", ""Administrator""]
            }";
            
            Assert.True(new JsonValidator<UserModel>(schema).IsObjectValid(json, out UserModel userModel));
            
            Assert.That(userModel.Name == "Arnie Admin");
            Assert.That(userModel.Roles.Length == 2);
            Assert.That(userModel.Roles[0] == "Developer");
            Assert.That(userModel.Roles[1] == "Administrator");
        }
        
        private sealed class UserModel
        {
            public string Name { get; set; }
            public string[] Roles { get; set; } 
        }
        
        [Test]
        public void TestJsonArray()
        {
            const string schema = @"{
              ""type"": ""array"",
              ""items"": {
                ""type"": ""object"",
                ""properties"": {
                  ""name"": {""type"": ""string""}
                }
              }
            }";

            const string json = @"[
              {
                ""name"": ""Jason""
              },
              {
                ""name"": ""Jack""
              }
            ]";
            
            Assert.True(new JsonValidator<NameModel[]>(schema).IsArrayValid(json, out NameModel[] nameModels));
            
            Assert.That(nameModels.Length == 2);
            Assert.That(nameModels[0].Name == "Jason");
            Assert.That(nameModels[1].Name == "Jack");
        }
 
        private sealed class NameModel
        {
            public string Name { get; set; }
        }
    }
}