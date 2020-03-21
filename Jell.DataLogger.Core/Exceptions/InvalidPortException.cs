using System;

namespace Jell.DataLogger.Core.Exceptions
{
    public class InvalidPortException : Exception
    {
        public override string Message => "Invalid port name.";
    }
}
