using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jell.DataLogger.Core.Exceptions
{
    public class InvalidPortException : Exception
    {
        public override string Message => "Invalid port name.";
    }
}
