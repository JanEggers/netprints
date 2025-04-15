using NetPrints.Core;
using System;
using System.Collections.Generic;

namespace NetPrintsEditor.Reflection
{
    public class ReflectionProviderMethodQuery : IReflectionProviderQuery, IEqualityComparer<ReflectionProviderMethodQuery>
    {
        public TypeSpecifier Type { get; set; }
        public bool? Static { get; set; }
        public TypeSpecifier VisibleFrom { get; set; }
        public TypeSpecifier ReturnType { get; set; }
        public TypeSpecifier ArgumentType { get; set; }
        public bool? HasGenericArguments { get; set; }

        public ReflectionProviderMethodQuery WithType(TypeSpecifier type)
        {
            Type = type;
            return this;
        }

        public ReflectionProviderMethodQuery WithStatic(bool isStatic)
        {
            Static = isStatic;
            return this;
        }

        public ReflectionProviderMethodQuery WithVisibleFrom(TypeSpecifier visibleFrom)
        {
            VisibleFrom = visibleFrom;
            return this;
        }

        public ReflectionProviderMethodQuery WithReturnType(TypeSpecifier returnType)
        {
            ReturnType = returnType;
            return this;
        }

        public ReflectionProviderMethodQuery WithArgumentType(TypeSpecifier argumentType)
        {
            ArgumentType = argumentType;
            return this;
        }

        public ReflectionProviderMethodQuery WithHasGenericArguments(bool hasGenericArguments)
        {
            HasGenericArguments = hasGenericArguments;
            return this;
        }

        public bool Equals(ReflectionProviderMethodQuery x, ReflectionProviderMethodQuery y)
        {
            return x.Type == y.Type && x.Static == y.Static && x.VisibleFrom == y.VisibleFrom
                && x.ReturnType == y.ReturnType && x.ArgumentType == y.ArgumentType && x.HasGenericArguments == y.HasGenericArguments;
        }

        public int GetHashCode(ReflectionProviderMethodQuery obj)
        {
            return HashCode.Combine(Type, Static, VisibleFrom, ReturnType, ArgumentType, HasGenericArguments);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj)
                || (obj is ReflectionProviderMethodQuery query && Equals(this, query));
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }
    }
}
