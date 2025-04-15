using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NetPrintsEditor.Reflection
{
    public static class ISymbolExtensions
    {
        private static readonly Dictionary<ITypeSymbol, List<ISymbol>> allMembersCache = new Dictionary<ITypeSymbol, List<ISymbol>>();

        /// <summary>
        /// Gets all members of a symbol including inherited ones, but not overriden ones.
        /// </summary>
        public static IEnumerable<ISymbol> GetAllMembers(this ITypeSymbol symbol)
        {
            if (allMembersCache.TryGetValue(symbol, out var allMembers))
            {
                return allMembers;
            }

            var members = new List<ISymbol>();
            var overridenMethods = new HashSet<IMethodSymbol>();

            var startSymbol = symbol;

            while (symbol != null)
            {
                var symbolMembers = symbol.GetMembers();

                // Add symbols which weren't overriden yet
                List<ISymbol> newMembers = symbolMembers.Where(m => !(m is IMethodSymbol methodSymbol) || !overridenMethods.Contains(methodSymbol)).ToList();

                members.AddRange(newMembers);

                // Recursively add overriden methods
                List<IMethodSymbol> newOverridenMethods = symbolMembers.OfType<IMethodSymbol>().ToList();
                while (newOverridenMethods.Count > 0)
                {
                    newOverridenMethods.ForEach(m => overridenMethods.Add(m));
                    newOverridenMethods = newOverridenMethods
                        .Where(m => m.OverriddenMethod != null)
                        .Select(m => m.OverriddenMethod)
                        .ToList();
                }

                symbol = symbol.BaseType;
            }

            allMembersCache.Add(startSymbol, members.ToList());

            return members;
        }

        public static bool IsPublic(this ISymbol symbol)
        {
            return symbol.DeclaredAccessibility == Microsoft.CodeAnalysis.Accessibility.Public;
        }

        public static bool IsProtected(this ISymbol symbol)
        {
            return symbol.DeclaredAccessibility == Microsoft.CodeAnalysis.Accessibility.Protected;
        }

        public static IEnumerable<IMethodSymbol> GetMethods(this ITypeSymbol symbol)
        {
            return symbol.GetAllMembers()
                    .Where(member => member.Kind == SymbolKind.Method)
                    .Cast<IMethodSymbol>()
                    .Where(method => method.MethodKind == MethodKind.Ordinary || method.MethodKind == MethodKind.BuiltinOperator || method.MethodKind == MethodKind.UserDefinedOperator);
        }

        public static IEnumerable<IMethodSymbol> GetConverters(this ITypeSymbol symbol)
        {
            return symbol.GetAllMembers()
                    .Where(member => member.Kind == SymbolKind.Method)
                    .Cast<IMethodSymbol>()
                    .Where(method => method.MethodKind == MethodKind.Conversion);
        }

        public static bool IsSubclassOf(this ITypeSymbol symbol, ITypeSymbol cls)
        {
            // If cls is an interface type, check if the interface is implemented
            // TODO: Currently only checking full name and type parameter count for interfaces.
            if (symbol != null && cls.TypeKind == TypeKind.Interface && cls is INamedTypeSymbol namedCls)
            {
                bool IsSameInterface(INamedTypeSymbol a, INamedTypeSymbol b)
                {
                    return a.GetFullName() == b.GetFullName() && a.TypeParameters.Length == b.TypeParameters.Length;
                }

                return (symbol is INamedTypeSymbol namedSymbol && IsSameInterface(namedSymbol, namedCls))
                    || symbol.AllInterfaces.Any(interf =>
                        cls is INamedTypeSymbol namedCls && IsSameInterface(interf, namedCls));
            }

            // Traverse base types to find out if symbol inherits from cls
            ITypeSymbol candidateBaseType = symbol;
            while (candidateBaseType != null)
            {
                if (candidateBaseType == cls)
                {
                    return true;
                }

                candidateBaseType = candidateBaseType.BaseType;
            }

            return false;
        }

        public static string GetFullName(this ITypeSymbol typeSymbol)
        {
            string fullName = typeSymbol.MetadataName;
            if (typeSymbol.ContainingNamespace != null && !typeSymbol.ContainingNamespace.IsGlobalNamespace)
            {
                fullName = $"{typeSymbol.ContainingNamespace.MetadataName}.{fullName}";
            }
            return fullName;
        }
    }
}
