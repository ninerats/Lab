using System;
using System.Net.Mail;
using NUnit.Framework;

namespace Craftsmaneer.Lang.Test
{
    [TestFixture]
    public class CodeBlockTest
    {
        [Test]
        public void MultiStatementTest()
        {
            var r = Result.Wrap(() =>
            {
                Method1();
                Method2();
            });

            Assert.IsTrue(r.Success);

        }   

        [Test]
        public void MultiStatementIntTest()
        {
            var r = Result<int>.Wrap(() =>
            {
                Method1();
                Method2();
                return 1;
            });
        }

        [Test]
        public void SmtpWrappingTest()
        {
           

            Assert.IsFalse(DoMailStuff().Success);

        }

        private Result DoMailStuff()
        {
            var smtp = new SmtpClient();
            var r = Result.Wrap(() => smtp.Send("", "", "", ""));
            if (r.Error is SmtpException || r.Error is InvalidOperationException || r.Error is ArgumentException)
            {
                return r;
            }

            // do other stuff
            return  Result.Successful;
          }

        private void Method2()
        {
          
        }

        private void Method1()
        {
           
        }
    }
}