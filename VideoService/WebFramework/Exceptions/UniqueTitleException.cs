using System;

namespace WebFramework.Exceptions
{
    public class UniqueTitleFieldException : Exception
    {
        public UniqueTitleFieldException(string message) : base(message)
        {
            
        }
    }
}