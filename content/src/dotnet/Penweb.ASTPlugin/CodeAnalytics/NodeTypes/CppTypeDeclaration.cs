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
using JetBrains.ReSharper.Psi.Cpp.Types;
using JetBrains.ReSharper.Psi;

namespace Penweb.CodeAnalytics
{
    public class PenWebDeclarationSpecifierTypename : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationSpecifierTypename DeclarationSpecifierTypename { get; set; }

        [JsonProperty] public string TypeName { get; set; }

        [JsonProperty] public string ClassTag { get; set; }

        public PenWebDeclarationSpecifierTypename( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationSpecifierTypename treeNode ) : base(parentNode, treeNode)
        {
            this.DeclarationSpecifierTypename = treeNode;
        }

        public override void Init()
        {
            try
            {
                CppQualifiedName cppQualifiedName = this.DeclarationSpecifierTypename.GetQualifiedName();

                this.TypeName = cppQualifiedName.GetNameStr();

                CppClassTag cppClassTag = this.DeclarationSpecifierTypename.GetClassTag();

                this.ClassTag = cppClassTag.ToString("g");

                DeclarationSpecifiersBase declarationSpecifiers = this.DeclarationSpecifierTypename.GetDeclarationSpecifiers();

                switch (declarationSpecifiers)
                {
                    case BaseTypeGenericConstraintItem genericConstraintItem:
                        break;

                    case DeclarationSpecifiers specifiers:
                        break;

                    case EnumBase enumBase:
                        break;

                    default:
                        break;
                }

                string identifier =  this.DeclarationSpecifierTypename.Identifier.GetText();

                TypeId typeId = this.DeclarationSpecifierTypename.CliSimpleTypeId;

                if (typeId != null)
                {
                    string typeIdStr = typeId.GetText();

                    CppQualType cppQualType = typeId.GetQualType();

                    CppTypeVisitor cppTypeVisitor = new CppTypeVisitor();

                    cppQualType.Accept(cppTypeVisitor);

                    string typeStr = cppTypeVisitor.TypeStr;
                    string nameStr = cppTypeVisitor.Name;
                    string dbgStr = cppTypeVisitor.DbgStr;
                }

                base.Init();

                //this.SaveToJson = true;
            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebDeclarationSpecifierTypename Exception", e);
            }

            this.DeclarationSpecifierTypename = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} TypeName: {this.TypeName} ClassTag: {this.ClassTag} |{SingleLineText}|";
        }
    }

    public class PenWebEnumSpecifier : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.EnumSpecifier EnumSpecifier { get; set; }

        [JsonProperty] public string EnumName { get; set; }

        public PenWebEnumSpecifier( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.EnumSpecifier treeNode ) : base(parentNode, treeNode)
        {
            this.EnumSpecifier = treeNode;
        }

        public override void Init()
        {
            try
            {
                this.EnumName = this.EnumSpecifier.DeclaredName;

                this.AstState    = AstState.InEnum;
                this.CurrentType = this.EnumName;

                if (String.IsNullOrEmpty(this.EnumName))
                {
                    if (this.EnumSpecifier.GetText().Contains("IDD"))
                    {
                        this.EnumName = "IDD";
                    }
                }

                base.Init();

                //this.SaveToJson = true;
            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebEnumSpecifier Exception", e);
            }

            this.EnumSpecifier = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} EnumName: {this.EnumName}  |{SingleLineText}|";
        }
    }

    public class PenWebClassSpecifier : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.ClassSpecifier ClassSpecifier { get; set;  }

        [JsonProperty] public string ClassName { get; set; }
        [JsonProperty] public string BaseClass { get; set; }

        public PenWebClassSpecifier( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ClassSpecifier treeNode ) : base(parentNode, treeNode)
        {
            this.ClassSpecifier = treeNode;
        }

        public override void Init()
        {
            try
            {
                this.ClassName = this.ClassSpecifier.DeclaredName;

                this.AstState    = AstState.InClass;
                this.CurrentType = this.ClassName;

                BaseClause baseClause = this.ClassSpecifier.GetBaseClause();

                if (baseClause != null)
                {
                    foreach (BaseSpecifier baseNode in baseClause.BaseSpecifierNodes)
                    {
                        QualifiedBaseTypeReference baseReference = baseNode.QualifiedBaseTypeReferenceNode;

                        if (baseReference != null)
                        {
                            CppQualifiedName qualifiedName = baseReference.GetQualifiedName();

                            this.BaseClass = qualifiedName.GetNameStr();
                        }
                        else
                        {
                            LogManager.Self.Log("PenWebClassSpecifier() baseReference is null");
                        }
                    }
                }
                else
                {
                    LogManager.Self.Log("PenWebClassSpecifier() baseClause is null");
                }

                base.Init();

                this.CppFunctionCatagory = CppFunctionCatagory.ClassDef;

                if (!String.IsNullOrWhiteSpace(this.ClassName) && !String.IsNullOrWhiteSpace(this.BaseClass))
                {
                    this.SaveToJson = true;
                }

                IDeclaredElement declarationElement = this.ClassSpecifier.DeclaredElement;

                foreach (IDeclaration declaration in declarationElement.GetDeclarations())
                {

                }
            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebClassSpecifier Exception", e);
            }

            this.ClassSpecifier = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} TypeName: {this.ClassName}  BaseClass: {this.BaseClass} |{SingleLineText}|";
        }
    }
}
