using System;

namespace CoreSolution.Dapper.Extension.Exception
{
    public class DapperExtensionException : ApplicationException
    {
        public DapperExtensionException(string msg) : base(msg)
        {

        }
    }
}
