﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer_Consumer_dotnet5
{
    internal class MssgData
    {
        public string Mssg { get; set; }
     
        // For extensible application, use "object" type.
        public object Data { get; set; }
    }
}
