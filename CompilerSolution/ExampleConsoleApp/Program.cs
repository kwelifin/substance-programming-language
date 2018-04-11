﻿using System;
using System.IO;
using CompilerUtilities.Plugins.Management;

namespace ExampleConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var manager = new PluginManager(Path.Combine(Directory.GetCurrentDirectory(), "plugins"));
            manager.Run("source.txt", "output.exe");

            Console.ReadKey();
        }
    }
}