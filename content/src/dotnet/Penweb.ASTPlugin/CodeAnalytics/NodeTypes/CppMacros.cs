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
    public class PenWebMacroCall : CppParseTreeNodeBase
    {
        JetBrains.ReSharper.Psi.Cpp.Tree.MacroCall MacroCall { get; set; } 

        [JsonProperty] public string       MacroName       { get; set; }
        [JsonProperty] public List<string> MacroParameters { get; set; } = new List<string>();

        public PenWebMacroCall( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroCall treeNode ) : base(parentNode, treeNode)
        {
            this.MacroCall = treeNode;
        }
        public override void Init()
        {
            try
            {
                base.Init();

                this.SaveToJson = true;

                if (this.MacroCall.MacroReferenceNode != null)
                {
                    this.MacroName = this.MacroCall.MacroReferenceNode.GetText();
                }
                else
                {
                    Console.WriteLine("PenWebMacroCall() MacroReferenceNode is null");
                }

                if ( this.MacroCall.ArgumentListNode != null)
                {
                    foreach (var argumentNode in this.MacroCall.ArgumentListNode.Children())
                    {
                        this.MacroParameters.Add(argumentNode.GetText());
                    }
                }
                else
                {
                    Console.WriteLine("PenWebMacroCall() ArgumentListNode is null");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            this.MacroCall = null;
        }

        public override string ToString()
        {
            string argList = String.Join(", ", this.MacroParameters);
            return $"[{this.Location.ToString()}]  {this.GetType().Name} MacroName: {this.MacroName} Parameters: [{argList}]";
        }
    }

    public class PenWebMacroReference : CppParseTreeNodeBase
    {
        public PenWebMacroReference( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroReference treeNode ) : base(parentNode, treeNode)
        {
        }
    }

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

    public class PenWebMacroArgument : CppParseTreeNodeBase
    {
        public PenWebMacroArgument( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MacroArgument treeNode ) : base(parentNode, treeNode)
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
}
