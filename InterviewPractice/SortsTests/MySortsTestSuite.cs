using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sorts;

namespace SortsTests
{
    [TestClass]
    public class MySortsTestSuite
    {
        [TestMethod]
        public void MySorts_Test_Constructor_Empty_()
        {
            MySorts<int> newSorts = new MySorts<int>();
            Assert.AreEqual(newSorts.type, typeof(int));
            Assert.AreEqual(newSorts.collection.Count, 0);
        }

        [TestMethod]
        public void MySorts_Test_Constructor_IEnumerable<T>()
        {
            List<int> ints = new List<int>() {1, 2, 3, 4, 5, 6 };
            MySorts<int> newSorts = new MySorts<int>(ints);

            Assert.AreEqual(typeof(int), newSorts.type);
            Assert.AreEqual(newSorts.collection, ints);
        }

        [TestMethod]
        public void MySorts_Test_()
        {
            //TODO: Implement Test
            throw new NotImplementedException();
        }

        //[TestMethod]
        //public void MySorts_Test_()
        //{
        //    //TODO: Implement Test
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void MySorts_Test_()
        //{
        //    //TODO: Implement Test
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void MySorts_Test_()
        //{
        //    //TODO: Implement Test
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void MySorts_Test_()
        //{
        //    //TODO: Implement Test
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void MySorts_Test_()
        //{
        //    //TODO: Implement Test
        //    throw new NotImplementedException();
        //}
    }
}
