using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn, IsReference=false)]
    public class CppClassResult
    {
        [JsonProperty] public string ClassName { get; set; }

        [JsonProperty] public string ParentClass { get; set; }

        [JsonProperty] public Dictionary<string, CppVariableDefinition> Variables { get; } = new Dictionary<string, CppVariableDefinition>();

        [JsonProperty] public Dictionary<string, int> MissingVarNames { get; } = new Dictionary<string, int>();

        public CppClassResult(string className, string parentClass)
        {
            this.ClassName   = className;
            this.ParentClass = parentClass;
        }

        public bool AddVariableDefinition(CppVariableDefinition cppVariableDefinition)
        {
            if (!this.Variables.ContainsKey(cppVariableDefinition.VariableName))
            {
                this.Variables.Add(cppVariableDefinition.VariableName, cppVariableDefinition);
                return true;
            }
            else
            {
                Console.WriteLine($"CppClassResult Duplicate Variable: {cppVariableDefinition.TypeName} {cppVariableDefinition.VariableName} {cppVariableDefinition.LineNumber}");
                return false;
            }
        }

        public CppVariableReference AddVariableReference(string variableName, string fileName, int lineNumber)
        {
            if (this.Variables.ContainsKey(variableName))
            {
                CppVariableDefinition variableDefinition = this.Variables[variableName];
                return variableDefinition.AddReferences(fileName, lineNumber);
            }
            else
            {
                Console.WriteLine($"Unknown Variable: {variableName}");

                if (this.MissingVarNames.ContainsKey(variableName))
                {
                    this.MissingVarNames[variableName]++;
                }
                else
                {
                    this.MissingVarNames.Add(variableName, 1);
                }
                return null;
            }
        }
    }
}
