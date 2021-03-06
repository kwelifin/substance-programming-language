﻿using System.Collections.Generic;
using CompilerUtilities.Plugins.Contract;

namespace ExampleStages.Types
{
    public class ExampleSyntaxTree : ISyntaxTree
    {
        public ExampleSyntaxTree()
        {
            Nodes = new List<ISyntaxTreeNode>();
        }

        public IList<ISyntaxTreeNode> Nodes { get; }

        public override string ToString()
        {
            return string.Join("\r\n", Nodes);
        }
    }
}