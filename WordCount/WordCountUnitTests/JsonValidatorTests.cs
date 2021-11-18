using NUnit.Framework;
using WordCount.Controllers.JsonInputModels;

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
            ""required"": [
                ""name"",
                ""roles""
            ],
            ""properties"": {
                ""name"": {""type"": ""string""},
                ""roles"": {""type"": ""array""}
            }
        }";

        private const string JsonArraySchema = @"{
            ""type"": ""array"",
            ""items"": {
                ""type"": ""object"",
                ""required"": [
                    ""name"",
                    ""roles""
                ],
                ""properties"": {
                    ""name"": {""type"": ""string""},
                    ""roles"": {""type"": ""array""},
                }
            }
        }";

        [Test]
        public void IsJsonObjectValid_CorrectFormat_Success()
        {
            const string jsonObject = @"{
                ""name"": ""Arnie Admin"",
                ""roles"": [""Developer"", ""Administrator""]
            }";

            Assert.True(new JsonValidator<UserModel>(JsonObjectSchema).IsValid(jsonObject, out UserModel userModel));

            Assert.That(userModel.Name == "Arnie Admin");
            Assert.That(userModel.Roles.Length == 2);
            Assert.That(userModel.Roles[0] == "Developer");
            Assert.That(userModel.Roles[1] == "Administrator");
        }

        [Test]
        public void IsJsonArrayValid_CorrectFormat_Success()
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

            Assert.True(new JsonValidator<UserModel[]>(JsonArraySchema).IsValid(jsonArray, out UserModel[] nameModels));

            Assert.That(nameModels.Length == 2);
            Assert.That(nameModels[0].Name == "Jason");
            Assert.That(nameModels[1].Name == "Jack");
        }

        [Test]
        public void IsJsonObjectValid_IncorrectKeys_Failure()
        {
            const string jsonObject = @"{
                ""bames"": ""Arnie Admin"",
                ""roles"": [""Developer"", ""Administrator""]
            }";

            Assert.False(new JsonValidator<UserModel>(JsonObjectSchema).IsValid(jsonObject, out UserModel userModel));
        }

        [Test]
        public void IsJsonArrayValid_IncorrectKeys_Failure()
        {
            const string jsonArray = @"[
                {
                    ""rame"": ""Jason"",
                    ""aoles"": [""Developer"", ""Administrator""]
                },
                {
                    ""name"": ""Jack"",
                    ""roles"": [""Manager""]
                }
            ]";

            Assert.False(new JsonValidator<UserModel[]>(JsonArraySchema).IsValid(jsonArray, out UserModel[] nameModels));
        }
    }
}