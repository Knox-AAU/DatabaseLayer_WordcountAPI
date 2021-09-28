using NUnit.Framework;
using WordCount.JsonModels;

namespace WordCountUnitTests
{
    public sealed class JsonValidatorTests
    {
        private sealed class UserModel
        {
            public string Name { get; set; }
            public string[] Roles { get; set; } 
        }
        
        private const string JsonObjectSchema = @"{
            ""type"": ""object"",
            ""properties"": {
                ""name"": {""type"": ""string""},
                ""roles"": {""type"": ""array""}
            }
        }";

        private const string JsonObject = @"{
            ""name"": ""Arnie Admin"",
            ""roles"": [""Developer"", ""Administrator""]
        }";
        
        private const string JsonArarySchema = @"{
            ""type"": ""array"",
            ""items"": {
                ""type"": ""object"",
                ""properties"": {
                    ""name"": {""type"": ""string""},
                    ""roles"": {""type"": ""array""},
                }
            }
        }";

        private const string JsonArray = @"[
            {
                ""name"": ""Jason"",
                ""roles"": [""Developer"", ""Administrator""]
            },
            {
                ""name"": ""Jack"",
                ""roles"": [""Manager""]
            }
        ]";

        [Test]
        public void TestJsonObject()
        {
            Assert.True(new JsonValidator<UserModel>(JsonObjectSchema).IsObjectValid(JsonObject, out UserModel userModel));
            
            Assert.That(userModel.Name == "Arnie Admin");
            Assert.That(userModel.Roles.Length == 2);
            Assert.That(userModel.Roles[0] == "Developer");
            Assert.That(userModel.Roles[1] == "Administrator");
        }
      
        [Test]
        public void TestJsonArray()
        {
            Assert.True(new JsonValidator<UserModel[]>(JsonArarySchema).IsArrayValid(JsonArray, out UserModel[] nameModels));
            
            Assert.That(nameModels.Length == 2);
            Assert.That(nameModels[0].Name == "Jason");
            Assert.That(nameModels[1].Name == "Jack");
        }
    }
}