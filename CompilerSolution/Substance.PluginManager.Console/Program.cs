﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Substance.PluginManager.Backend;
using Substance.PluginManager.Backend.Configs;

namespace Substance.PluginManager.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Configuration();
            var extensionManager = new ExtensionManager(config);
           
        }
    }
}
