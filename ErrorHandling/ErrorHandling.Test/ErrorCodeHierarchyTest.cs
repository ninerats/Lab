using NUnit.Framework;

namespace Craftsmaneer.Lang.Test
{
    [TestFixture]
    public class ErrorCodeHierarchyTest
    {
        [Test]
        public void WrapMultiTypedErrorTest()
        {
            var r = Result.Wrap(ThrowSmallError2);
            Assert.That(r.Error is SmallError);
            Assert.That(r.Error is HorribleError);
            Assert.That(r.Error is GreenError);
            Assert.IsFalse(r.Error is BigError);
        }


        private void ThrowSmallError2()
        {
            throw new SmallError2();
        }

    }

    class BigError : ErrorCode { }

    internal class SmallError : ErrorCode
    {
    }

    internal class SmallError1 : SmallError
    {
    }

    class SmallError2 : SmallError, HorribleError, GreenError
    {
       }

    interface HorribleError { }

    interface GreenError{ }

}