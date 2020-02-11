using JetBrains.ReSharper.Psi.Cpp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    public class CppTypeVisitorResult
    {

    }

    public enum CppTypeVisitorType
    {
        Enum,
        Class,
        Unknown,
    }

    public class CppTypeVisitor : ICppTypeVisitor<CppTypeVisitorResult>
    {
        private StringBuilder NameBuilder { get; set; }

        private StringBuilder DbgBuilder { get; set; }

        private StringBuilder TypeBuilder { get; set; }

        private StringBuilder QualifierBuilder { get; set; }

        public string Name => this.NameBuilder.ToString().Trim();
        public string TypeStr => this.TypeBuilder.ToString().Trim();

        public string DbgStr => this.DbgBuilder.ToString().Trim();

        public string Quaifier => this.QualifierBuilder.ToString().Trim();

        public CppTypeVisitorType CppType => this.getCppTypeVisitorType();

        public CppTypeVisitor()
        {
            this.NameBuilder      = new StringBuilder();
            this.DbgBuilder       = new StringBuilder();
            this.TypeBuilder      = new StringBuilder();
            this.QualifierBuilder = new StringBuilder();
        }

        private CppTypeVisitorType getCppTypeVisitorType()
        {
            switch (this.TypeStr)
            {
                case "Enum":      return CppTypeVisitorType.Enum;
                case "TypeName": return CppTypeVisitorType.Class;

                default:
                    return CppTypeVisitorType.Unknown;
            }
        }

        public CppTypeVisitorResult Visit(CppAutoType t, Qualifiers q)
        {
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppDecltypeAutoType t, Qualifiers q)
        {
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppArrayType t, Qualifiers q)
        {
            // has inner type
            this.TypeBuilder.Append($"Array[]");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppFunctionType t, Qualifiers q)
        {
            // has inner type
            this.TypeBuilder.Append($"Function()");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppUnresolvedFunctionType t, Qualifiers q)
        {
            // has inner type
            this.TypeBuilder.Append($"Unresolved-Function()");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppReplacedFunctionType t, Qualifiers q)
        {
            // has inner type
            this.TypeBuilder.Append($"Replaced-Function()");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppTemplateParameterType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"TemplateParameter");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppGenericParameterType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"GenericParameter");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppPackExpansionType t, Qualifiers q)
        {
            // has inner type
            this.TypeBuilder.Append($"PackExpansion");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppDependentType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"DependentType");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppTypeReference t, Qualifiers q)
        {
            this.TypeBuilder.Append($"TypeReference");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppElaboratedTypeReference t, Qualifiers q)
        {
            this.TypeBuilder.Append($"ElaboratedTypeReference");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppVoidType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"Void");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppClassType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"TypeName");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");

            this.NameBuilder.Append(t.Symbol.Name + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppPendingClassType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"PendingClass");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppUnknownType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"UnknownType");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppNullptrType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"Nullptr");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppEnumType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"Enum");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");

            this.NameBuilder.Append(t.Symbol.Name + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppResolvedEnumType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"ResolvedEnum");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");

            this.NameBuilder.Append(t.ResolveEntity.Name + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppDecoratedType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"DecoratedType");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppMemberPointerType t, Qualifiers q)
        {
            // has inner type
            this.TypeBuilder.Append($"DecoratedType");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppDeclarationSpecifierType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"DeclarationSpecifier");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppCliInteriorPointerType t, Qualifiers q)
        {
            // has inner type
            this.TypeBuilder.Append($"CliInteriorPointer");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppCliPinPointerType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"CliPinPointer");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppCliArrayType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"CliPinPointer");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppDeducedClassTypePlaceholder A_0, Qualifiers A_1)
        {
            this.TypeBuilder.Append($"DeducedClassTypePlaceholder");
            this.DbgBuilder.Append(A_0.DbgDescription + " ");
            this.QualifierBuilder.Append(A_1.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppLinkageEntityType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"LinkageEntityType");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppDependentLinkageEntityType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"DependentLinkageEntity");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppLinkageTemplateParameterType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"LinkageTemplateParameter");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppLinkageArrayType t, Qualifiers q)
        {
            // has inner type
            this.TypeBuilder.Append($"LinkageArray");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppLinkageCliArrayType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"LinkageArray");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult VisitReference(CppQualType innerType, ReferenceFlag flag)
        {
            this.TypeBuilder.Append($"CppQualType");
            this.DbgBuilder.Append(innerType.DbgDescription + " ");
            this.QualifierBuilder.Append(flag.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult VisitTypeWithDeclSpecPlacement(CppQualType innerType, CppDeclSpecPlacement placement)
        {
            this.TypeBuilder.Append($"CppQualType-Placement");
            this.DbgBuilder.Append(innerType.DbgDescription + " ");
            //this.QualifierBuilder.Append(placement.ToString("g") + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppPointerType t, Qualifiers q)
        {
            // has inner type
            this.TypeBuilder.Append($"Pointer");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");

            t.InnerType.Accept(this);
            return null;
        }

        public CppTypeVisitorResult Visit(CppCliHandleType t, Qualifiers q)
        {
            // has inner type
            this.TypeBuilder.Append($"CliHandleType");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");

            t.InnerType.Accept(this);

            return null;
        }

        public CppTypeVisitorResult Visit(CppResolvedClassType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"ResolvedClassType");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");

            this.NameBuilder.Append(t.ResolveEntity.Name + " ");
            return null;
        }

        public CppTypeVisitorResult Visit(CppNumericType t, Qualifiers q)
        {
            this.TypeBuilder.Append($"Numeric");
            this.DbgBuilder.Append(t.DbgDescription + " ");
            this.QualifierBuilder.Append(q.ToString("g") + " ");
            return null;
        }
    }
}
