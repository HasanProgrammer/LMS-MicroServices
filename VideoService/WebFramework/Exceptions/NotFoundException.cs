using System;

namespace WebFramework.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
            
        }
    }
}