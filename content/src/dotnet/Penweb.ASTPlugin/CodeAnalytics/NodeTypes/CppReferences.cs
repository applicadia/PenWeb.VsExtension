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
        public PenWebQualifiedBaseTypeReference( JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedBaseTypeReference treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebQualifiedNamespaceReference : CppParseTreeNodeBase
    {
        public PenWebQualifiedNamespaceReference( JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedNamespaceReference treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebQualifiedReference : CppParseTreeNodeBase
    {
        public PenWebQualifiedReference( JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedReference treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebQualifiedUsingDeclarationTargetReference : CppParseTreeNodeBase
    {
        public PenWebQualifiedUsingDeclarationTargetReference( JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedUsingDeclarationTargetReference treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebOperatorFunctionId : CppParseTreeNodeBase
    {
        public PenWebOperatorFunctionId( JetBrains.ReSharper.Psi.Cpp.Tree.OperatorFunctionId treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebParametersAndQualifiers : CppParseTreeNodeBase
    {
        public PenWebParametersAndQualifiers( JetBrains.ReSharper.Psi.Cpp.Tree.ParametersAndQualifiers treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebNameQualifier : CppParseTreeNodeBase
    {
        public PenWebNameQualifier( JetBrains.ReSharper.Psi.Cpp.Tree.NameQualifier treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebMSDeclSpec : CppParseTreeNodeBase
    {
        public PenWebMSDeclSpec( JetBrains.ReSharper.Psi.Cpp.Tree.MSDeclSpec treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebFunctionArgumentList : CppParseTreeNodeBase
    {
        public PenWebFunctionArgumentList( JetBrains.ReSharper.Psi.Cpp.Tree.FunctionArgumentList treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebFwdClassSpecifier : CppParseTreeNodeBase
    {
        public PenWebFwdClassSpecifier( JetBrains.ReSharper.Psi.Cpp.Tree.FwdClassSpecifier treeNode ) : base(treeNode)
        {
        }
    }
}
