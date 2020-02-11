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
    public class CppCodeContext : CppFileContextBase
    {
        public CppHeaderContext CppHeaderContext { get; set; }

        public CppCodeContext( string fileName ) : base( fileName )
        {
        }

        public override void WriteSavedNodes()
        {
            this.InitNodes();
            CppCodeAnalysis.DumpFileJson(this.FileName, $"ClassDefs-cpp.json", SaveTreeNodes.ClassDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"MessageMap-cpp.json", SaveTreeNodes.MessageMap);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"VariableDefs-cpp.json", SaveTreeNodes.VariableDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"VariableRefs-cpp.json", SaveTreeNodes.VariableRefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"MethodDefs-cpp.json", SaveTreeNodes.MethodDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"MethodCalls-cpp.json", SaveTreeNodes.MethodCalls);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"ScreenDefs-cpp.json", SaveTreeNodes.ScreenDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"EnumDefs-cpp.json", SaveTreeNodes.EnumDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"ListDefs-cpp.json", SaveTreeNodes.ListDefs);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"Uncatagorized-cpp.json", SaveTreeNodes.Uncatagorized);
            CppCodeAnalysis.DumpFileJson(this.FileName, $"All-cpp.json", SaveTreeNodes.All);
        }

        public override void ProcessResults()
        {
            foreach (PenWebClassSpecifier penWebClassSpecifier in SaveTreeNodes.ClassDefs) {}

            foreach (PenWebDeclaration penWebDeclaration in SaveTreeNodes.MethodDefs) {}

            foreach (PenWebQualifiedReference penWebQualifiedReference in SaveTreeNodes.VariableRefs)
            {
                CppResultsManager.Self.AddVariableReference(this, penWebQualifiedReference, $"{this.FileName}.cpp");
            }
        }
    }
}
