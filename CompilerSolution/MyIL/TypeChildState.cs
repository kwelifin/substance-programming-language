﻿using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace IL2MSIL
{
    internal abstract class TypeChildState:AssemblyChildState
    {
        protected readonly TypeBuilder TypeBuilder;

        public TypeChildState(Stack<State> stateStack, Dictionary<string, Type> definedTypes, AssemblyBuilder asmBuilder, TypeBuilder typeBuilder) : base(stateStack,definedTypes, asmBuilder)
        {
            TypeBuilder = typeBuilder;
        }
    }
}