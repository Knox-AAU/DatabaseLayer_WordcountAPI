using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WordCount.Data.DataAccess;

namespace WordCountUnitTests
{
    public sealed class ReadOnlyRepositoryTest
    {
        
        private sealed class DbObject : DatabaseEntityModel<string>
        {
            public override string PrimaryKey
            {
                get => Key;
            }

            public bool BoolProp { get; init; }
            public string Key { get; init; }
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
                    BoolProp = true
                }
            };
            ReadOnlyRepository<DbObject, string> repos = new(objList);
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
            ReadOnlyRepository<DbObject, string> repos = new(new List<DbObject>());
            //Act
            IEnumerable<DbObject> reposResult = repos.All();

            //Assert
            Assert.NotNull(reposResult);
        }

        [Test]
        public void GetByKey_GivesCorrectEntity()
        {
            string primKey = "123fds";
            var obj = new DbObject()
            {
                Key = primKey,
                BoolProp = true
            };
        
            
                
            ReadOnlyRepository<DbObject, string> repos = new(new List<DbObject>()
                {obj});
            var res = repos.GetById(primKey);
            Assert.IsNotNull(res);
            Assert.AreEqual(obj,res);
        }
        
        [Test]
        public void GetByKey_GivesNullOnNoEntity()
        {
            string primKey = "123fds";
            var obj = new DbObject()
            {
                Key = primKey,
                BoolProp = true
            };
            
            ReadOnlyRepository<DbObject, string> repos = new(new List<DbObject>()
                {obj});

            Assert.IsNull(repos.GetById(" "));
        }

        

        [Test]
        public void Repository_FindAll_FindDataOnKey()
        {
            //Arrange
            List<DbObject> objList = new List<DbObject>()
            {
                new DbObject() {
                    Key = "two",
                    BoolProp = true
                }
            };
   
            ReadOnlyRepository<DbObject, string> repos = new(objList);
            //Act
            IEnumerable<DbObject> p = repos.FindAll(a => a.BoolProp);

            //Assert
            Assert.NotNull(p);
            Assert.AreEqual(1,p.Count());
        }

        [Test]
        public void Repository_Find_FindDataOnPredicate()
        {
            List<DbObject> objList = new List<DbObject>()
            {
                new DbObject() {
                    Key = "two",
                    BoolProp = true
                }
            };
   
            ReadOnlyRepository<DbObject, string> repos = new(objList);
            //Act
            DbObject p = repos.Find(a => a.BoolProp == false);

            //Assert
            Assert.IsNull(p);
        }
        
        [Test]
        public void Find_GivesNullOnNoPredicateMatch()
        {
            //Arrange
            string key = "123";
            List<DbObject> objList = new List<DbObject>();
            DbObject testObj = new DbObject {Key = key};
            ReadOnlyRepository<DbObject, string> repos = new(new List<DbObject>(){testObj});
            
            //Act
            DbObject res = repos.Find(a => a.Key == "123");
            
            //Assert
            Assert.NotNull(res);
            Assert.AreEqual(testObj, res);
        }
    }
}