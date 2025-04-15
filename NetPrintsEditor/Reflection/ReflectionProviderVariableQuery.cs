using NetPrints.Core;
using System;
using System.Collections.Generic;

namespace NetPrintsEditor.Reflection
{
    public class ReflectionProviderVariableQuery : IReflectionProviderQuery, IEqualityComparer<ReflectionProviderVariableQuery>
    {
        public ReflectionProviderVariableQuery()
        {
        }

        public TypeSpecifier Type { get; set; }
        public bool? Static { get; set; }
        public TypeSpecifier VisibleFrom { get; set; }
        public TypeSpecifier VariableType { get; set; }
        public bool VariableTypeDerivesFrom { get; set; } = false;

        public ReflectionProviderVariableQuery WithType(TypeSpecifier type)
        {
            Type = type;
            return this;
        }

        public ReflectionProviderVariableQuery WithStatic(bool isStatic)
        {
            Static = isStatic;
            return this;
        }

        public ReflectionProviderVariableQuery WithVisibleFrom(TypeSpecifier visibleFrom)
        {
            VisibleFrom = visibleFrom;
            return this;
        }

        public ReflectionProviderVariableQuery WithVariableType(TypeSpecifier type, bool derivesFrom = false)
        {
            VariableType = type;
            VariableTypeDerivesFrom = derivesFrom;
            return this;
        }

        public bool Equals(ReflectionProviderVariableQuery x, ReflectionProviderVariableQuery y)
        {
            return x.Type == y.Type && x.Static == y.Static && x.VisibleFrom == y.VisibleFrom
                && x.VariableType == y.VariableType && x.VariableTypeDerivesFrom == y.VariableTypeDerivesFrom;
        }

        public int GetHashCode(ReflectionProviderVariableQuery obj)
        {
            return HashCode.Combine(Type, Static, VisibleFrom, VariableType, VariableTypeDerivesFrom);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj)
                || (obj is ReflectionProviderVariableQuery query && Equals(this, query));
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }
    }
}
