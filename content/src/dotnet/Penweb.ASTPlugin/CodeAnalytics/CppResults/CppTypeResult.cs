using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn, IsReference=false)]
    public class CppTypeResult
    {
        [JsonProperty] public string TypeName { get; set; }

        [JsonProperty] public List<CppVariableDefinition> References { get; } = new List<CppVariableDefinition>();

        public CppTypeResult(string typeName)
        {
            this.TypeName = typeName;
        }

        public CppVariableDefinition AddDefinition(string variableName, string className, string fileName, int lineNumber)
        {
            CppVariableDefinition cppVariableDefinition = new CppVariableDefinition(this.TypeName, variableName, className, fileName, lineNumber);

            this.References.Add(cppVariableDefinition);

            return cppVariableDefinition;
        }

    }
}
