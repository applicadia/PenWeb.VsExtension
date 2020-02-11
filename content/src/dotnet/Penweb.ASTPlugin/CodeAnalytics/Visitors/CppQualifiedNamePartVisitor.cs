using JetBrains.ReSharper.Psi.Cpp.Symbols;
using JetBrains.ReSharper.Psi.Cpp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    public class CppQualifiedNamePartVisitorResult
    {
    }

    public class CppQualifiedNamePartVisitorData
    {
    }

    public class CppQualifiedNamePartVisitor : ICppQualifiedNamePartVisitor<CppQualifiedNamePartVisitorResult, CppQualifiedNamePartVisitorData>
    {
        public StringBuilder StringBuilder { get; }

        public CppQualifiedNamePartVisitor()
        {
            this.StringBuilder = new StringBuilder();
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppQualifiedId name)
        {
            this.StringBuilder.Append($"{name.Name} ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppDestructorTag name)
        {
            this.StringBuilder.Append($"CppDestructorTag: {name.Name} ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppOperatorId name)
        {
            this.StringBuilder.Append($"CppOperatorId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppConversionId name)
        {
            this.StringBuilder.Append($"CppConversionId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppUserDefinedLiteralId name)
        {
            this.StringBuilder.Append($"CppUserDefinedLiteralId: {name.Suffix} ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppAnonymousId name)
        {
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppDecltypeDestructorTag name)
        {
            this.StringBuilder.Append($"CppDecltypeDestructorTag: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppDecltypeId name)
        {
            this.StringBuilder.Append($"CppDecltypeId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppGlobalNamespaceId name)
        {
            this.StringBuilder.Append($"CppGlobalNamespaceId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppMSSuperId name)
        {
            this.StringBuilder.Append($"CppMSSuperId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppUnderlyingTypeId name)
        {
            this.StringBuilder.Append($"CppUnderlyingTypeId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppLambdaId name)
        {
            this.StringBuilder.Append($"CppLambdaId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppConversionsPackId name)
        {
            this.StringBuilder.Append($"CppConversionsPackId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppTemplateId name)
        {
            this.StringBuilder.Append($"CppTemplateId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppSubstitutionId name)
        {
            this.StringBuilder.Append($"CppSubstitutionId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppAnonymousLinkageId name)
        {
            this.StringBuilder.Append($"CppAnonymousLinkageId: {name.Name} ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppLinkageTemplateId name)
        {
            this.StringBuilder.Append($"CppLinkageTemplateId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppResolvedStructuredBindingId name)
        {
            this.StringBuilder.Append($"CppResolvedStructuredBindingId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppUnresolvedStructuredBindingId name)
        {
            this.StringBuilder.Append($"CppUnresolvedStructuredBindingId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppCliSimpleTypeId name)
        {
            CppQualType qualType = name.GetQualType();

            this.StringBuilder.Append($"CppCliSimpleTypeId: ? ");
            return null;
        }

        public CppQualifiedNamePartVisitorResult Visit(CppQualifiedNamePartVisitorData data, CppCliFinalizerTag name)
        {
            this.StringBuilder.Append($"CppCliFinalizerTag: {name.Name} ");
            return null;
        }
    }
}
