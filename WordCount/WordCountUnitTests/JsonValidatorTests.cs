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

        [Test]
        public void TestJsonObject()
        {
            const string jsonObject = @"{
                ""name"": ""Arnie Admin"",
                ""roles"": [""Developer"", ""Administrator""]
            }";
        
            Assert.True(new JsonValidator<UserModel>(JsonObjectSchema).IsObjectValid(jsonObject, out UserModel userModel));
            
            Assert.That(userModel.Name == "Arnie Admin");
            Assert.That(userModel.Roles.Length == 2);
            Assert.That(userModel.Roles[0] == "Developer");
            Assert.That(userModel.Roles[1] == "Administrator");
        }
      
        [Test]
        public void TestJsonArray()
        {
            const string jsonArray = @"[
                {
                    ""name"": ""Jason"",
                    ""roles"": [""Developer"", ""Administrator""]
                },
                {
                    ""name"": ""Jack"",
                    ""roles"": [""Manager""]
                }
            ]";
            
            Assert.True(new JsonValidator<UserModel[]>(JsonArarySchema).IsArrayValid(jsonArray, out UserModel[] nameModels));
            
            Assert.That(nameModels.Length == 2);
            Assert.That(nameModels[0].Name == "Jason");
            Assert.That(nameModels[1].Name == "Jack");
        }

        [Test]
        public void TestJsonObjectFail()
        {
            const string jsonObject = @"{
                ""names"": ""Arnie Admin"",
                ""roles"": true
            }";
            
            Assert.False(new JsonValidator<UserModel>(JsonObjectSchema).IsObjectValid(jsonObject, out UserModel userModel));
        }
      
        [Test]
        public void TestJsonArrayFail()
        {
            const string jsonArray = @"[
                {
                    ""name"": 1,
                    ""roles"": [""Developer"", ""Administrator""]
                },
                {
                    ""name"": ""Jack"",
                    ""roles"": [""Manager""]
                }
            ]";
            
            Assert.False(new JsonValidator<UserModel[]>(JsonArarySchema).IsArrayValid(jsonArray, out UserModel[] nameModels));
        }
    }
}