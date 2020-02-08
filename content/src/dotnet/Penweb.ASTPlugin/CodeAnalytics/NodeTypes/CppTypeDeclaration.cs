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
    public class PenWebDeclarationSpecifiers : CppParseTreeNodeBase
    {
        public PenWebDeclarationSpecifiers( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationSpecifiers treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebDeclarationSpecifierTypename : CppParseTreeNodeBase
    {
        public PenWebDeclarationSpecifierTypename( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationSpecifierTypename treeNode ) : base(parentNode, treeNode)
        {
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
                base.Init();

                this.SaveToJson = true;

                this.EnumName = this.EnumSpecifier.DeclaredName;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                base.Init();

                this.SaveToJson = true;

                this.ClassName = this.ClassSpecifier.DeclaredName;

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
                            Console.WriteLine("PenWebClassSpecifier() baseReference is null");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("PenWebClassSpecifier() baseClause is null");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            this.ClassSpecifier = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} EnumName: {this.ClassName}  BaseClass: {this.BaseClass} |{SingleLineText}|";
        }
    }
}
