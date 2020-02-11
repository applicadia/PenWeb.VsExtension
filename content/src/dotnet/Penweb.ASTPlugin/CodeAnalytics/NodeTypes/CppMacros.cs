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

namespace Penweb.CodeAnalytics
{

    public class PenWebMacroParameterList : CppParseTreeNodeBase
    {
        public PenWebMacroParameterList( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroParameterList treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebMacroParameter : CppParseTreeNodeBase
    {
        public PenWebMacroParameter( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroParameter treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebMacroArgumentList : CppParseTreeNodeBase
    {
        public PenWebMacroArgumentList( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroArgumentList treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebMacroBody : CppParseTreeNodeBase
    {
        public PenWebMacroBody( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroBody treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebMacroDefinition : CppParseTreeNodeBase
    {
        public PenWebMacroDefinition( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroDefinition treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebMacroUndefinition : CppParseTreeNodeBase
    {
        public PenWebMacroUndefinition( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroUndefinition treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebMacroArgument : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.MacroArgument MacroArgument { get; set; }

        [JsonProperty] public string ArguementName       { get; set; }

        public bool IsGood = false;

        public PenWebMacroArgument( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroArgument treeNode ) : base(parentNode, treeNode)
        {
            this.MacroArgument = treeNode;
        }

        public override void Init()
        {
            this.ArguementName = this.MacroArgument.GetText();

            if (this.ArguementName == "\"\"")
            {
                this.IsGood = false;
            }
            else
            {
                this.IsGood = true;
            }

            base.Init();

            this.MacroArgument = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} ArguementName: {this.ArguementName} |{SingleLineText}|";
        }
    }


    public class PenWebMacroReference : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.MacroReference MacroReference { get; set; }

        [JsonProperty] public string MacroName { get; set; }
        public PenWebMacroReference( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroReference treeNode ) : base(parentNode, treeNode)
        {
            this.MacroReference = treeNode;
        }

        public override void Init()
        {
            base.Init();

            this.MacroName = this.MacroReference.GetText();

            //CppTreeElementRange cppTreeElementRange = this.MacroReference.GetNameTokenRange();
            //text = cppTreeElementRange.ToString();
            //TreeTextRange treeTextRange = cppTreeElementRange.GetTreeTextRange();
            //text = treeTextRange.ToString();
            

            this.MacroReference = null;
        }
    }

    public enum PenWebMacroType
    {
        Unknown         = 0,
        ResourceId      = 1,
        ScreenIdDef     = 2,
        MessageMapEntry = 3,
        ListDef         = 4,
        EnumDef         = 5,
    }

    public class PenWebMacroCall : CppParseTreeNodeBase
    {
        JetBrains.ReSharper.Psi.Cpp.Tree.MacroCall MacroCall { get; set; } 

        [JsonProperty] public string       MacroName       { get; set; }
        [JsonProperty] public List<string> MacroParameters { get; set; } = new List<string>();

        [JsonProperty] public string       ParentName      { get; set; }

        [JsonProperty] public PenWebMacroType      PenWebMacroType   { get; set; }

        public PenWebMacroCall( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroCall treeNode ) : base(parentNode, treeNode)
        {
            this.MacroCall = treeNode;
        }
        public override void Init()
        {
            try
            {
                if (this.MacroCall.MacroReferenceNode != null)
                {
                    CppPPDefineSymbol symbol = this.MacroCall.MacroReferenceNode.GetReferencedSymbol();

                    if (symbol != null)
                    {
                        this.MacroName = symbol.Name;
                    }
                    else
                    {
                        this.MacroName = this.MacroCall.MacroReferenceNode.GetText();
                        //Console.WriteLine("PenWebMacroCall() Name is null");
                    }
                }
                else
                {
                    Console.WriteLine("PenWebMacroCall() MacroReferenceNode is null");
                }

                base.Init();

                if (!String.IsNullOrWhiteSpace(this.MacroName))
                {
                    this.PenWebMacroType = CppResourceManager.Self.GetMacroType(this.MacroName);
                }

                switch (this.PenWebMacroType)
                {
                    case PenWebMacroType.ResourceId:
                        break;

                    case PenWebMacroType.ScreenIdDef:
                        break;

                    case PenWebMacroType.ListDef:
                        break;

                    case PenWebMacroType.EnumDef:
                        break;

                    case PenWebMacroType.Unknown:
                        break;

                    case PenWebMacroType.MessageMapEntry:
                        this.WalkArguements();
                        break;


                }

                /*
                PenWebLiteralExpression penWebLiteralExpression = this.ParentNode.GetChildByType<PenWebLiteralExpression>();

                if (penWebLiteralExpression != null )
                {
                    PenWebEnumSpecifier penWebEnumeratorSpecifier = this.GetParentByType<PenWebEnumSpecifier>();

                    if (penWebEnumeratorSpecifier != null)
                    {
                        switch (penWebEnumeratorSpecifier.EnumName)
                        {
                            case "IDD":
                                this.PenWebMacroType = PenWebMacroType.ScreenIdDef;
                                this.ParentName = penWebEnumeratorSpecifier.EnumName;
                                break;

                            default:
                                this.PenWebMacroType = PenWebMacroType.EnumDef;
                                break;
                        }
                    }
                    else
                    {
                        PenWebMacroCall topMacroCall = GetTopParentByType<PenWebMacroCall>(null);

                        if (topMacroCall != null)
                        {
                            switch (topMacroCall.MacroName)
                            {
                                case "BEGIN_MESSAGE_MAP":
                                    this.PenWebMacroType = PenWebMacroType.MessageMapEntry;
                                    break;

                                default:
                                    this.PenWebMacroType = PenWebMacroType.ResourceId;
                                    break;
                            }
                        }
                        else
                        {
                            PenWebInitDeclarator penWebInitDeclarator = this.GetParentByType<PenWebInitDeclarator>();

                            if (penWebInitDeclarator != null)
                            {
                                this.PenWebMacroType = PenWebMacroType.ListDef;
                                this.ParentName = penWebInitDeclarator.TypeName;
                            }
                            else
                            {
                                this.PenWebMacroType = PenWebMacroType.Unknown;
                            }
                        }
                    }
                }
                */

                switch (this.PenWebMacroType)
                {
                    case PenWebMacroType.ListDef:
                        this.CppFunctionCatagory = CppFunctionCatagory.ListDef;
                        this.SaveToJson = true;
                        break;

                    case PenWebMacroType.ScreenIdDef:
                        this.CppFunctionCatagory = CppFunctionCatagory.ScreenDef;
                        this.SaveToJson = true;
                        break;

                    case PenWebMacroType.EnumDef:                              
                        this.CppFunctionCatagory = CppFunctionCatagory.EnumDef;
                        this.SaveToJson = true;
                        break;

                    case PenWebMacroType.MessageMapEntry:
                        this.CppFunctionCatagory = CppFunctionCatagory.MessageMap;
                        this.SaveToJson = true;
                        break;

                    case PenWebMacroType.ResourceId:
                        break;

                    case PenWebMacroType.Unknown:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            this.MacroCall = null;
        }

        private void WalkArguements()
        {
            HierarchySnapshot hierarchySnapshot = new HierarchySnapshot(this);

            List<PenWebMacroArgument> macroArguments = this.GetAllChildrenByTypeAsList<PenWebMacroArgument>();

            foreach (PenWebMacroArgument penWebMacroArgument in macroArguments)
            {
                if (penWebMacroArgument.IsGood)
                {
                    this.MacroParameters.Add(penWebMacroArgument.ArguementName);
                }
            }

            if ( this.MacroCall.ArgumentListNode != null)
            {
                foreach (ITreeNode argumentNode in this.MacroCall.ArgumentListNode.Arguments )
                {
                    string nodeTypeStr = argumentNode.NodeType.ToString();
                    string nodeType = argumentNode.GetType().Name;
                    string text = argumentNode.GetText();
                    string firstChild = argumentNode.FirstChild.GetText();
                        
                    //this.MacroParameters.Add(argumentNode.firstChild.GetText());
                }
            }
            else
            {
                this.MacroParameters.Add("null");
                //Console.WriteLine("PenWebMacroCall() ArgumentListNode is null");
            }
        }

        public override string ToString()
        {
            string argList = String.Join(", ", this.MacroParameters);
            return $"[{this.Location.ToString()}]  {this.GetType().Name} MacroName: {this.MacroName} Type: {this.PenWebMacroType.ToString("g")} Parameters: [{argList}] ParentName: {ParentName}";
        }
    }

}
