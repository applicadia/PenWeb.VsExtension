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
            CppCodeAnalysis.DumpJson($"{this.FileName}-h.json", SaveTreeNodes);
        }
    }
}
