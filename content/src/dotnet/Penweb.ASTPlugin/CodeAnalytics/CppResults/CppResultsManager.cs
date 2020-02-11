using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    public class CppResultsManager
    {
        public static CppResultsManager Self;

        public Dictionary<string, CppTypeResult> TypeMap { get; } = new Dictionary<string, CppTypeResult>();

        public Dictionary<string, CppClassResult> ClassMap { get; } = new Dictionary<string, CppClassResult>();

        public Dictionary<string, int> MissingDefClassNames = new Dictionary<string, int>();
        public Dictionary<string, int> MissingRefClassNames = new Dictionary<string, int>();

        private CppResultsManager()
        {
            Self = this;
        }

        public void Clear()
        {
            this.TypeMap.Clear();
            this.ClassMap.Clear();
            this.MissingDefClassNames.Clear();
            this.MissingRefClassNames.Clear();
        }

        public void WriteResults()
        {
            string typeMapPath = CppCodeAnalysis.CreateAnalyticsFilePath("CppTypeMap.json");

            string jsonData = JsonConvert.SerializeObject(this.TypeMap, Formatting.Indented);

            File.WriteAllText(typeMapPath, jsonData);

            string classMapPath = CppCodeAnalysis.CreateAnalyticsFilePath("CppClassMap.json");

            jsonData = JsonConvert.SerializeObject(this.ClassMap, Formatting.Indented);

            File.WriteAllText(classMapPath, jsonData);

            string missingRefMapPath = CppCodeAnalysis.CreateAnalyticsFilePath("MissingRefClassNames.json");

            jsonData = JsonConvert.SerializeObject(this.MissingRefClassNames, Formatting.Indented);

            File.WriteAllText(missingRefMapPath, jsonData);

            string missingDefMapPath = CppCodeAnalysis.CreateAnalyticsFilePath("MissingDefClassNames.json");

            jsonData = JsonConvert.SerializeObject(this.MissingDefClassNames, Formatting.Indented);

            File.WriteAllText(missingDefMapPath, jsonData);
        }

        public CppClassResult AddClassResult(CppFileContextBase cppFileContext, PenWebClassSpecifier penWebClassSpecifier)
        {
            CppClassResult cppClassResult = null;

            if (!this.ClassMap.ContainsKey(penWebClassSpecifier.ClassName))
            {
                cppClassResult = new CppClassResult(penWebClassSpecifier.ClassName, penWebClassSpecifier.BaseClass);
                this.ClassMap.Add(penWebClassSpecifier.ClassName, cppClassResult);
                return cppClassResult;
            }
            else
            {
                return this.ClassMap[penWebClassSpecifier.ClassName];
            }
        }

        public void AddVariableDefinition(CppFileContextBase cppFileContext, PenWebDeclaration penWebDeclaration, string fileName)
        {
            CppTypeResult cppTypeResult = null;

            if (this.TypeMap.ContainsKey(penWebDeclaration.TypeName))
            {
                cppTypeResult = this.TypeMap[penWebDeclaration.TypeName];
            }
            else
            {
                cppTypeResult = new CppTypeResult(penWebDeclaration.TypeName);
                this.TypeMap.Add(penWebDeclaration.TypeName, cppTypeResult);
            }

            CppVariableDefinition cppVariableDefinition = cppTypeResult.AddDefinition(penWebDeclaration.VariableName, penWebDeclaration.OwningClass, fileName, penWebDeclaration.Location.StartLine);

            if (this.ClassMap.ContainsKey(penWebDeclaration.OwningClass))
            {
                CppClassResult cppClassResult = this.ClassMap[penWebDeclaration.OwningClass];
            }
            else
            {
                if (this.MissingDefClassNames.ContainsKey(penWebDeclaration.OwningClass))
                {
                    this.MissingDefClassNames[penWebDeclaration.OwningClass]++;
                }
                else
                {
                    this.MissingDefClassNames.Add(penWebDeclaration.OwningClass, 1);
                }

                Console.WriteLine($"Unknown class: {penWebDeclaration.OwningClass}");
            }
        }

        public void AddVariableReference(CppFileContextBase cppFileContext, PenWebQualifiedReference penWebQualifiedReference, string fileName)
        {
            if (this.ClassMap.ContainsKey(penWebQualifiedReference.ClassName))
            {
                CppClassResult cppClassResult = this.ClassMap[penWebQualifiedReference.ClassName];

                CppVariableReference cppVariableReference = cppClassResult.AddVariableReference(penWebQualifiedReference.ItemName, fileName, penWebQualifiedReference.Location.StartLine);
            }
            else
            {
                if (this.MissingRefClassNames.ContainsKey(penWebQualifiedReference.ClassName))
                {
                    this.MissingRefClassNames[penWebQualifiedReference.ClassName]++;
                }
                else
                {
                    this.MissingRefClassNames.Add(penWebQualifiedReference.ClassName, 1);
                }

                Console.WriteLine($"Unknown ClassName: {penWebQualifiedReference.ClassName}");
            }
        }

        public static void Start()
        {
            new CppResultsManager();
        }
    }
}
