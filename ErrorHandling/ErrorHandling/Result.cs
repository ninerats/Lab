using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Craftsmaneer.Lang
{

    public class Result
    {
        public bool Success { get; protected set; }
        public Exception Error { get; protected set; }

        public Result Inner { get; protected set; }

        private static Result _successful = new Result(true);
        public static Result Successful
        {
            get { return _successful; } 
        }

        protected Result(): this(true)
        {
           
        }

        public Result(Exception ex)
        {
            Success = false;
            Error = ex;

        }

        public Result(bool success)
        {
            Success = success;
        }

        public static Result Wrap(Action action, Action<Exception> errorContinue = null)
        {
            try
            {
                action();
                var r = new Result();
                return r;
            }
            catch (Exception ex)
            {
                // log it.
                var r = new Result(ex);
                RunErrorHandler(ex, errorContinue);
                return r;
            }
        }

        protected static void LogException(Exception ex)
        {
            // logging code goes here.
        }

        protected static void RunErrorHandler(Exception ex, Action<Exception> errorHandler)
        {
            if (errorHandler == null)
            {
                LogException(ex);
            }
            else
            {
                try
                {
                    errorHandler(ex);
                }
                catch
                {
                }
            }
        }
    }

    /// <summary>
    /// encapsulates a function result, and supports multi-valued returns for error conditions.
    /// </summary>
    public class Result<T> : Result
    {
       public T Value { get; private set; }
        

        public Result(Exception ex) :base(ex){}

        public Result(T returnValue)
        {
            Success = true;
            Value = returnValue;

        }

        private Result(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// assume failure due to result passed in.
        /// </summary>
        /// <param name="result"></param>
        public static Result<T> ChainResult<TResult>(Result<TResult> result)
        {
            var r = new Result<T>(result.Error);
            r.Inner = result;
            r.Success = result.Success;
            r.Error = result.Error;
        
            return r;
        }

        public static Result<T> Wrap(Func<T> func, Action<Exception> errorHandler = null)
        {
            try
            {
                T ret = func();
                var r = new Result<T>(ret);
                return r;
            }
            catch (Exception ex)
            {
                // log it.
                var r = new Result<T>(ex);
                RunErrorHandler(ex, errorHandler);
                return r;
            }
        }

        public static Result<T> FilteredWrap(Func<T> func, Func<T, bool> filter, Action<Exception> errorHandler = null)
        {
            try
            {
                T ret = func();

                var r = new Result<T>(filter(ret));
                return r;
            }
            catch (Exception ex)
            {
                // log it.
                var r = new Result<T>(ex);
                RunErrorHandler(ex, errorHandler);
                return r;
            }
        }
    }
}
