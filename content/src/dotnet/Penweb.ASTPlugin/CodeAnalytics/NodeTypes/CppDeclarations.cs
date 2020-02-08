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
    public class PenWebTypeId : CppParseTreeNodeBase
    {
        public PenWebTypeId( JetBrains.ReSharper.Psi.Cpp.Tree.TypeId treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebNestedDeclarator : CppParseTreeNodeBase
    {
        public PenWebNestedDeclarator( JetBrains.ReSharper.Psi.Cpp.Tree.NestedDeclarator treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebInitDeclarator : CppParseTreeNodeBase
    {
        public PenWebInitDeclarator( JetBrains.ReSharper.Psi.Cpp.Tree.InitDeclarator treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebDeclarationStatement : CppParseTreeNodeBase
    {
        public PenWebDeclarationStatement( JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationStatement treeNode ) : base(treeNode)
        {
        }
    }


    public class PenWebDeclaratorQualifiedName : CppParseTreeNodeBase
    {
        public PenWebDeclaratorQualifiedName( JetBrains.ReSharper.Psi.Cpp.Tree.DeclaratorQualifiedName treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebCtorInitializer : CppParseTreeNodeBase
    {
        public PenWebCtorInitializer( JetBrains.ReSharper.Psi.Cpp.Tree.CtorInitializer treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebClassQualifiedName : CppParseTreeNodeBase
    {
        public PenWebClassQualifiedName( JetBrains.ReSharper.Psi.Cpp.Tree.ClassQualifiedName treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebBaseClause : CppParseTreeNodeBase
    {
        public PenWebBaseClause( JetBrains.ReSharper.Psi.Cpp.Tree.BaseClause treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebBaseSpecifier : CppParseTreeNodeBase
    {
        public PenWebBaseSpecifier( JetBrains.ReSharper.Psi.Cpp.Tree.BaseSpecifier treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebAbstractDeclarator : CppParseTreeNodeBase
    {
        public PenWebAbstractDeclarator( JetBrains.ReSharper.Psi.Cpp.Tree.AbstractDeclarator treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebAbstractDeclaratorName : CppParseTreeNodeBase
    {
        public PenWebAbstractDeclaratorName( JetBrains.ReSharper.Psi.Cpp.Tree.AbstractDeclaratorName treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebDeclarator : CppParseTreeNodeBase
    {
        public PenWebDeclarator( JetBrains.ReSharper.Psi.Cpp.Tree.Declarator treeNode ) : base(treeNode)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class PenWebDeclaration : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.Declaration Declaration { get; set; }

        [JsonProperty] public string ClassName  { get; set; }
        [JsonProperty] public string MethodName { get; set; }

        public PenWebDeclaration( JetBrains.ReSharper.Psi.Cpp.Tree.Declaration treeNode ) : base(treeNode)
        {
            this.Declaration = treeNode;
        }

        public override void Init()
        {
            try
            {
                base.Init();

                this.SaveToJson = true;

                CppDeclarationSymbol symbol = this.Declaration.GetSymbol();

                if (symbol != null)
                {
                    this.MethodName = symbol.GetQualifiedName().GetNameStr();
                }
                else
                {
                    Console.WriteLine($"PenWebDeclaration() symbol is null");
                }

                AttributeList attributeList = this.Declaration.AttributeListNode;

                if (attributeList != null)
                {
                    foreach (Attribute attribute in attributeList.Children())
                    {
                        var typeId    = attribute.TypeId;
                        var toString  = attribute.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            this.Declaration = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} ClassName: {this.ClassName} MethodName: {this.MethodName} |{SingleLineText}|";
        }
    }


    public class PenWebSimpleDeclaration : CppParseTreeNodeBase
    {
        private JetBrains.ReSharper.Psi.Cpp.Tree.SimpleDeclaration SimpleDeclaration { get; set; }

        [JsonProperty] public string ClassName  { get; set; }
        [JsonProperty] public string MethodName { get; set; }

        public PenWebSimpleDeclaration( JetBrains.ReSharper.Psi.Cpp.Tree.SimpleDeclaration treeNode ) : base(treeNode)
        {
            this.SimpleDeclaration = treeNode;
        }

        public override void Init()
        {
            try
            {
                base.Init();

                //this.SaveToJson = true;

                Declaration declarationNode = this.SimpleDeclaration.DeclarationNode;

                if (declarationNode != null)
                {
                    CppDeclarationSymbol symbol = declarationNode.GetSymbol();

                    if (symbol != null)
                    {
                        this.MethodName = symbol.GetQualifiedName().GetNameStr();
                    }
                    else
                    {
                        Console.WriteLine($"PenWebSimpleDeclaration() declarationNode.symbol is null");
                    }
                }
                else
                {
                    Console.WriteLine($"PenWebSimpleDeclaration() declarationNode is null");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            this.SimpleDeclaration = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} ClassName: {this.ClassName} MethodName: {this.MethodName} |{SingleLineText}|";
        }
    }


}
