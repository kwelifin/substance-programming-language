using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace IL2MSIL
{
    internal class NamespaceBodyState : AssemblyChildState
    {
        public override void Execute(IList<Token> tokens, ref int i)
        {
            if (tokens[i].TokenType == TokenType.Identifier)
            {
            }
        }

        public NamespaceBodyState(Stack<State> stateStack, Dictionary<string, Type> definedTypes, AssemblyBuilder asmBuilder) : base(stateStack, definedTypes, asmBuilder)
        {
        }
    }
}