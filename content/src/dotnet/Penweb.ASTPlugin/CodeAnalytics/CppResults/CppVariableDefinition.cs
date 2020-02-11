using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn, IsReference=false)]
    public class CppVariableDefinition
    {
        [JsonProperty] public string TypeName     { get; set; }
        [JsonProperty] public string VariableName { get; set; }

        [JsonProperty] public string ClassName    { get; set; }
        [JsonProperty] public string FileName     { get; set; }

        [JsonProperty] public int LineNumber { get; set; }

        [JsonProperty] public List<CppVariableReference> References { get; set; } = new List<CppVariableReference>();

        public CppVariableDefinition(string typeName, string variableName, string className, string fileName, int lineNumber)
        {
            this.TypeName     = typeName;
            this.VariableName = variableName;
            this.ClassName    = className;
            this.FileName     = fileName;
            this.LineNumber   = lineNumber;
        }

        public CppVariableReference AddReferences(string fileName, int lineNumber )
        {
            CppVariableReference cppVariableReference = new CppVariableReference(this.TypeName, this.VariableName, this.ClassName, fileName, lineNumber);
            this.References.Add(cppVariableReference);
            return cppVariableReference;
        }
    }
}
