using JetBrains.ReSharper.Psi.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PenWeb.ASTPlugin;

namespace Penweb.CodeAnalytics
{
    public class CppHeaderContext : CppFileContextBase
    {
        public CppCodeContext CppCodeContext { get; set; }
        public CppHeaderContext( string fileName, string dialogClassName ) : base( fileName, dialogClassName )
        {
        }

        public override void WriteSavedNodes()
        {
            this.InitNodes();
            CppParseManager.DumpFileJson(this.FileName, $"ClassDefs-h.json", ParseResults.ClassDefs);
            CppParseManager.DumpFileJson(this.FileName, $"MessageMap-h.json", ParseResults.MessageMap);
            CppParseManager.DumpFileJson(this.FileName, $"VariableDefs-h.json", ParseResults.VariableDefs);
            CppParseManager.DumpFileJson(this.FileName, $"VariableRefs-h.json", ParseResults.VariableRefs);
            CppParseManager.DumpFileJson(this.FileName, $"MethodDefs-h.json", ParseResults.MethodDefs);
            CppParseManager.DumpFileJson(this.FileName, $"MethodCalls-h.json", ParseResults.MethodCalls);
            CppParseManager.DumpFileJson(this.FileName, $"DDXCalls-h.json", ParseResults.DDxCalls);
            CppParseManager.DumpFileJson(this.FileName, $"ScreenDefs-h.json", ParseResults.ScreenDefs);
            CppParseManager.DumpFileJson(this.FileName, $"EnumDefs-h.json", ParseResults.EnumDefs);
            CppParseManager.DumpFileJson(this.FileName, $"ListDefs-h.json", ParseResults.ListDefs);
            CppParseManager.DumpFileJson(this.FileName, $"Uncatagorized-h.json", ParseResults.Uncatagorized);
            CppParseManager.DumpFileJson(this.FileName, $"All-h.json", ParseResults.All);
        }

        public override void ProcessResults()
        {
            foreach (PenWebClassSpecifier penWebClassSpecifier in ParseResults.ClassDefs)
            {
                CppResultsManager.Self.AddClassResult(this, penWebClassSpecifier);
            }

            foreach (PenWebDeclaration penWebDeclaration in ParseResults.VariableDefs)
            {
                CppResultsManager.Self.AddVariableDefinition(this, penWebDeclaration, $"{this.FileName}.h" );
            }

            foreach (PenWebQualifiedReference penWebQualifiedReference in ParseResults.VariableRefs)
            {
                CppResultsManager.Self.AddVariableReference(this, penWebQualifiedReference, $"{this.FileName}.h");
            }
        }
    }
}
