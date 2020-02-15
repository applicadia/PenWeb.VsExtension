using JetBrains.ReSharper.Psi.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PenWeb.ASTPlugin;
using Newtonsoft.Json;

namespace Penweb.CodeAnalytics
{
    public class CppCodeContext : CppFileContextBase
    {
        public CppHeaderContext CppHeaderContext { get; set; }

        public WidgetTypeFinder WidgetTypeFinder { get; set; }

        public CppCodeContext( string fileName, string dialogClassName ) : base( fileName, dialogClassName )
        {
        }

        public override void WriteSavedNodes()
        {
            this.InitNodes();
            CppParseManager.DumpFileJson(this.FileName, $"ClassDefs-cpp.json", ParseResults.ClassDefs);
            CppParseManager.DumpFileJson(this.FileName, $"MessageMap-cpp.json", ParseResults.MessageMap);
            CppParseManager.DumpFileJson(this.FileName, $"VariableDefs-cpp.json", ParseResults.VariableDefs);
            CppParseManager.DumpFileJson(this.FileName, $"VariableRefs-cpp.json", ParseResults.VariableRefs);
            CppParseManager.DumpFileJson(this.FileName, $"MethodDefs-cpp.json", ParseResults.MethodDefs);
            CppParseManager.DumpFileJson(this.FileName, $"MethodCalls-cpp.json", ParseResults.MethodCalls);
            CppParseManager.DumpFileJson(this.FileName, $"DDXCalls-cpp.json", ParseResults.DDxCalls);
            CppParseManager.DumpFileJson(this.FileName, $"ScreenDefs-cpp.json", ParseResults.ScreenDefs);
            CppParseManager.DumpFileJson(this.FileName, $"EnumDefs-cpp.json", ParseResults.EnumDefs);
            CppParseManager.DumpFileJson(this.FileName, $"ListDefs-cpp.json", ParseResults.ListDefs);
            CppParseManager.DumpFileJson(this.FileName, $"Uncatagorized-cpp.json", ParseResults.Uncatagorized);
            CppParseManager.DumpFileJson(this.FileName, $"All-cpp.json", ParseResults.All);
        }

        public override void ProcessResults()
        {
            foreach (PenWebClassSpecifier penWebClassSpecifier in ParseResults.ClassDefs) {}

            foreach (PenWebDeclaration penWebDeclaration in ParseResults.MethodDefs) {}

            foreach (PenWebQualifiedReference penWebQualifiedReference in ParseResults.VariableRefs)
            {
                CppResultsManager.Self.AddVariableReference(this, penWebQualifiedReference, $"{this.FileName}.cpp");
            }
        }

        public void SaveClassInfo()
        {
            string classInfoPath = CppParseManager.CreateAnalyticsFilePath(this.DialogClassName, $"ClassMap.json");

            if (CppResultsManager.Self.ClassMap.ContainsKey(this.DialogClassName))
            {
                CppClassResult cppClassResult = CppResultsManager.Self.ClassMap[this.DialogClassName];

                string jsonData = JsonConvert.SerializeObject(cppClassResult,Formatting.Indented);

                File.WriteAllText(classInfoPath, jsonData);

                CppResultsManager.Self.ClassMap.Remove(this.DialogClassName);
            }
        }
    }
}
