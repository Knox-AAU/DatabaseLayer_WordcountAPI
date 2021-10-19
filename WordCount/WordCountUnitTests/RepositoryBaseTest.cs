using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using WordCount.DataAccess;

namespace WordCountUnitTests
{
    
    public sealed class RepositoryBaseTest
    {
        private sealed class DbObject : DatabaseEntityModel<string>
        {
            public override string PrimaryKey
            {
                get => Key;
            }

            public int IntProp { get; set; }
            public bool BoolProp { get; set; }
            public string Key { get; set; }
        }

        private List<DbObject> CorrectData { get; set; }

        [SetUp]
        public void SetUp()
        {
            CorrectData = new List<DbObject>
            {
               
            };
        }

        [Test]
        public void Repository_GivesNotNullAndSameCountAsInput()
        {
            //Arrange
            RepositoryBase<DbObject, string> repos = new(CorrectData);
            //Act
            IEnumerable<DbObject> reposResult = repos.All();
            CorrectData.Add( new()
                {
                    Key = "one",
                    IntProp = 1,
                    BoolProp = true
                });
            //Assert
            Assert.NotNull(reposResult);
            Assert.AreEqual(reposResult.Count(), CorrectData.Count());
            Assert.AreEqual(reposResult, CorrectData);
        }

        [Test]
        public void Repository_NewListGivesNoObjects()
        {
            //Arrange
            RepositoryBase<DbObject, string> repos = new(new List<DbObject>());
            //Act
            IEnumerable<DbObject> reposResult = repos.All();

            //Assert
            Assert.NotNull(reposResult);
        }

        [Test]
        public void Repository_Insert_AddObjectToCollection()
        {
            //Arrange
            
            DbObject test = new DbObject {Key = "TestObject"};
            RepositoryBase<DbObject, string> repos = new(new List<DbObject>());
            //Act
            repos.Insert(test);

            //Assert
            Assert.IsTrue(repos.EntitySet.Contains(test));
        }

        [Test]
        public void Repository_FindAll_FindDataOnKey()
        {
            //Arrange
            
            CorrectData.Add(
                new()
                {
                    Key = "two",
                    IntProp = 2,
                    BoolProp = true
                });
            RepositoryBase<DbObject, string> repos = new(CorrectData);
            //Act
            IEnumerable<DbObject> p = repos.FindAll(a => a.BoolProp);


            //Assert
            Assert.NotNull(p);
            Assert.AreEqual(1,p.Count());
        }

        [Test]
        public void Repository_Find_FindDataOnPredicate()
        {
            //Arrange
            RepositoryBase<DbObject, string> repos = new(CorrectData);
            //Act
            DbObject testObj = new DbObject {Key = "123bvd"};
            CorrectData.Add(testObj);
            DbObject res = repos.Find(a => a.Key == "123bvd");


            //Assert
            Assert.NotNull(res);
            Assert.AreEqual(testObj, res);
        }
        
        [Test]
        public void Repository_UpdateUpdatesCollection()
        {
            //Arrange
            DbObject testObj = new DbObject {Key = "123bvd"};
            CorrectData.Add(testObj);
            
            RepositoryBase<DbObject, string> repos = new(CorrectData);
            //Act
            repos.Update(CorrectData.First().Key, testObj);
            
            //Assert
            Assert.IsTrue(CorrectData.Contains(testObj));
        }
        
        [Test]
        public void Repository_DuplicateInsertionGivesError()
        {
            //Arrange
            DbObject testObj1 = new DbObject {Key = "123bvd"};
            DbObject testObj2 = new DbObject {Key = "123bvd"};
            CorrectData.Add(testObj1);
            RepositoryBase<DbObject, string> repos = new(CorrectData);
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => repos.Insert(testObj2));
        }
        
        [Test]
        public void Repository_DuplicateKeyInListInsertionGivesError()
        {
            //Arrange
            DbObject testObj1 = new DbObject {Key = "123bvd"};
            DbObject testObj2 = new DbObject {Key = "123bvd"};
            DbObject testObj3 = new DbObject {Key = "321bvd"};

            List<DbObject> testData = new() {testObj1, testObj2, testObj3};

            RepositoryBase<DbObject, string> repos = new(testData);
            //
            //Assert
            Assert.Throws<ArgumentException>(() => repos.Insert(testObj2));
        }
    }
}