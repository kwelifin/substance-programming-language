using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace IL2MSIL
{
    internal class TypeState : AssemblyChildState
    {
        private readonly ModuleBuilder _moduleBuilder;
        public TypeAttributes? AccesssModifier;
        public TypeAttributes Modifiers;
        public string Name;

        public TypeState(Stack<State> stateStack, Dictionary<string, Type> definedTypes, AssemblyBuilder asmBuilder, ModuleBuilder moduleBuilder) : base(stateStack,definedTypes, asmBuilder)
        {
            _moduleBuilder = moduleBuilder;
            AccesssModifier = TypeAttributes.NotPublic;
        }

        public override void Execute(IList<Token> tokens, ref int i)
        {
            if (tokens[i].TokenType == TokenType.Modifier)
            {
                if (!ModifierCollection.TypeAttributeses.ContainsKey(tokens[i].Value))
                    throw new NotImplementedException();

                if (ModifierCollection.TypeModifiers.ContainsKey(tokens[i].Value))
                    if (AccesssModifier is null)
                        AccesssModifier = ModifierCollection.TypeModifiers[tokens[i].Value];
                    else throw new NotImplementedException();

                Modifiers |= ModifierCollection.TypeAttributeses[tokens[i].Value];
                i++;
            }
            else if (tokens[i].TokenType == TokenType.TypeDef)
            {
                if (tokens[++i].TokenType != TokenType.Type)
                    throw new NotImplementedException();

                Name = tokens[i++].Value;

                var type = _moduleBuilder.DefineType(Name, (TypeAttributes)(AccesssModifier | Modifiers));

                StateStack.Push(new TypeBodyState(StateStack, DefinedTypes, AsmBuilder, type));
            }
        }
    }
}