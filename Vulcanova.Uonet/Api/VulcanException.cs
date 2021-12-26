using System;

namespace Vulcanova.Uonet.Api
{
    public class VulcanException : Exception
    {
        public VulcanException(string message) : base(message)
        {
        }
    }
}