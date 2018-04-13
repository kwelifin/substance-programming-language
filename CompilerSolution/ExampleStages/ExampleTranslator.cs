﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using CompilerUtilities.BaseTypes.Interfaces;
using CompilerUtilities.Plugins.Contract;
using CompilerUtilities.Plugins.Contract.Versions;

namespace ExampleStages
{
    [Export(typeof(IStage<,>))]
    public class ExampleTranslator : IStage<ITextProcessor, Blanket>
    {
        public uint Priority { get; }
        public VersionInfo Version { get; }
        public VersionInfo RequreCompilerVersion { get; }
        public string Name { get; }
        public string Author { get; }
        public string Description { get; }

        private ICompileOptions _options;

        public void Initialize(ICompileOptions options)
        {
            _options = options;
        }

        public Blanket Process(ITextProcessor input)
        {
            File.WriteAllLines(_options["output_file"], input.Presentation);

            return null;
        }
        
    }
}