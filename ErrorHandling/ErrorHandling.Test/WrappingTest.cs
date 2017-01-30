using System;
using NUnit.Framework;

namespace Craftsmaneer.Lang.Test
{
    [TestFixture]
    public class WrappingTest
    {
        [Test]
        public void TwoArgTest()
        {
            var r = Result<int>.Wrap(() => TwoArgThrower("1", 2));
            Assert.IsFalse(r.Success);
            Assert.That(r.Error is ErrorCode);
        }

        [Test]
        public void VoidTest()
        {
            var r = Result.Wrap(ThrowingVoidMethod);
            Assert.IsFalse(r.Success);
        }

        [Test]
        public void FilteredWrapTest()
        {
            var r = Result<int>.FilteredWrap(() => 
                HttpAction(200), 
                code => code < 300);

            Assert.IsTrue(r.Success);

            r = Result<int>.FilteredWrap(() =>
                HttpAction(404),
                code => code < 300);
            Assert.IsFalse(r.Success);
            Assert.IsNull(r.Error);
        }



        private void ThrowingVoidMethod()
        {
            throw new ErrorCode("ThrowingVoidMethod");
        }

        private void SuccessfulVoidMethod()
        {
            // doesn't throw.
        }

        private int TwoArgThrower(string arg1, int arg2)
        {
            throw new ErrorCode("TwoArgThrower");
        }


        /// <summary>
        /// simulates a function that performs an http action and returns a result.
        /// </summary>
        /// <returns></returns>
        private int HttpAction(int resultCode, Exception shouldThrow=null)
        {
            if (shouldThrow != null)
            {
                throw shouldThrow;
            }

            return resultCode;
        }

    }

    

   
}