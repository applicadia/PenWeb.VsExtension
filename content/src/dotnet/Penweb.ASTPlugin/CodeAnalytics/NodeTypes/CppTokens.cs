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
        public CppGenericTreeNode( ITreeNode treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebCppCommentTokenNode : CppParseTreeNodeBase
    {
        public PenWebCppCommentTokenNode( JetBrains.ReSharper.Psi.Cpp.Parsing.CppCommentTokenNode treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebCppFromSubstitutionTokenNode : CppParseTreeNodeBase
    {
        public PenWebCppFromSubstitutionTokenNode( JetBrains.ReSharper.Psi.Cpp.Parsing.CppFromSubstitutionTokenNode treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebCppGenericTokenNode : CppParseTreeNodeBase
    {
        public PenWebCppGenericTokenNode( JetBrains.ReSharper.Psi.Cpp.Parsing.CppGenericTokenNode treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebCppIdentifierTokenNode : CppParseTreeNodeBase
    {
        public PenWebCppIdentifierTokenNode( JetBrains.ReSharper.Psi.Cpp.Parsing.CppIdentifierTokenNode treeNode ) : base(treeNode)
        {
        }
    }
}
