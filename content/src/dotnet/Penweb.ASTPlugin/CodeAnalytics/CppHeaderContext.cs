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
            CppParseManager.DumpFileJson(this.DialogClassName, $"ClassDefs-h.json", ParseResults.ClassDefs);
            CppParseManager.DumpFileJson(this.DialogClassName, $"MessageMap-h.json", ParseResults.MessageMap);
            CppParseManager.DumpFileJson(this.DialogClassName, $"VariableDefs-h.json", ParseResults.VariableDefs);
            CppParseManager.DumpFileJson(this.DialogClassName, $"VariableRefs-h.json", ParseResults.VariableRefs);
            CppParseManager.DumpFileJson(this.DialogClassName, $"MethodDefs-h.json", ParseResults.MethodDefs);
            CppParseManager.DumpFileJson(this.DialogClassName, $"MethodCalls-h.json", ParseResults.MethodCalls);
            CppParseManager.DumpFileJson(this.DialogClassName, $"DDXCalls-h.json", ParseResults.DDxCalls);
            CppParseManager.DumpFileJson(this.DialogClassName, $"ScreenDefs-h.json", ParseResults.ScreenDefs);
            CppParseManager.DumpFileJson(this.DialogClassName, $"EnumDefs-h.json", ParseResults.EnumDefs);
            CppParseManager.DumpFileJson(this.DialogClassName, $"ListDefs-h.json", ParseResults.ListDefs);
            CppParseManager.DumpFileJson(this.DialogClassName, $"Uncatagorized-h.json", ParseResults.Uncatagorized);
            CppParseManager.DumpFileJson(this.DialogClassName, $"All-h.json", ParseResults.All);
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
