using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using WordCount.Data.DataAccess;

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





        [Test]
        public void Repository_GivesNotNullAndSameCountAsInput()
        {
            //Arrange
            //Act
            List<DbObject> objList = new List<DbObject>()
            {
                new DbObject() {
                    Key = "one",
                    IntProp = 1,
                    BoolProp = true
                }
            };
            RepositoryBase<DbObject, string> repos = new(objList);
            IEnumerable<DbObject> reposResult = repos.All();
            //Assert
            Assert.NotNull(reposResult);
            Assert.AreEqual(reposResult.Count(), objList.Count());
            Assert.AreEqual(reposResult, objList);
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
        public void Repository_Insert_AddObjectsToCollection()
        {
            //Arrange
            
            DbObject test1 = new DbObject {Key = "TestObject1"};
            DbObject test2= new DbObject {Key = "TestObject2"};
            RepositoryBase<DbObject, string> repos = new(new List<DbObject>());
            //Act
            repos.Insert(new List<DbObject>(){test1,test2});

            //Assert
            Assert.IsTrue(repos.EntitySet.Contains(test1));
            Assert.IsTrue(repos.EntitySet.Contains(test2));
        }
        
        [Test]
        public void Repository_Update_UpdatesEntity()
        {
            //Arrange
            DbObject original = new DbObject {Key = "TestObject1", BoolProp = false};
            DbObject newEntity= new DbObject {Key = "TestObject2", BoolProp = true};
            RepositoryBase<DbObject, string> repos = new(new List<DbObject>(){original});

            //Act
            repos.Update(original, newEntity);
            //Assert
            Assert.IsTrue(repos.EntitySet.Contains(newEntity));
            Assert.IsNotNull(repos.EntitySet.First(a => a.BoolProp = true));
        }

        [Test]
        public void Repository_FindAll_FindDataOnKey()
        {
            //Arrange
            List<DbObject> objList = new List<DbObject>()
            {
                new DbObject() {
                    Key = "two",
                    IntProp = 1,
                    BoolProp = true
                }
            };
   
            RepositoryBase<DbObject, string> repos = new(objList);
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
            List<DbObject> objList = new List<DbObject>();
            RepositoryBase<DbObject, string> repos = new(objList);
            //Act
            DbObject testObj = new DbObject {Key = "123bvd"};
            objList.Add(testObj);
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
            List<DbObject> objList = new List<DbObject>();
            objList.Add(testObj);
            
            RepositoryBase<DbObject, string> repos = new(objList);
            //Act
            repos.Update(objList.First().Key, testObj);
            
            //Assert
            Assert.IsTrue(objList.Contains(testObj));
        }
        
        [Test]
        public void Repository_DuplicateInsertionGivesError()
        {
            //Arrange
            DbObject testObj1 = new DbObject {Key = "123bvd"};
            DbObject testObj2 = new DbObject {Key = "123bvd"};
            List<DbObject> objList = new List<DbObject>();
            objList.Add(testObj1);
            RepositoryBase<DbObject, string> repos = new(objList);
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

        [Test]
        public void Delete_RemoveEntityFromList()
        {
            DbObject testObj1 = new DbObject {Key = "123bvd"};
            DbObject testObj2 = new DbObject {Key = "321bvd"};

            List<DbObject> testData = new() {testObj1, testObj2};
                

            RepositoryBase<DbObject, string> repos = new(testData);
            repos.Delete(testObj1);
            
            Assert.False(repos.EntitySet.Contains(testObj1));
        }
        
        [Test]
        public void Delete_RemoveEntityFromListWithPredicate()
        {
            string KeyToRemove = "123bvd";
            DbObject testObj1 = new DbObject {Key = "123bvd"};
            DbObject testObj2 = new DbObject {Key = KeyToRemove};

            List<DbObject> testData = new() {testObj1, testObj2};
                

            RepositoryBase<DbObject, string> repos = new(testData);
            repos.Delete(a => a.Key == KeyToRemove);
            
            Assert.False(repos.EntitySet.Contains(testObj1));
        }
        
        [Test]
        public void Update_NonExistingGivesArgumentException()
        {
            string KeyToRemove = "123bvd";
            DbObject testObj = new DbObject {Key = KeyToRemove};

            List<DbObject> testData = new() {};

            RepositoryBase<DbObject, string> repos = new(testData);


            Assert.Throws<ArgumentException>(() => repos.Update(testObj.Key, testObj));
        }
    }
}