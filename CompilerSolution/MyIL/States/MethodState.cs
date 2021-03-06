using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using CompilerUtilities.Exceptions;
using Sigil.NonGeneric;

namespace IL2MSIL
{
    internal class MethodState : TypeChildState
    {
        public Type ReturnType;
        public string Name;
        private string[] modifs;

        public MethodState(Stack<State> stateStack, Dictionary<string, Type> definedTypes, AssemblyBuilder asmBuilder, Type typeBuilder, string[] modifs) : base(stateStack, definedTypes, asmBuilder, typeBuilder)
        {
            this.modifs = modifs;
        }

        public override void Execute(IList<Token> tokens, ref int i)
        {
            if (tokens[i].TokenType == TokenType.Type)
                ReturnType = DefinedTypes[tokens[i++].Value];
            else if (tokens[i].TokenType == TokenType.Identifier && ReturnType != null)
            {
                Name = tokens[i++].Value;
            }
            else if (tokens[i].TokenType == TokenType.OpenBrace)
            {
                i++;
                var isType = true;
                var parameters = new List<(Type type, string name)>();
                while (tokens[i].TokenType != TokenType.CloseBrace)
                {
                    if (isType != (tokens[i].TokenType == TokenType.Type))
                        //todo TypeExpected
                        ExceptionManager.ThrowCompiler(ErrorCode.TypeExpected, "", tokens[i].Line);

                    if (!isType)
                        parameters.Add((DefinedTypes[tokens[i - 1].Value], tokens[i].Value));

                    isType = !isType;
                    i++;
                }
                i++;

                MethodAttributes atrs = 0;
                var accessModifierSet = false;
                var modifsLength = modifs.Length;
                for (var j = 0; j < modifsLength; j++)
                {
                    if (ModifierCollection.MethodModifiers.ContainsKey(modifs[j]))
                    {
                        if (accessModifierSet)
                            ExceptionManager.ThrowCompiler(ErrorCode.AccessModifierAlreadySet, "", tokens[i].Line);

                        atrs |= ModifierCollection.MethodModifiers[modifs[j]];
                        accessModifierSet = true;
                    }
                    else if (ModifierCollection.MethodAttributeses.ContainsKey(modifs[j]))
                        atrs |= ModifierCollection.MethodAttributeses[modifs[j]];
                    else
                        ExceptionManager.ThrowCompiler(ErrorCode.ModifierExpected, "", tokens[i].Line);
                }

                var method = Emit.BuildMethod(ReturnType, parameters.Select(x => x.type).ToArray(), (TypeBuilder)TypeBuilder, Name, atrs, CallingConventions.Standard);
                StateStack.Push(new MethodBodyState(StateStack, DefinedTypes, AsmBuilder, TypeBuilder, method, parameters, i));
            }
            else
                ExceptionManager.ThrowCompiler(ErrorCode.UnexpectedToken, "", tokens[i].Line);
        }
    }
}