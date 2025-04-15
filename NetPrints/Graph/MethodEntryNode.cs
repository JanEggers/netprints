using NetPrints.Core;
using System;
using System.Runtime.Serialization;
using System.Linq;

namespace NetPrints.Graph
{
    /// <summary>
    /// Node representing the initial execution node of a method.
    /// </summary>
    [DataContract]
    public class MethodEntryNode : ExecutionEntryNode
    {
        public MethodEntryNode(MethodGraph graph)
            : base(graph)
        {
            AddOutputExecPin("Exec");
        }

        public override string ToString()
        {
            return $"{MethodGraph.Name} Entry";
        }

        public void AddGenericArgument()
        {
            string name = $"T{OutputTypePins.Count}";
            AddOutputTypePin(name, new GenericType(name));
        }

        public void RemoveGenericArgument()
        {
            if (OutputTypePins.Count > 0)
            {
                NodeOutputTypePin otpToRemove = OutputTypePins.Last();

                GraphUtil.DisconnectOutputTypePin(otpToRemove);

                OutputTypePins.Remove(otpToRemove);
            }
        }
    }
}
