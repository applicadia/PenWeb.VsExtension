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
    public class PenWebQualifiedBaseTypeReference : CppParseTreeNodeBase
    {
        public PenWebQualifiedBaseTypeReference( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedBaseTypeReference treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebQualifiedNamespaceReference : CppParseTreeNodeBase
    {
        public PenWebQualifiedNamespaceReference( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedNamespaceReference treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebQualifiedReference : CppParseTreeNodeBase
    {
        public PenWebQualifiedReference( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedReference treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebQualifiedUsingDeclarationTargetReference : CppParseTreeNodeBase
    {
        public PenWebQualifiedUsingDeclarationTargetReference( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedUsingDeclarationTargetReference treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebOperatorFunctionId : CppParseTreeNodeBase
    {
        public PenWebOperatorFunctionId( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.OperatorFunctionId treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebParametersAndQualifiers : CppParseTreeNodeBase
    {
        public PenWebParametersAndQualifiers( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ParametersAndQualifiers treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebNameQualifier : CppParseTreeNodeBase
    {
        public PenWebNameQualifier( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.NameQualifier treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebMSDeclSpec : CppParseTreeNodeBase
    {
        public PenWebMSDeclSpec( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MSDeclSpec treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebFunctionArgumentList : CppParseTreeNodeBase
    {
        public PenWebFunctionArgumentList( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.FunctionArgumentList treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebFwdClassSpecifier : CppParseTreeNodeBase
    {
        public PenWebFwdClassSpecifier( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.FwdClassSpecifier treeNode ) : base(parentNode, treeNode)
        {
        }
    }
}
