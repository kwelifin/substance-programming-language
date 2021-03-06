﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CompilerUtilities.Plugins.Contract;
using Substance.PluginManager.Backend.Configs;

namespace Substance.PluginManager.Backend
{
    public class Plugin:IExtension
    {
        public Configuration Configuration { get; }
        public ExtensionInfo Info { get; }
        public string ExtensionFolder { get; }
        public ExtensionStatus Status { get; }
        public Type[] Types { get; }

        public Plugin(object plugin)
        {
            var @interfaces = plugin.GetType().FindInterfaces((type, criteria) => true, null);
            if (@interfaces.Length != 1)
                throw new ArgumentException("Invalid object type");

            var location = Path.GetDirectoryName(plugin.GetType().Assembly.Location);
            var iplugin = @interfaces[0];
            var types = iplugin.GenericTypeArguments;

            Types = types;
            ExtensionFolder = location;
        }
    }
}