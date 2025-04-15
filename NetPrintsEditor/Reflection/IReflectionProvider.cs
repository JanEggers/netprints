using NetPrints.Core;
using System.Collections.Generic;

namespace NetPrintsEditor.Reflection
{
    public interface IReflectionProviderQuery
    {
    }

    /// <summary>
    /// Interface for reflecting on types, methods etc.
    /// </summary>
    public interface IReflectionProvider
    {
        bool TypeSpecifierIsSubclassOf(TypeSpecifier a, TypeSpecifier b);
        bool HasImplicitCast(TypeSpecifier fromType, TypeSpecifier toType);
        IEnumerable<TypeSpecifier> GetNonStaticTypes();
        IEnumerable<MethodSpecifier> GetOverridableMethodsForType(TypeSpecifier typeSpecifier);
        IEnumerable<MethodSpecifier> GetPublicMethodOverloads(MethodSpecifier methodSpecifier);
        IEnumerable<ConstructorSpecifier> GetConstructors(TypeSpecifier typeSpecifier);
        IEnumerable<string> GetEnumNames(TypeSpecifier typeSpecifier);

        IEnumerable<MethodSpecifier> GetMethods(ReflectionProviderMethodQuery query);
        IEnumerable<VariableSpecifier> GetVariables(ReflectionProviderVariableQuery query);

        // Documentation
        string GetMethodDocumentation(MethodSpecifier methodSpecifier);
        string GetMethodParameterDocumentation(MethodSpecifier methodSpecifier, int parameterIndex);
        string GetMethodReturnDocumentation(MethodSpecifier methodSpecifier, int returnIndex);
    }
}
