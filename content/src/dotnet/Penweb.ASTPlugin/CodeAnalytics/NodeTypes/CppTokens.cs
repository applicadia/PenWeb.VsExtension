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
    public class CppGenericTreeNode : CppParseTreeNodeBase
    {
        public CppGenericTreeNode( CppParseTreeNodeBase parentNode, ITreeNode treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCppCommentTokenNode : CppParseTreeNodeBase
    {
        public PenWebCppCommentTokenNode( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Parsing.CppCommentTokenNode treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCppFromSubstitutionTokenNode : CppParseTreeNodeBase
    {
        public PenWebCppFromSubstitutionTokenNode( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Parsing.CppFromSubstitutionTokenNode treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCppGenericTokenNode : CppParseTreeNodeBase
    {
        public PenWebCppGenericTokenNode( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Parsing.CppGenericTokenNode treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCppIdentifierTokenNode : CppParseTreeNodeBase
    {
        public PenWebCppIdentifierTokenNode( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Parsing.CppIdentifierTokenNode treeNode ) : base(parentNode, treeNode)
        {
        }
    }
}
