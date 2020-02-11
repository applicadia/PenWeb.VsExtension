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

namespace Penweb.CodeAnalytics
{
    public abstract class PenWebExpressionBase : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.CppExpressionNode CppExpressionNode { get; set; }

        public PenWebExpressionBase(CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CppExpressionNode cppExpressionNode) : base(parentNode, cppExpressionNode)
        {
            this.CppExpressionNode = cppExpressionNode;
        }
    }

    public class PenWebUnaryExpression : PenWebExpressionBase
    {
        public PenWebUnaryExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.UnaryExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCastExpression : PenWebExpressionBase
    {
        public PenWebCastExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CastExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCliTypeIdExpression : PenWebExpressionBase
    {
        public PenWebCliTypeIdExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CliTypeIdExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebConditionalExpression : PenWebExpressionBase
    {
        public PenWebConditionalExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ConditionalExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebDeleteExpression : PenWebExpressionBase
    {
        public PenWebDeleteExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.DeleteExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebLiteralExpression : PenWebExpressionBase
    {
        public PenWebLiteralExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.LiteralExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebNewExpression : PenWebExpressionBase
    {
        public PenWebNewExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.NewExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebParenExpression : PenWebExpressionBase
    {
        public PenWebParenExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ParenExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebPostfixExpression : PenWebExpressionBase
    {
        public PenWebPostfixExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.PostfixExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebSizeOfExpression : PenWebExpressionBase
    {
        public PenWebSizeOfExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.SizeOfExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebSubscriptExpression : PenWebExpressionBase
    {
        public PenWebSubscriptExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.SubscriptExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebThisExpression : PenWebExpressionBase
    {
        public PenWebThisExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ThisExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebThrowExpression : PenWebExpressionBase
    {
        public PenWebThrowExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ThrowExpression treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebBinaryExpression : PenWebExpressionBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.BinaryExpression BinaryExpression { get; set; }

        [JsonProperty] public string LeftArguement { get; set; }
        [JsonProperty] public string LeftOpperand { get; set; }

        public PenWebBinaryExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.BinaryExpression treeNode ) : base(parentNode, treeNode)
        {
            this.BinaryExpression = treeNode;
        }

        public override void Init()
        {
            try
            {
                ICppExpression leftArguement = this.BinaryExpression.GetLeftArgument();

                if (leftArguement != null)
                {
                    this.LeftArguement = leftArguement.ToString();
                }
                else
                {
                    LogManager.Self.Log("PenWebBinaryExpression() InvokedExpression is null");
                }

                ICppExpressionNode cppExpressionNode = this.BinaryExpression.LeftOperand;

                if (leftArguement != null)
                {
                    this.LeftOpperand = cppExpressionNode.ToString();
                }
                else
                {
                    LogManager.Self.Log("PenWebBinaryExpression() InvokedExpression is null");
                }

                base.Init();

                this.CppFunctionCatagory = CppFunctionCatagory.VariableRef;
                //this.SaveToJson = true;
            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebBinaryExpression Exception", e);
            }

            this.BinaryExpression = null;
            this.CppExpressionNode = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} LeftArguement: {this.LeftArguement}  LeftOpperand: {this.LeftOpperand}  Code: |{SingleLineText}|";
        }
    }

    public class PenWebCallExpression : PenWebExpressionBase
    {
        private JetBrains.ReSharper.Psi.Cpp.Tree.CallExpression CallExpression { get; set;  }

        [JsonProperty] public string Class     { get; set; }

        [JsonProperty] public List<string> VariableChain { get; set; } = new List<string>();

        [JsonProperty] public string Method    { get; set; }

        [JsonProperty] public List<string> MacroReferences { get; set; } = new List<string>();

        public PenWebCallExpression(CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CallExpression treeNode ) : base(parentNode, treeNode)
        {
            this.CallExpression = treeNode;
        }

        public override void Init()
        {
            try
            {
                if (this.CallExpression.InvokedExpression != null)
                {
                     string invokeText = this.CallExpression.InvokedExpression.GetText();

                     invokeText = invokeText.Replace("->", ".");

                     string[] invokeAr = invokeText.Split('.');

                     if (invokeAr.Length == 1 )
                     {
                        this.Method = invokeAr[0];
                     }
                     else
                     {
                         List<string> invokeList = new List<string>(invokeAr);

                         int cnt = 1;
                         foreach (string invokeName in invokeList)
                         {
                             if (cnt++ == invokeList.Count)
                             {
                                 this.Method = invokeName;
                             }
                             else
                             {
                                 this.VariableChain.Add(invokeName);
                             }
                         }
                     }
                }
                else
                {
                    LogManager.Self.Log("PenWebCallExpression() InvokedExpression is null");
                }

                base.Init();

                PenWebMemberAccessExpression penWebMemberAccessExpression = this.GetChildByType<PenWebMemberAccessExpression>();

                if (penWebMemberAccessExpression != null)
                {
                    this.Class = penWebMemberAccessExpression.Class;
                }
                else
                {
                    LogManager.Self.Log("PenWebCallExpression() penWebMemberAccessExpression is null");

                    HierarchySnapshot hierarchySnapshot = new HierarchySnapshot(this);
                }

                List<PenWebMacroReference> macroReferences = this.GetAllChildrenByTypeAsList<PenWebMacroReference>();

                foreach (PenWebMacroReference penWebMacroReference in macroReferences)
                {
                    this.MacroReferences.Add(penWebMacroReference.MacroName);
                }
               
                this.CppFunctionCatagory = CppFunctionCatagory.MethodCall;
                this.SaveToJson = true;
            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebCallExpression Exception", e);
            }

            this.CallExpression = null;
            this.CppExpressionNode = null;
        }

        public override string ToString()
        {
            string varList = String.Join(" ", this.VariableChain);
            return $"[{this.Location.ToString()}]  {this.GetType().Name} TypeName: {this.Class} VarChain: {varList}  Method: {this.Method} Code: |{SingleLineText}|";
        }
    }

    public class PenWebMemberAccessExpression : PenWebExpressionBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.MemberAccessExpression MemberAccessExpression { get; set; }

        //[JsonProperty] public string LeftArguement { get; set; }
        //[JsonProperty] public string TypeName     { get; set; }

        [JsonProperty] public string Class     { get; set; }

        [JsonProperty] public string MethodName    { get; set; }

        public PenWebMemberAccessExpression( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MemberAccessExpression treeNode ) : base(parentNode, treeNode)
        {
            this.MemberAccessExpression = treeNode;
        }

        public override void Init()
        {
            try
            {
                QualifiedReference qualifiedReference = this.MemberAccessExpression.Member;

                if (qualifiedReference != null)
                {
                    CppQualifiedName cppQualifiedName = qualifiedReference.GetQualifiedName();

                    this.MethodName = cppQualifiedName.GetNameStr();
                }

                ICppExpressionNode cppExpressionNode = this.MemberAccessExpression.Qualifier;
                CppTypeAndCategory cppTypeAndCatagory = cppExpressionNode.GetTypeAndCategory();
                CppQualType cppQualType = cppTypeAndCatagory.Type;

                CppTypeVisitor cppTypeVisitor = new CppTypeVisitor();

                cppQualType.Accept(cppTypeVisitor);

                string typeStr = cppTypeVisitor.TypeStr;
                string dbgStr = cppTypeVisitor.DbgStr;

                this.Class = cppTypeVisitor.Name;

                if (String.IsNullOrWhiteSpace(this.Class))
                {
                    this.Class = dbgStr;

                    LogManager.Self.Log($"PenWebMemberAccessExpression() class name empty");

                    cppQualType.Accept(cppTypeVisitor);

                    typeStr = cppTypeVisitor.TypeStr;
                    dbgStr = cppTypeVisitor.DbgStr;

                    this.Class = cppTypeVisitor.Name;
                }

                /*
                ICppExpression leftArguement = this.MemberAccessExpression.GetLeftArgument();
                CppExpressionVisitor cppExpressionVisitor = new CppExpressionVisitor();
                leftArguement.Accept<CppExpressonVisitorResult>(cppExpressionVisitor);
                */

                base.Init();

                this.CppFunctionCatagory = CppFunctionCatagory.MethodCall;

                //this.SaveToJson = true;
            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebMemberAccessExpression Exception", e);
            }

            this.MemberAccessExpression = null;
            this.CppExpressionNode = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} TypeName: {this.Class} ItemName: {this.MethodName}  Code: |{SingleLineText}|";
        }
    }

    public class PenWebExpressionStatement : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.ExpressionStatement ExpressionStatement { get; set; } 

        public PenWebExpressionStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ExpressionStatement treeNode ) : base(parentNode, treeNode)
        {
            this.ExpressionStatement = treeNode;
        }

        public override void Init()
        {
            try
            {
                base.Init();

                /*
                this.SaveToJson = true;

                if (this.ExpressionStatement.Expression != null)
                {
                    ICppExpressionNode cppExpressionNode = this.ExpressionStatement.Expression.;
                }
                else
                {
                    LogManager.Self.Log("PenWebMacroCall() MacroReferenceNode is null");
                }
                */
            }
            catch (Exception e)
            {
                LogManager.Self.Log("PenWebExpressionStatement Exception", e);
            }

            this.ExpressionStatement = null;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}
