using JetBrains.ReSharper.Psi.Cpp.Resolve;
using JetBrains.ReSharper.Psi.Cpp.Symbols;
using JetBrains.ReSharper.Psi.Cpp.Tree.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    public class CppClassifyVisitor : ICppResolveEntityVisitor<CppQualRefEntities>
    {
        protected CppClassifyVisitor() : base( )
        {
        }

        public CppQualRefEntities Visit(ICppClassResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppStatementResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppParameterListResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppDeclaratorResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppConceptDefinitionResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppEnumeratorResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppNamespaceResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppNamespaceAliasResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppUsingDeclarationResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppClassTemplateResolveEntityPack ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppTypeTemplateParameter ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppTypeTemplateParameterPack ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppExpressionTemplateParameter ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppExpressionTemplateParameterPack ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppTemplateTemplateParameter ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppTemplateTemplateParameterPack ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppGenericParameter ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppDependentResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppDependentAutoTypeResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppDependentDecltypeResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppDependentFunctionReturnAutoTypeResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppDependentDeducedClassTypePlaceholder A_0)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppClassTemplateResolveEntityPackWithSubstitution ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppUsingDeclarationResolveEntityWithSubstitution ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppUndeterminedSpecializationResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppTemplateTemplateParameterWithAppliedArgumentsResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppDecltypeResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppDeclaratorResolveEntityPack ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppTypeTemplateDeclaratorWithAppliedArgumentsResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppUndeterminedTemplateVariableSpecializationResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppSuperResolveEntity A_0)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppUnderlyingTypeResolveEntity A_0)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppBuiltinOperatorResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppFunctionTemplateDeclaratorResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppTypeTemplateDeclaratorResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppVariableTemplateDeclaratorResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppDependencyKillerResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppCxxCliPropertySetResolveEntity ent)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppSynthesizedDeductionGuide A_0)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppUserProvidedDeductionGuide A_0)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(ICppRequiresExpressionScopeResolveEntity A_0)
        {
            throw new NotImplementedException();
        }

        public CppQualRefEntities Visit(CppRequiresExpressionItemResolveEntity A_0)
        {
            throw new NotImplementedException();
        }
    }
}
