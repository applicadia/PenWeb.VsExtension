using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn, IsReference=false)]
    public class CppVariableReference
    {
        [JsonProperty] public string TypeName     { get; set; }
        [JsonProperty] public string VariableName { get; set; }

        [JsonProperty] public string ClassName    { get; set; }
        [JsonProperty] public string FileName     { get; set; }

        [JsonProperty] public int LineNumber { get; set; }

        public CppVariableReference(string typeName, string variableName, string className, string fileName, int lineNumber)
        {
            this.TypeName     = typeName;
            this.VariableName = variableName;
            this.ClassName    = className;
            this.FileName     = fileName;
            this.LineNumber   = lineNumber;
        }
    }
}
