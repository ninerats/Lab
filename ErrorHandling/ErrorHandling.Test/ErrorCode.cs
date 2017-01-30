using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Craftsmaneer.Lang.Test
{
    class ErrorCode : Exception
    {
        public ErrorCode()
        {
            
        }

        public ErrorCode(string message) :base(message)
        {
            
        }
    }
}
