using System;

namespace Jell.DataLogger.Core.Exceptions
{
    public class ParsingException : Exception
    {
        public override string Message => "Error parsing datastring.";
    }
}
