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
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Cpp.Types;

namespace Penweb.CodeAnalytics
{
    public class PenWebTypeId : CppParseTreeNodeBase
    {
        public PenWebTypeId( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.TypeId treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebNestedDeclarator : CppParseTreeNodeBase
    {
        public PenWebNestedDeclarator( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.NestedDeclarator treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebDeclarationStatement : CppParseTreeNodeBase
    {
        public PenWebDeclarationStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCtorInitializer : CppParseTreeNodeBase
    {
        public PenWebCtorInitializer( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CtorInitializer treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebClassQualifiedName : CppParseTreeNodeBase
    {
        public PenWebClassQualifiedName( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ClassQualifiedName treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebBaseClause : CppParseTreeNodeBase
    {
        public PenWebBaseClause( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.BaseClause treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebBaseSpecifier : CppParseTreeNodeBase
    {
        public PenWebBaseSpecifier( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.BaseSpecifier treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebAbstractDeclarator : CppParseTreeNodeBase
    {
        public PenWebAbstractDeclarator( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.AbstractDeclarator treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebAbstractDeclaratorName : CppParseTreeNodeBase
    {
        public PenWebAbstractDeclaratorName( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.AbstractDeclaratorName treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebDeclarator : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.Declarator Declarator { get; set; }

        [JsonProperty] public string ItemName  { get; set; }

        public PenWebDeclarator( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.Declarator treeNode ) : base(parentNode, treeNode)
        {
            this.Declarator = treeNode;
        }

        public override void Init()
        {
            try
            {
                this.ItemName = this.Declarator.DeclaredName;

                base.Init();

                //this.CppFunctionCatagory = CppFunctionCatagory.VariableDef;
                //this.SaveToJson = true;

            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebDeclarator Exception", e);
            }

            this.Declarator = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name}  TypeName: {this.ItemName} |{SingleLineText}|";
        }
    }

    public class PenWebInitDeclarator : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.InitDeclarator InitDeclarator { get; set; }

        [JsonProperty] public string ItemName  { get; set; }

        public PenWebInitDeclarator( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.InitDeclarator treeNode ) : base(parentNode, treeNode)
        {
            this.InitDeclarator = treeNode;
        }

        public override void Init()
        {
            try
            {
                this.ItemName = this.InitDeclarator.DeclaredName;

                IDeclaredElement declaredElement = this.InitDeclarator.DeclaredElement;

                if (declaredElement != null)
                {
                    foreach (IDeclaration declaration in declaredElement.GetDeclarations())
                    {
                        IDeclaredElement childDeclaredElement = declaration.DeclaredElement;
                        string childDeclarationName = declaration.DeclaredName;
                    }
                }

                ICppExpressionsArgumentListNode arguementList = this.InitDeclarator.ArgumentList;

                if (arguementList != null)
                {
                    foreach (ITreeNode attribute in arguementList.Children())
                    {
                        var typeId    = attribute.GetType().Name;
                        var text  = attribute.GetText();
                    }
                }


                base.Init();

                this.CppFunctionCatagory = CppFunctionCatagory.None;
                this.SaveToJson = true;

            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebInitDeclarator Exception",e);
            }

            this.InitDeclarator = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name}  TypeName: {this.ItemName} |{SingleLineText}|";
        }
    }

    public class PenWebDeclaration : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.Declaration Declaration { get; set; }

        [JsonProperty] public string OwningClass  { get; set; }
        [JsonProperty] public string TypeName     { get; set; }

        [JsonProperty] public string VariableName     { get; set; }

        public PenWebDeclaration( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.Declaration treeNode ) : base(parentNode, treeNode)
        {
            this.Declaration = treeNode;
        }

        public override void Init()
        {
            try
            {
                CppDeclarationSymbol symbol = this.Declaration.GetSymbol();

                if (symbol != null)
                {
                    this.VariableName = symbol.GetQualifiedName().GetNameStr();
                }
                else
                {
                    LogManager.Self.Log($"PenWebDeclaration() symbol is null");
                }

                AttributeList attributeList = this.Declaration.AttributeListNode;

                if (attributeList != null)
                {
                    foreach (ITreeNode attribute in attributeList.Children())
                    {
                        var typeId    = attribute.NodeType.ToString();
                        var toString  = attribute.ToString();
                    }
                }

                base.Init();

                HierarchySnapshot hierarchySnapshot = new HierarchySnapshot(this);

                PenWebDeclarator penWebDeclarator = this.GetChildByType<PenWebDeclarator>();

                if (penWebDeclarator != null)
                {
                    this.VariableName = penWebDeclarator.ItemName;
                }
                else
                {
                    LogManager.Self.Log($"penWebDeclarator is null");
                }

                if (String.IsNullOrWhiteSpace(this.VariableName))
                {
                    PenWebDeclaratorQualifiedName declaratorQualifiedName = this.GetChildByType<PenWebDeclaratorQualifiedName>();

                    if (declaratorQualifiedName != null)
                    {
                        this.VariableName = declaratorQualifiedName.ItemName;
                    }
                    else
                    {
                        LogManager.Self.Log($"declarationSpecifiers is null");
                    }
                }

                PenWebDeclarationSpecifiers declarationSpecifiers = this.GetChildByType<PenWebDeclarationSpecifiers>();

                if (declarationSpecifiers != null)
                {
                    this.TypeName = declarationSpecifiers.ItemName;
                }
                else
                {
                    LogManager.Self.Log($"penWebDeclarator is null");
                }

                PenWebClassSpecifier penWebClassSpecifier = this.GetParentByType<PenWebClassSpecifier>();

                if (penWebClassSpecifier != null)
                {
                    this.OwningClass = penWebClassSpecifier.ClassName;
                }

                this.CppFunctionCatagory = CppFunctionCatagory.VariableDef;

                if (!String.IsNullOrWhiteSpace(this.TypeName) && !String.IsNullOrWhiteSpace(this.VariableName) && !String.IsNullOrWhiteSpace(this.OwningClass))
                {
                    this.SaveToJson = true;
                }
            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebDeclaration Exception", e);
            }

            this.Declaration = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} OwningClass: {this.OwningClass} TypeName: {this.TypeName} |{SingleLineText}|";
        }
    }


    public enum PenWebDeclarationType
    {
        Unset,
        ClassDef,
        EnumDef,
        VarDef,
        MethodDef,
    }

    public class PenWebSimpleDeclaration : CppParseTreeNodeBase
    {
        private JetBrains.ReSharper.Psi.Cpp.Tree.SimpleDeclaration SimpleDeclaration { get; set; }

        [JsonProperty] public string ClassName  { get; set; }
        [JsonProperty] public string ItemName { get; set; }

        [JsonProperty] public PenWebDeclarationType PenWebDeclarationType { get; set; } = PenWebDeclarationType.Unset;

        public PenWebSimpleDeclaration( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.SimpleDeclaration treeNode ) : base(parentNode, treeNode)
        {
            this.SimpleDeclaration = treeNode;
        }

        public override void Init()
        {
            try
            {
                Declaration declarationNode = this.SimpleDeclaration.DeclarationNode;

                if (declarationNode != null)
                {
                    CppDeclarationSymbol symbol = declarationNode.GetSymbol();

                    if (symbol != null)
                    {
                        ICppParserSymbol parentSymbol = symbol.Parent;

                        CppQualifiedName parentQualfiedName =  parentSymbol.GetQualifiedName();
                        string parentQualName = parentQualfiedName.GetNameStr();

                        string parentShortName = parentSymbol.GetShortName();

                        ICppParserSymbol classByMemberSymbol = parentSymbol.GetClassByMember();

                        string classByMemberShortName = classByMemberSymbol.GetShortName();

                        ICppParserSymbol enclosingClassSymbol = parentSymbol.GetEnclosingClass(true);
                        CppQualifiedName qualifiedInnerName = parentSymbol.GetQualifiedInnerName();

                        CppQualType cppQualType = symbol.GetDeclSpecType();


                        string debugStr = cppQualType.DbgDescription;

                        CppTypeVisitor cppTypeVisitor = new CppTypeVisitor();


                        if (cppQualType.RefFlag != ReferenceFlag.Empty)
                        {
                            cppQualType.Accept(cppTypeVisitor);

                            string typeStr = cppTypeVisitor.TypeStr;
                            string dbgStr = cppTypeVisitor.DbgStr;

                            this.ItemName = cppTypeVisitor.Name;

                            switch (cppTypeVisitor.CppType)
                            {
                                case CppTypeVisitorType.Class:
                                    this.PenWebDeclarationType = PenWebDeclarationType.ClassDef;
                                    break;

                                case CppTypeVisitorType.Enum:
                                    this.PenWebDeclarationType = PenWebDeclarationType.EnumDef;
                                    break;

                                default:
                                    break;
                            }
                        }

                        CppQualifiedName cppQualifiedName = symbol.GetQualifiedName();

                        ICppQualifiedNamePart namePart = cppQualifiedName.Name;

                        string partStr = namePart.ToString();

                        var subQualifier = cppQualifiedName.Qualifier;

                        string qualiferStr = subQualifier.GetNameStr();


                        if (!String.IsNullOrWhiteSpace(this.ItemName))
                        {
                            Console.WriteLine("");
                        }
                    }
                    else
                    {
                        LogManager.Self.Log($"PenWebSimpleDeclaration() declarationNode.symbol is null");
                    }

                    ICppResolvedTypeFactory cppResolvedTypeFactory = declarationNode.GetTypeFactory();

                    if (cppResolvedTypeFactory != null)
                    {
                        cppResolvedTypeFactory.GetIdentityTypeVisitor();
                    }

                }
                else
                {
                    LogManager.Self.Log($"PenWebSimpleDeclaration() declarationNode is null");
                }


                CompoundStatement compoundStatement = this.SimpleDeclaration.CompoundStatementNode;

                if (compoundStatement != null)
                {

                }

                CtorInitializer ctorInitializer = this.SimpleDeclaration.ConstructorInitializerNode;

                if (ctorInitializer != null)
                {

                }

                DefaultSpecifier defaultSpecifier  = this.SimpleDeclaration.DefaultSpecifierNode;

                if (defaultSpecifier != null)
                {

                }

                CtorBlock constructorBlock = this.SimpleDeclaration.ConstructorBlock;

                ITreeNode simicolonNode = this.SimpleDeclaration.SemicolonNode;

                ITreeNode trailingNode = this.SimpleDeclaration.TrailingNode;

                base.Init();

                //this.SaveToJson = true;

            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebSimpleDeclaration Exception", e);
            }

            this.SimpleDeclaration = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} TypeName: {this.ClassName} TypeName: {this.ItemName} |{SingleLineText}|";
        }
    }

    public class PenWebDeclaratorQualifiedName : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.DeclaratorQualifiedName DeclaratorQualifiedName { get; set; }

        [JsonProperty] public string ItemName { get; set; }

        public PenWebDeclaratorQualifiedName( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.DeclaratorQualifiedName treeNode ) : base(parentNode, treeNode)
        {
            this.DeclaratorQualifiedName = treeNode;
        }

        public override void Init()
        {
            try
            {
                this.ItemName = this.DeclaratorQualifiedName.GetText();



                base.Init();

                //this.SaveToJson = true;

            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebDeclaratorQualifiedName Exception", e);
            }

            this.DeclaratorQualifiedName = null;
        }

        public override string ToString()
        {
            return $"[{this.DeclaratorQualifiedName.ToString()}]  {this.GetType().Name} ItemName: {this.ItemName} |{SingleLineText}|";
        }
    }

    public class PenWebDeclarationSpecifiers : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationSpecifiers DeclarationSpecifiers { get; set; }
        [JsonProperty] public string OwningClass { get; set; }

        [JsonProperty] public string ItemName { get; set; }

        public PenWebDeclarationSpecifiers( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationSpecifiers treeNode ) : base(parentNode, treeNode)
        {
            this.DeclarationSpecifiers = treeNode;
        }

        public override void Init()
        {
            try
            {
                if (this.DeclarationSpecifiers.FirstChild != null)
                {
                    this.ItemName = this.DeclarationSpecifiers.FirstChild.GetText();
                }

                //IClassOrEnumSpecifier classOrEnumSpecifier = this.DeclarationSpecifiers.ClassSpecifierNode;
                //ClassQualifiedName classQualifiedName = classOrEnumSpecifier.GetClassQualifiedName();
                //ICppQualifiedNamePart cppQualifiedNamePart = classQualifiedName.GetNamePart();
                //cppQualifiedNamePart.ToString();

                base.Init();

                //this.SaveToJson = true;

            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebDeclarationSpecifiers Exception", e);
            }

            this.DeclarationSpecifiers = null;
        }

        public override string ToString()
        {
            return $"[{this.DeclarationSpecifiers.ToString()}]  {this.GetType().Name} ItemName: {this.ItemName} |{SingleLineText}|";
        }
    }
}
