using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Craftsmaneer.Lang.Test
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void ExceptionWrappingTest()
        {
            

            var path = "badpath";
            Result<string> r = Result<string>.Wrap(() => 
                File.ReadAllText(path)
                );

            Assert.IsFalse(r.Success);
            Assert.That(r.Error is FileNotFoundException);
        }

        [Test]
        public void ValueWrappingTest()
        {

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"resource\wrappingtest.txt");
            Result<string> r = Result<string>.Wrap(() => File.ReadAllText(path));

            Assert.IsTrue(r.Success);
            Assert.AreEqual("This is the best part of the trip.",r.Value);
        }

        [Test]
        public void ChainingTest()
        {
            var r = FuncA();
            Assert.IsFalse(r.Success);
            Assert.AreEqual("error in FuncB",r.Error.Message);
            
            
            Assert.AreEqual("",r.Inner.GetType());
        }

        [Test]
        public void TwoArgTest()
        {
            
        }

        private Result<string> FuncA()
        {
            var r = FuncB();
            return Result<string>.ChainResult(r);
            
        }

        private Result<int> FuncB()
        {
            return new Result<int>(new Exception("error in FuncB"));
        }
    }
}
