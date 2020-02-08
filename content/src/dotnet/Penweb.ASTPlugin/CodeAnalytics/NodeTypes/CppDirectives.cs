using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.ReSharper.Psi.Cpp.Symbols;
using JetBrains.ReSharper.Psi.Cpp.Tree;
using Newtonsoft.Json;
using PenWeb.ASTPlugin;
using JetBrains.ReSharper.Psi.Cpp.Expressions;

namespace Penweb.CodeAnalytics
{
    public class PenWebDirective : CppParseTreeNodeBase
    {
        public PenWebDirective( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.Directive treeNode ) : base(parentNode, treeNode)
        {
        }
    }
}
