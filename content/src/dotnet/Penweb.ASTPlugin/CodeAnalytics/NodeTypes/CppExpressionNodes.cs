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
    public abstract class PenWebExpressionBase : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.CppExpressionNode CppExpressionNode { get; set; }

        public PenWebExpressionBase(JetBrains.ReSharper.Psi.Cpp.Tree.CppExpressionNode cppExpressionNode) : base(cppExpressionNode)
        {
            this.CppExpressionNode = cppExpressionNode;
        }
    }

    public class PenWebUnaryExpression : PenWebExpressionBase
    {
        public PenWebUnaryExpression( JetBrains.ReSharper.Psi.Cpp.Tree.UnaryExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebCastExpression : PenWebExpressionBase
    {
        public PenWebCastExpression( JetBrains.ReSharper.Psi.Cpp.Tree.CastExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebCliTypeIdExpression : PenWebExpressionBase
    {
        public PenWebCliTypeIdExpression( JetBrains.ReSharper.Psi.Cpp.Tree.CliTypeIdExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebConditionalExpression : PenWebExpressionBase
    {
        public PenWebConditionalExpression( JetBrains.ReSharper.Psi.Cpp.Tree.ConditionalExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebDeleteExpression : PenWebExpressionBase
    {
        public PenWebDeleteExpression( JetBrains.ReSharper.Psi.Cpp.Tree.DeleteExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebLiteralExpression : PenWebExpressionBase
    {
        public PenWebLiteralExpression( JetBrains.ReSharper.Psi.Cpp.Tree.LiteralExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebNewExpression : PenWebExpressionBase
    {
        public PenWebNewExpression( JetBrains.ReSharper.Psi.Cpp.Tree.NewExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebParenExpression : PenWebExpressionBase
    {
        public PenWebParenExpression( JetBrains.ReSharper.Psi.Cpp.Tree.ParenExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebPostfixExpression : PenWebExpressionBase
    {
        public PenWebPostfixExpression( JetBrains.ReSharper.Psi.Cpp.Tree.PostfixExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebSizeOfExpression : PenWebExpressionBase
    {
        public PenWebSizeOfExpression( JetBrains.ReSharper.Psi.Cpp.Tree.SizeOfExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebSubscriptExpression : PenWebExpressionBase
    {
        public PenWebSubscriptExpression( JetBrains.ReSharper.Psi.Cpp.Tree.SubscriptExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebThisExpression : PenWebExpressionBase
    {
        public PenWebThisExpression( JetBrains.ReSharper.Psi.Cpp.Tree.ThisExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebThrowExpression : PenWebExpressionBase
    {
        public PenWebThrowExpression( JetBrains.ReSharper.Psi.Cpp.Tree.ThrowExpression treeNode ) : base(treeNode)
        {
        }
    }

    public class PenWebCallExpression : PenWebExpressionBase
    {
        private JetBrains.ReSharper.Psi.Cpp.Tree.CallExpression CallExpression { get; set;  }

        [JsonProperty] public string CalledExpressionId { get; set; }
        public PenWebCallExpression( JetBrains.ReSharper.Psi.Cpp.Tree.CallExpression treeNode ) : base(treeNode)
        {
            this.CallExpression = treeNode;
        }

        public override void Init()
        {
            try
            {
                base.Init();

                this.SaveToJson = true;

                if (this.CallExpression.InvokedExpression != null)
                {
                    this.CalledExpressionId = this.CallExpression.InvokedExpression.GetText();
                }
                else
                {
                    Console.WriteLine("PenWebCallExpression() InvokedExpression is null");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            this.CallExpression = null;
            this.CppExpressionNode = null;
        }



        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} Called: {this.CalledExpressionId} Code: |{SingleLineText}|";
        }
    }

    public class PenWebBinaryExpression : PenWebExpressionBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.BinaryExpression BinaryExpression { get; set; }

        [JsonProperty] public string LeftArguement { get; set; }
        [JsonProperty] public string LeftOpperand { get; set; }

        public PenWebBinaryExpression( JetBrains.ReSharper.Psi.Cpp.Tree.BinaryExpression treeNode ) : base(treeNode)
        {
            this.BinaryExpression = treeNode;
        }

        public override void Init()
        {
            try
            {
                base.Init();

                this.SaveToJson = true;

                ICppExpression leftArguement = this.BinaryExpression.GetLeftArgument();

                if (leftArguement != null)
                {
                    this.LeftArguement = leftArguement.ToString();
                }
                else
                {
                    Console.WriteLine("PenWebBinaryExpression() InvokedExpression is null");
                }

                ICppExpressionNode cppExpressionNode = this.BinaryExpression.LeftOperand;

                if (leftArguement != null)
                {
                    this.LeftOpperand = cppExpressionNode.ToString();
                }
                else
                {
                    Console.WriteLine("PenWebBinaryExpression() InvokedExpression is null");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            this.BinaryExpression = null;
            this.CppExpressionNode = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} LeftArguement: {this.LeftArguement}  LeftOpperand: {this.LeftOpperand}  Code: |{SingleLineText}|";
        }
    }

    public class PenWebMemberAccessExpression : PenWebExpressionBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.MemberAccessExpression MemberAccessExpression { get; set; }

        [JsonProperty] public string LeftArguement { get; set; }
        [JsonProperty] public string ClassName     { get; set; }
        [JsonProperty] public string MethodName    { get; set; }

        public PenWebMemberAccessExpression( JetBrains.ReSharper.Psi.Cpp.Tree.MemberAccessExpression treeNode ) : base(treeNode)
        {
            this.MemberAccessExpression = treeNode;
        }

        public override void Init()
        {
            try
            {
                base.Init();

                this.SaveToJson = true;

                QualifiedReference qualifiedReference = this.MemberAccessExpression.Member;

                if (qualifiedReference != null)
                {
                    CppQualifiedName cppQualifiedName = qualifiedReference.GetQualifiedName();

                    if (cppQualifiedName.Name != null)
                    {
                        this.MethodName = cppQualifiedName.Name.ToString();
                    }

                    if (cppQualifiedName.Qualifier.Name != null)
                    {
                        this.ClassName = cppQualifiedName.Qualifier.Name.ToString();
                    }
                }

                ICppExpression leftArguement = this.MemberAccessExpression.GetLeftArgument();

                if (leftArguement != null)
                {
                    this.LeftArguement = leftArguement.ToString();
                }
                else
                {
                    Console.WriteLine("PenWebMemberAccessExpression() InvokedExpression is null");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            this.MemberAccessExpression = null;
            this.CppExpressionNode = null;
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} LeftArguement: {this.LeftArguement}  ClassName: {this.ClassName}  MethodName: {this.MethodName}  Code: |{SingleLineText}|";
        }
    }

    public class PenWebExpressionStatement : CppParseTreeNodeBase
    {
        public JetBrains.ReSharper.Psi.Cpp.Tree.ExpressionStatement ExpressionStatement { get; set; } 

        public PenWebExpressionStatement( JetBrains.ReSharper.Psi.Cpp.Tree.ExpressionStatement treeNode ) : base(treeNode)
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
                    Console.WriteLine("PenWebMacroCall() MacroReferenceNode is null");
                }
                */
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            this.ExpressionStatement = null;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}
