using System;
using System.IO;
using System.Runtime.Serialization;

namespace NetPrints.Core
{
    [DataContract]
    public class RuntimeAssemblyReference : AssemblyReference
    { 
        /// <summary>
        /// Path relative to reference assemblies path.
        /// </summary>
        public string RuntimeRelativePath
        {
            get => runtimeRelativePath;
            private set
            {
                runtimeRelativePath = value;
                UpdateAssemblyPath();
            }
        }

        [DataMember]
        private string runtimeRelativePath;

        public RuntimeAssemblyReference(string relativePath)
            : base(null)
        {
            RuntimeRelativePath = relativePath;
        }

        private void UpdateAssemblyPath()
        {
            AssemblyPath = Path.Combine(RuntimeRootFolder, RuntimeRelativePath);
        }

        public static string RuntimeRootFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "dotnet", "shared", "Microsoft.NETCore.App");

        public override string ToString() =>
            $"Runtime reference assembly {RuntimeRelativePath} found at {AssemblyPath}";
    }
}
