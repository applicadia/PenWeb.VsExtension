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
        public CppHeaderContext( string fileName ) : base( fileName )
        {
        }

        public override void WriteSavedNodes()
        {
            this.InitNodes();
            CppCodeAnalysis.DumpFileJson(this.FileName, $"ClassDefs-h.json", SaveTreeNodes.ClassDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"MessageMap-h.json", SaveTreeNodes.MessageMap);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"VariableDefs-h.json", SaveTreeNodes.VariableDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"VariableRefs-h.json", SaveTreeNodes.VariableRefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"MethodDefs-h.json", SaveTreeNodes.MethodDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"MethodCalls-h.json", SaveTreeNodes.MethodCalls);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"ScreenDefs-h.json", SaveTreeNodes.ScreenDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"EnumDefs-h.json", SaveTreeNodes.EnumDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"ListDefs-h.json", SaveTreeNodes.ListDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"Uncatagorized-h.json", SaveTreeNodes.Uncatagorized);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"All-h.json", SaveTreeNodes.All);
        }

        public override void ProcessResults()
        {
            foreach (PenWebClassSpecifier penWebClassSpecifier in SaveTreeNodes.ClassDefs)
            {
                CppResultsManager.Self.AddClassResult(this, penWebClassSpecifier);
            }

            foreach (PenWebDeclaration penWebDeclaration in SaveTreeNodes.VariableDefs)
            {
                CppResultsManager.Self.AddVariableDefinition(this, penWebDeclaration, $"{this.FileName}.h" );
            }

            foreach (PenWebQualifiedReference penWebQualifiedReference in SaveTreeNodes.VariableRefs)
            {
                CppResultsManager.Self.AddVariableReference(this, penWebQualifiedReference, $"{this.FileName}.h");
            }
        }
    }
}
