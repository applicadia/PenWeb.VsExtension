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
    public class PenWebIfStatement : CppParseTreeNodeBase
    {
        public PenWebIfStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.IfStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebSwitchStatement : CppParseTreeNodeBase
    {
        public PenWebSwitchStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.SwitchStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebBreakStatement : CppParseTreeNodeBase
    {
        public PenWebBreakStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.BreakStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCaseStatement : CppParseTreeNodeBase
    {
        public PenWebCaseStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CaseStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }
}
