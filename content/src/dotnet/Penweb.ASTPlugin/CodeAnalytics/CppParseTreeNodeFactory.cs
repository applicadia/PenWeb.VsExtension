using JetBrains.ReSharper.Psi.Cpp.Language;
using JetBrains.ReSharper.Psi.Cpp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    public class CppParseTreeNodeFactory
    {
        public static CppParseTreeNodeFactory Self;

        public SortedDictionary<string,string> NodeSchemaMap { get; } = new SortedDictionary<string, string>();
        public SortedDictionary<string,string> NodeTypeMap   { get; } = new SortedDictionary<string, string>();

        private CppParseTreeNodeFactory()
        {
            Self = this;
        }

        public CppParseTreeNodeBase CreateTypedNode(CppParseTreeNodeBase parentNode, ITreeNode treeNode)
        {
            string typeName = treeNode.GetType().ToString();

            if ( !NodeTypeMap.ContainsKey(typeName))
            {
                NodeTypeMap.Add(typeName, typeName);
            }

            switch ( treeNode.NodeType.ToString() )
            {
                case "NEW_LINE":             return null;
                case "EOL_COMMENT":          return null;
                case "PRAGMA_DIRECTIVE":     return null;
                case "WHITE_SPACE":          return null;
            }

            switch (treeNode)
            {
                case JetBrains.ReSharper.Psi.Cpp.Parsing.CppCommentTokenNode penWebCppCommentTokenNode: 
                    return new PenWebCppCommentTokenNode( parentNode, penWebCppCommentTokenNode );

                case JetBrains.ReSharper.Psi.Cpp.Parsing.CppFromSubstitutionTokenNode penWebCppFromSubstitutionTokenNode: 
                    return new PenWebCppFromSubstitutionTokenNode( parentNode, penWebCppFromSubstitutionTokenNode );

                case JetBrains.ReSharper.Psi.Cpp.Parsing.CppGenericTokenNode penWebCppGenericTokenNode: 
                    return new PenWebCppGenericTokenNode( parentNode, penWebCppGenericTokenNode );

                case JetBrains.ReSharper.Psi.Cpp.Parsing.CppIdentifierTokenNode penWebCppIdentifierTokenNode: 
                    return new PenWebCppIdentifierTokenNode( parentNode, penWebCppIdentifierTokenNode );

                case JetBrains.ReSharper.Psi.Cpp.Tree.AbstractDeclarator penWebAbstractDeclarator: 
                    return new PenWebAbstractDeclarator( parentNode, penWebAbstractDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.AbstractDeclaratorName penWebAbstractDeclaratorName: 
                    return new PenWebAbstractDeclaratorName( parentNode, penWebAbstractDeclaratorName );

                case JetBrains.ReSharper.Psi.Cpp.Tree.AccessSpecifier penWebAccessSpecifier: 
                    return new PenWebAccessSpecifier( parentNode, penWebAccessSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ArraySizeSpecifier penWebArraySizeSpecifier: 
                    return new PenWebArraySizeSpecifier( parentNode, penWebArraySizeSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.BaseClause penWebBaseClause: 
                    return new PenWebBaseClause( parentNode, penWebBaseClause );

                case JetBrains.ReSharper.Psi.Cpp.Tree.BaseSpecifier penWebBaseSpecifier: 
                    return new PenWebBaseSpecifier( parentNode, penWebBaseSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.BinaryExpression penWebBinaryExpression: 
                    return new PenWebBinaryExpression( parentNode, penWebBinaryExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.BracedInitList penWebBracedInitList: 
                    return new PenWebBracedInitList( parentNode, penWebBracedInitList );

                case JetBrains.ReSharper.Psi.Cpp.Tree.BreakStatement penWebBreakStatement: 
                    return new PenWebBreakStatement( parentNode, penWebBreakStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CallExpression penWebCallExpression: 
                    return new PenWebCallExpression( parentNode, penWebCallExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CaseStatement penWebCaseStatement: 
                    return new PenWebCaseStatement( parentNode, penWebCaseStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CastExpression penWebCastExpression: 
                    return new PenWebCastExpression( parentNode, penWebCastExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CatchSection penWebCatchSection: 
                    return new PenWebCatchSection( parentNode, penWebCatchSection );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ClassQualifiedName penWebClassQualifiedName: 
                    return new PenWebClassQualifiedName( parentNode, penWebClassQualifiedName );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ClassSpecifier penWebClassSpecifier: 
                    return new PenWebClassSpecifier( parentNode, penWebClassSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CliTypeIdExpression penWebCliTypeIdExpression: 
                    return new PenWebCliTypeIdExpression( parentNode, penWebCliTypeIdExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CompoundStatement penWebCompoundStatement: 
                    return new PenWebCompoundStatement( parentNode, penWebCompoundStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ConditionalExpression penWebConditionalExpression: 
                    return new PenWebConditionalExpression( parentNode, penWebConditionalExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ContinueStatement penWebContinueStatement: 
                    return new PenWebContinueStatement( parentNode, penWebContinueStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CppChameleonCtorBlock penWebCppChameleonCtorBlock: 
                    return new PenWebCppChameleonCtorBlock( parentNode, penWebCppChameleonCtorBlock );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CtorInitializer penWebCtorInitializer: 
                    return new PenWebCtorInitializer( parentNode, penWebCtorInitializer );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CxxCliPropertyDeclaration penWebCxxCliPropertyDeclaration: 
                    return new PenWebCxxCliPropertyDeclaration( parentNode, penWebCxxCliPropertyDeclaration );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CxxCliPropertyOrEventDeclarator penWebCxxCliPropertyOrEventDeclarator: 
                    return new PenWebCxxCliPropertyOrEventDeclarator( parentNode, penWebCxxCliPropertyOrEventDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.Declaration penWebDeclaration: 
                    return new PenWebDeclaration( parentNode, penWebDeclaration );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationSpecifiers penWebDeclarationSpecifiers: 
                    return new PenWebDeclarationSpecifiers( parentNode, penWebDeclarationSpecifiers );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationSpecifierTypename penWebDeclarationSpecifierTypename: 
                    return new PenWebDeclarationSpecifierTypename( parentNode, penWebDeclarationSpecifierTypename );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationStatement penWebDeclarationStatement: 
                    return new PenWebDeclarationStatement( parentNode, penWebDeclarationStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.Declarator penWebDeclarator: 
                    return new PenWebDeclarator( parentNode, penWebDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DeclaratorQualifiedName penWebDeclaratorQualifiedName: 
                    return new PenWebDeclaratorQualifiedName( parentNode, penWebDeclaratorQualifiedName );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DefaultStatement penWebDefaultStatement: 
                    return new PenWebDefaultStatement( parentNode, penWebDefaultStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DeleteExpression penWebDeleteExpression: 
                    return new PenWebDeleteExpression( parentNode, penWebDeleteExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.Directive penWebDirective: 
                    return new PenWebDirective( parentNode, penWebDirective );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DoStatement penWebDoStatement: 
                    return new PenWebDoStatement( parentNode, penWebDoStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DoStatementBody penWebDoStatementBody: 
                    return new PenWebDoStatementBody( parentNode, penWebDoStatementBody );

                case JetBrains.ReSharper.Psi.Cpp.Tree.EmptyDeclaration penWebEmptyDeclaration: 
                    return new PenWebEmptyDeclaration( parentNode, penWebEmptyDeclaration );

                case JetBrains.ReSharper.Psi.Cpp.Tree.EmptyStatement penWebEmptyStatement: 
                    return new PenWebEmptyStatement( parentNode, penWebEmptyStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.Enumerator penWebEnumerator: 
                    return new PenWebEnumerator( parentNode, penWebEnumerator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.EnumSpecifier penWebEnumSpecifier: 
                    return new PenWebEnumSpecifier( parentNode, penWebEnumSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ExpressionStatement penWebExpressionStatement: 
                    return new PenWebExpressionStatement( parentNode, penWebExpressionStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ForStatement penWebForStatement: 
                    return new PenWebForStatement( parentNode, penWebForStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.FunctionArgumentList penWebFunctionArgumentList: 
                    return new PenWebFunctionArgumentList( parentNode, penWebFunctionArgumentList );

                case JetBrains.ReSharper.Psi.Cpp.Tree.FwdClassSpecifier penWebFwdClassSpecifier: 
                    return new PenWebFwdClassSpecifier( parentNode, penWebFwdClassSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.IfStatement penWebIfStatement: 
                    return new PenWebIfStatement( parentNode, penWebIfStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ImportDirective penWebImportDirective: 
                    return new PenWebImportDirective( parentNode, penWebImportDirective );

                case JetBrains.ReSharper.Psi.Cpp.Tree.InitDeclarator penWebInitDeclarator: 
                    return new PenWebInitDeclarator( parentNode, penWebInitDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.LinkageSpecification penWebLinkageSpecification: 
                    return new PenWebLinkageSpecification( parentNode, penWebLinkageSpecification );

                case JetBrains.ReSharper.Psi.Cpp.Tree.LiteralExpression penWebLiteralExpression: 
                    return new PenWebLiteralExpression( parentNode, penWebLiteralExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroArgument penWebMacroArgument: 
                    return new PenWebMacroArgument( parentNode, penWebMacroArgument );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroArgumentList penWebMacroArgumentList: 
                    return new PenWebMacroArgumentList( parentNode, penWebMacroArgumentList );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroBody penWebMacroBody: 
                    return new PenWebMacroBody( parentNode, penWebMacroBody );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroCall penWebMacroCall:
                    if (penWebMacroCall.IsTopLevel())
                    {
                        return new PenWebMacroCall(parentNode, penWebMacroCall);
                    }
                    else
                    {
                        return null;
                    }

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroDefinition penWebMacroDefinition: 
                    return new PenWebMacroDefinition( parentNode, penWebMacroDefinition );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroParameter penWebMacroParameter: 
                    return new PenWebMacroParameter( parentNode, penWebMacroParameter );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroParameterList penWebMacroParameterList: 
                    return new PenWebMacroParameterList( parentNode, penWebMacroParameterList );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroReference penWebMacroReference: 
                    return new PenWebMacroReference( parentNode, penWebMacroReference );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroUndefinition penWebMacroUndefinition: 
                    return new PenWebMacroUndefinition( parentNode, penWebMacroUndefinition );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MemberAccessExpression penWebMemberAccessExpression: 
                    return new PenWebMemberAccessExpression( parentNode, penWebMemberAccessExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MemInitializer penWebMemInitializer: 
                    return new PenWebMemInitializer( parentNode, penWebMemInitializer );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MemInitializerName penWebMemInitializerName: 
                    return new PenWebMemInitializerName( parentNode, penWebMemInitializerName );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MSAttributes penWebMSAttributes: 
                    return new PenWebMSAttributes( parentNode, penWebMSAttributes );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MSDeclSpec penWebMSDeclSpec: 
                    return new PenWebMSDeclSpec( parentNode, penWebMSDeclSpec );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MSForeachStatement penWebMSForeachStatement: 
                    return new PenWebMSForeachStatement( parentNode, penWebMSForeachStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NameQualifier penWebNameQualifier: 
                    return new PenWebNameQualifier( parentNode, penWebNameQualifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NamespaceAliasDefinition penWebNamespaceAliasDefinition: 
                    return new PenWebNamespaceAliasDefinition( parentNode, penWebNamespaceAliasDefinition );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NamespaceDefinition penWebNamespaceDefinition: 
                    return new PenWebNamespaceDefinition( parentNode, penWebNamespaceDefinition );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NamespaceDefinitionName penWebNamespaceDefinitionName: 
                    return new PenWebNamespaceDefinitionName( parentNode, penWebNamespaceDefinitionName );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NestedDeclarator penWebNestedDeclarator: 
                    return new PenWebNestedDeclarator( parentNode, penWebNestedDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NewExpression penWebNewExpression: 
                    return new PenWebNewExpression( parentNode, penWebNewExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.OperatorFunctionId penWebOperatorFunctionId: 
                    return new PenWebOperatorFunctionId( parentNode, penWebOperatorFunctionId );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ParametersAndQualifiers penWebParametersAndQualifiers: 
                    return new PenWebParametersAndQualifiers( parentNode, penWebParametersAndQualifiers );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ParenExpression penWebParenExpression: 
                    return new PenWebParenExpression( parentNode, penWebParenExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.PostfixExpression penWebPostfixExpression: 
                    return new PenWebPostfixExpression( parentNode, penWebPostfixExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.PPPragmaDirective penWebPPPragmaDirective: 
                    return new PenWebPPPragmaDirective( parentNode, penWebPPPragmaDirective );

                case JetBrains.ReSharper.Psi.Cpp.Tree.PragmaDirective penWebPragmaDirective: 
                    return new PenWebPragmaDirective( parentNode, penWebPragmaDirective );

                case JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedBaseTypeReference penWebQualifiedBaseTypeReference: 
                    return new PenWebQualifiedBaseTypeReference( parentNode, penWebQualifiedBaseTypeReference );

                case JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedNamespaceReference penWebQualifiedNamespaceReference: 
                    return new PenWebQualifiedNamespaceReference( parentNode, penWebQualifiedNamespaceReference );

                case JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedReference penWebQualifiedReference: 
                    return new PenWebQualifiedReference( parentNode, penWebQualifiedReference );

                case JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedUsingDeclarationTargetReference penWebQualifiedUsingDeclarationTargetReference: 
                    return new PenWebQualifiedUsingDeclarationTargetReference( parentNode, penWebQualifiedUsingDeclarationTargetReference );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ReturnStatement penWebReturnStatement: 
                    return new PenWebReturnStatement( parentNode, penWebReturnStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.SimpleDeclaration penWebSimpleDeclaration: 
                    return new PenWebSimpleDeclaration( parentNode, penWebSimpleDeclaration );

                case JetBrains.ReSharper.Psi.Cpp.Tree.SizeOfExpression penWebSizeOfExpression: 
                    return new PenWebSizeOfExpression( parentNode, penWebSizeOfExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.SubscriptExpression penWebSubscriptExpression: 
                    return new PenWebSubscriptExpression( parentNode, penWebSubscriptExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.SwitchStatement penWebSwitchStatement: 
                    return new PenWebSwitchStatement( parentNode, penWebSwitchStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.TemplateArgumentList penWebTemplateArgumentList: 
                    return new PenWebTemplateArgumentList( parentNode, penWebTemplateArgumentList );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ThisExpression penWebThisExpression: 
                    return new PenWebThisExpression( parentNode, penWebThisExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ThrowExpression penWebThrowExpression: 
                    return new PenWebThrowExpression( parentNode, penWebThrowExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.TryStatement penWebTryStatement: 
                    return new PenWebTryStatement( parentNode, penWebTryStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.TypeId penWebTypeId: 
                    return new PenWebTypeId( parentNode, penWebTypeId );

                case JetBrains.ReSharper.Psi.Cpp.Tree.UnaryExpression penWebUnaryExpression: 
                    return new PenWebUnaryExpression( parentNode, penWebUnaryExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.UsingDeclaration penWebUsingDeclaration: 
                    return new PenWebUsingDeclaration( parentNode, penWebUsingDeclaration );

                case JetBrains.ReSharper.Psi.Cpp.Tree.UsingDeclarator penWebUsingDeclarator: 
                    return new PenWebUsingDeclarator( parentNode, penWebUsingDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.UsingDirective penWebUsingDirective: 
                    return new PenWebUsingDirective( parentNode, penWebUsingDirective );

                case JetBrains.ReSharper.Psi.Cpp.Tree.WhileStatement penWebWhileStatement: 
                    return new PenWebWhileStatement( parentNode, penWebWhileStatement );

                //case JetBrains.ReSharper.Psi.Cpp.Parsing.CppGenericKeywordTokenNode penWebCppGenericKeywordTokenNode:
                    //return new PenWebCppGenericKeywordTokenNode( penWebCppGenericKeywordTokenNode );

                //case JetBrains.ReSharper.Psi.Cpp.Tree.CppChameleonCompoundStatement penWebCppChameleonCompoundStatement:
                    //return new PenWebCppChameleonCompoundStatement( penWebCppChameleonCompoundStatement );

                default:
                    return this.CreateUnknown(parentNode, treeNode);
            }
        }

        private CppParseTreeNodeBase CreateUnknown(CppParseTreeNodeBase parentNode, ITreeNode treeNode)
        {
            string nodeType = treeNode.NodeType.ToString();

            if ( !this.NodeSchemaMap.ContainsKey( nodeType ) )
            {
                this.NodeSchemaMap.Add(nodeType, nodeType);
            }

            return new CppGenericTreeNode(parentNode, treeNode);
        }

        public void DumpTreeSchema()
        {
            CppCodeAnalysis.DumpJson("TreeSchema.json", this.NodeSchemaMap.Values);
            CppCodeAnalysis.DumpJson("TreeTypes.json", this.NodeTypeMap.Values);
        }

        public static void Start()
        {
            new CppParseTreeNodeFactory();
        }
    }
}
