﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jell.DataLogger.Core.Exceptions
{
    public class ParsingException : Exception
    {
        public override string Message => "Error parsing datastring.";
    }
}
