using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.ReSharper.Psi.Cpp.Expressions;

namespace Penweb.CodeAnalytics
{
    public class CppExpressonVisitorResult
    {

    }

    public class CppExpressionVisitor : ICppExpressionVisitor<CppExpressonVisitorResult>
    {
        public CppExpressionVisitor()
        {

        }

        public CppExpressonVisitorResult Visit(ICppEmptyExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppValueExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppUserDefinedLiteralExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedUserDefinedLiteralExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppThisExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppBinaryExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedBinaryExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppUnaryExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedUnaryExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppPostfixExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedPostfixExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppConditionalExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedConditionalExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppQualifiedReferenceExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppLinkageQualifiedReferenceExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppMemberAccessExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppNewExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedNewExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppDeleteExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppSubscriptExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedSubscriptExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppCallExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppCastExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedCastExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppSizeOfExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedSizeOfExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppSizeOfEllipsisExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedSizeOfEllipsisExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppAlignOfExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedAlignOfExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedTypeIdExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppNoExceptExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppMSSingleArgumentTraitsExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedMSSingleArgumentTraitsExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppMSDoubleArgumentTraitsExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedMSDoubleArgumentTraitsExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppMSMultiArgumentTraitsExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedMSMultiArgumentTraitsExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppMSUuidOfExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedMSUuidOfExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppMSEventHookExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedMSEventHookExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppMSNoopExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppMSAssumeExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppMSBuiltinAddressofExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppThrowExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppBracedInitListExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppBraceInitializedTemporaryExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedBraceInitializedTemporaryExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppLambdaExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppReplacedLambdaExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppRequiresExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppReplacedRequiresExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppLinkageRequiresExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppCliMultiArgumentSubscriptExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedCliMultiArgumentSubscriptExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppCliTypeIdExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedCliTypeIdExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppPackExpansionExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppFoldExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedFoldExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppCoAwaitExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedCoAwaitExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppCoYieldExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedCoYieldExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedThisExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppParenExpression expr)
        {
            return null;
        }
        public CppExpressonVisitorResult Visit(ICppTypeIdExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedQualifiedReferenceExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedCallExpression expr)
        {
            return null;
        }

        public CppExpressonVisitorResult Visit(ICppResolvedMemberAccessExpression expr)
        {
            return null;
        }

    }
}
