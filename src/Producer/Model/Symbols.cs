﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.Model
{
    public class Symbols
    {
        public Symbols()
        {
            SymbolsToRate = new List<string>();
        }
        public IList<string> SymbolsToRate { get; set; }
    }
}
