﻿using System.Runtime.Serialization;

namespace NetPrints.Core
{
    [DataContract]
    [KnownType(typeof(AssemblyReference))]
    [KnownType(typeof(FrameworkAssemblyReference))]
    [KnownType(typeof(RuntimeAssemblyReference))]
    [KnownType(typeof(SourceDirectoryReference))]
    public abstract class CompilationReference : ICompilationReference
    {
    }
}
