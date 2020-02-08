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

        public CppParseTreeNodeBase CreateTypedNode(ITreeNode treeNode)
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
                    return new PenWebCppCommentTokenNode( penWebCppCommentTokenNode );

                case JetBrains.ReSharper.Psi.Cpp.Parsing.CppFromSubstitutionTokenNode penWebCppFromSubstitutionTokenNode: 
                    return new PenWebCppFromSubstitutionTokenNode( penWebCppFromSubstitutionTokenNode );

                case JetBrains.ReSharper.Psi.Cpp.Parsing.CppGenericTokenNode penWebCppGenericTokenNode: 
                    return new PenWebCppGenericTokenNode( penWebCppGenericTokenNode );

                case JetBrains.ReSharper.Psi.Cpp.Parsing.CppIdentifierTokenNode penWebCppIdentifierTokenNode: 
                    return new PenWebCppIdentifierTokenNode( penWebCppIdentifierTokenNode );

                case JetBrains.ReSharper.Psi.Cpp.Tree.AbstractDeclarator penWebAbstractDeclarator: 
                    return new PenWebAbstractDeclarator( penWebAbstractDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.AbstractDeclaratorName penWebAbstractDeclaratorName: 
                    return new PenWebAbstractDeclaratorName( penWebAbstractDeclaratorName );

                case JetBrains.ReSharper.Psi.Cpp.Tree.AccessSpecifier penWebAccessSpecifier: 
                    return new PenWebAccessSpecifier( penWebAccessSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ArraySizeSpecifier penWebArraySizeSpecifier: 
                    return new PenWebArraySizeSpecifier( penWebArraySizeSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.BaseClause penWebBaseClause: 
                    return new PenWebBaseClause( penWebBaseClause );

                case JetBrains.ReSharper.Psi.Cpp.Tree.BaseSpecifier penWebBaseSpecifier: 
                    return new PenWebBaseSpecifier( penWebBaseSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.BinaryExpression penWebBinaryExpression: 
                    return new PenWebBinaryExpression( penWebBinaryExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.BracedInitList penWebBracedInitList: 
                    return new PenWebBracedInitList( penWebBracedInitList );

                case JetBrains.ReSharper.Psi.Cpp.Tree.BreakStatement penWebBreakStatement: 
                    return new PenWebBreakStatement( penWebBreakStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CallExpression penWebCallExpression: 
                    return new PenWebCallExpression( penWebCallExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CaseStatement penWebCaseStatement: 
                    return new PenWebCaseStatement( penWebCaseStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CastExpression penWebCastExpression: 
                    return new PenWebCastExpression( penWebCastExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CatchSection penWebCatchSection: 
                    return new PenWebCatchSection( penWebCatchSection );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ClassQualifiedName penWebClassQualifiedName: 
                    return new PenWebClassQualifiedName( penWebClassQualifiedName );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ClassSpecifier penWebClassSpecifier: 
                    return new PenWebClassSpecifier( penWebClassSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CliTypeIdExpression penWebCliTypeIdExpression: 
                    return new PenWebCliTypeIdExpression( penWebCliTypeIdExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CompoundStatement penWebCompoundStatement: 
                    return new PenWebCompoundStatement( penWebCompoundStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ConditionalExpression penWebConditionalExpression: 
                    return new PenWebConditionalExpression( penWebConditionalExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ContinueStatement penWebContinueStatement: 
                    return new PenWebContinueStatement( penWebContinueStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CppChameleonCtorBlock penWebCppChameleonCtorBlock: 
                    return new PenWebCppChameleonCtorBlock( penWebCppChameleonCtorBlock );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CtorInitializer penWebCtorInitializer: 
                    return new PenWebCtorInitializer( penWebCtorInitializer );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CxxCliPropertyDeclaration penWebCxxCliPropertyDeclaration: 
                    return new PenWebCxxCliPropertyDeclaration( penWebCxxCliPropertyDeclaration );

                case JetBrains.ReSharper.Psi.Cpp.Tree.CxxCliPropertyOrEventDeclarator penWebCxxCliPropertyOrEventDeclarator: 
                    return new PenWebCxxCliPropertyOrEventDeclarator( penWebCxxCliPropertyOrEventDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.Declaration penWebDeclaration: 
                    return new PenWebDeclaration( penWebDeclaration );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationSpecifiers penWebDeclarationSpecifiers: 
                    return new PenWebDeclarationSpecifiers( penWebDeclarationSpecifiers );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationSpecifierTypename penWebDeclarationSpecifierTypename: 
                    return new PenWebDeclarationSpecifierTypename( penWebDeclarationSpecifierTypename );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DeclarationStatement penWebDeclarationStatement: 
                    return new PenWebDeclarationStatement( penWebDeclarationStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.Declarator penWebDeclarator: 
                    return new PenWebDeclarator( penWebDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DeclaratorQualifiedName penWebDeclaratorQualifiedName: 
                    return new PenWebDeclaratorQualifiedName( penWebDeclaratorQualifiedName );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DefaultStatement penWebDefaultStatement: 
                    return new PenWebDefaultStatement( penWebDefaultStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DeleteExpression penWebDeleteExpression: 
                    return new PenWebDeleteExpression( penWebDeleteExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.Directive penWebDirective: 
                    return new PenWebDirective( penWebDirective );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DoStatement penWebDoStatement: 
                    return new PenWebDoStatement( penWebDoStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.DoStatementBody penWebDoStatementBody: 
                    return new PenWebDoStatementBody( penWebDoStatementBody );

                case JetBrains.ReSharper.Psi.Cpp.Tree.EmptyDeclaration penWebEmptyDeclaration: 
                    return new PenWebEmptyDeclaration( penWebEmptyDeclaration );

                case JetBrains.ReSharper.Psi.Cpp.Tree.EmptyStatement penWebEmptyStatement: 
                    return new PenWebEmptyStatement( penWebEmptyStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.Enumerator penWebEnumerator: 
                    return new PenWebEnumerator( penWebEnumerator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.EnumSpecifier penWebEnumSpecifier: 
                    return new PenWebEnumSpecifier( penWebEnumSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ExpressionStatement penWebExpressionStatement: 
                    return new PenWebExpressionStatement( penWebExpressionStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ForStatement penWebForStatement: 
                    return new PenWebForStatement( penWebForStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.FunctionArgumentList penWebFunctionArgumentList: 
                    return new PenWebFunctionArgumentList( penWebFunctionArgumentList );

                case JetBrains.ReSharper.Psi.Cpp.Tree.FwdClassSpecifier penWebFwdClassSpecifier: 
                    return new PenWebFwdClassSpecifier( penWebFwdClassSpecifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.IfStatement penWebIfStatement: 
                    return new PenWebIfStatement( penWebIfStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ImportDirective penWebImportDirective: 
                    return new PenWebImportDirective( penWebImportDirective );

                case JetBrains.ReSharper.Psi.Cpp.Tree.InitDeclarator penWebInitDeclarator: 
                    return new PenWebInitDeclarator( penWebInitDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.LinkageSpecification penWebLinkageSpecification: 
                    return new PenWebLinkageSpecification( penWebLinkageSpecification );

                case JetBrains.ReSharper.Psi.Cpp.Tree.LiteralExpression penWebLiteralExpression: 
                    return new PenWebLiteralExpression( penWebLiteralExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroArgument penWebMacroArgument: 
                    return new PenWebMacroArgument( penWebMacroArgument );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroArgumentList penWebMacroArgumentList: 
                    return new PenWebMacroArgumentList( penWebMacroArgumentList );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroBody penWebMacroBody: 
                    return new PenWebMacroBody( penWebMacroBody );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroCall penWebMacroCall: 
                    return new PenWebMacroCall( penWebMacroCall );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroDefinition penWebMacroDefinition: 
                    return new PenWebMacroDefinition( penWebMacroDefinition );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroParameter penWebMacroParameter: 
                    return new PenWebMacroParameter( penWebMacroParameter );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroParameterList penWebMacroParameterList: 
                    return new PenWebMacroParameterList( penWebMacroParameterList );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroReference penWebMacroReference: 
                    return new PenWebMacroReference( penWebMacroReference );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MacroUndefinition penWebMacroUndefinition: 
                    return new PenWebMacroUndefinition( penWebMacroUndefinition );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MemberAccessExpression penWebMemberAccessExpression: 
                    return new PenWebMemberAccessExpression( penWebMemberAccessExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MemInitializer penWebMemInitializer: 
                    return new PenWebMemInitializer( penWebMemInitializer );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MemInitializerName penWebMemInitializerName: 
                    return new PenWebMemInitializerName( penWebMemInitializerName );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MSAttributes penWebMSAttributes: 
                    return new PenWebMSAttributes( penWebMSAttributes );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MSDeclSpec penWebMSDeclSpec: 
                    return new PenWebMSDeclSpec( penWebMSDeclSpec );

                case JetBrains.ReSharper.Psi.Cpp.Tree.MSForeachStatement penWebMSForeachStatement: 
                    return new PenWebMSForeachStatement( penWebMSForeachStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NameQualifier penWebNameQualifier: 
                    return new PenWebNameQualifier( penWebNameQualifier );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NamespaceAliasDefinition penWebNamespaceAliasDefinition: 
                    return new PenWebNamespaceAliasDefinition( penWebNamespaceAliasDefinition );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NamespaceDefinition penWebNamespaceDefinition: 
                    return new PenWebNamespaceDefinition( penWebNamespaceDefinition );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NamespaceDefinitionName penWebNamespaceDefinitionName: 
                    return new PenWebNamespaceDefinitionName( penWebNamespaceDefinitionName );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NestedDeclarator penWebNestedDeclarator: 
                    return new PenWebNestedDeclarator( penWebNestedDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.NewExpression penWebNewExpression: 
                    return new PenWebNewExpression( penWebNewExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.OperatorFunctionId penWebOperatorFunctionId: 
                    return new PenWebOperatorFunctionId( penWebOperatorFunctionId );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ParametersAndQualifiers penWebParametersAndQualifiers: 
                    return new PenWebParametersAndQualifiers( penWebParametersAndQualifiers );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ParenExpression penWebParenExpression: 
                    return new PenWebParenExpression( penWebParenExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.PostfixExpression penWebPostfixExpression: 
                    return new PenWebPostfixExpression( penWebPostfixExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.PPPragmaDirective penWebPPPragmaDirective: 
                    return new PenWebPPPragmaDirective( penWebPPPragmaDirective );

                case JetBrains.ReSharper.Psi.Cpp.Tree.PragmaDirective penWebPragmaDirective: 
                    return new PenWebPragmaDirective( penWebPragmaDirective );

                case JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedBaseTypeReference penWebQualifiedBaseTypeReference: 
                    return new PenWebQualifiedBaseTypeReference( penWebQualifiedBaseTypeReference );

                case JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedNamespaceReference penWebQualifiedNamespaceReference: 
                    return new PenWebQualifiedNamespaceReference( penWebQualifiedNamespaceReference );

                case JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedReference penWebQualifiedReference: 
                    return new PenWebQualifiedReference( penWebQualifiedReference );

                case JetBrains.ReSharper.Psi.Cpp.Tree.QualifiedUsingDeclarationTargetReference penWebQualifiedUsingDeclarationTargetReference: 
                    return new PenWebQualifiedUsingDeclarationTargetReference( penWebQualifiedUsingDeclarationTargetReference );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ReturnStatement penWebReturnStatement: 
                    return new PenWebReturnStatement( penWebReturnStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.SimpleDeclaration penWebSimpleDeclaration: 
                    return new PenWebSimpleDeclaration( penWebSimpleDeclaration );

                case JetBrains.ReSharper.Psi.Cpp.Tree.SizeOfExpression penWebSizeOfExpression: 
                    return new PenWebSizeOfExpression( penWebSizeOfExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.SubscriptExpression penWebSubscriptExpression: 
                    return new PenWebSubscriptExpression( penWebSubscriptExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.SwitchStatement penWebSwitchStatement: 
                    return new PenWebSwitchStatement( penWebSwitchStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.TemplateArgumentList penWebTemplateArgumentList: 
                    return new PenWebTemplateArgumentList( penWebTemplateArgumentList );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ThisExpression penWebThisExpression: 
                    return new PenWebThisExpression( penWebThisExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.ThrowExpression penWebThrowExpression: 
                    return new PenWebThrowExpression( penWebThrowExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.TryStatement penWebTryStatement: 
                    return new PenWebTryStatement( penWebTryStatement );

                case JetBrains.ReSharper.Psi.Cpp.Tree.TypeId penWebTypeId: 
                    return new PenWebTypeId( penWebTypeId );

                case JetBrains.ReSharper.Psi.Cpp.Tree.UnaryExpression penWebUnaryExpression: 
                    return new PenWebUnaryExpression( penWebUnaryExpression );

                case JetBrains.ReSharper.Psi.Cpp.Tree.UsingDeclaration penWebUsingDeclaration: 
                    return new PenWebUsingDeclaration( penWebUsingDeclaration );

                case JetBrains.ReSharper.Psi.Cpp.Tree.UsingDeclarator penWebUsingDeclarator: 
                    return new PenWebUsingDeclarator( penWebUsingDeclarator );

                case JetBrains.ReSharper.Psi.Cpp.Tree.UsingDirective penWebUsingDirective: 
                    return new PenWebUsingDirective( penWebUsingDirective );

                case JetBrains.ReSharper.Psi.Cpp.Tree.WhileStatement penWebWhileStatement: 
                    return new PenWebWhileStatement( penWebWhileStatement );

                //case JetBrains.ReSharper.Psi.Cpp.Parsing.CppGenericKeywordTokenNode penWebCppGenericKeywordTokenNode:
                    //return new PenWebCppGenericKeywordTokenNode( penWebCppGenericKeywordTokenNode );

                //case JetBrains.ReSharper.Psi.Cpp.Tree.CppChameleonCompoundStatement penWebCppChameleonCompoundStatement:
                    //return new PenWebCppChameleonCompoundStatement( penWebCppChameleonCompoundStatement );

                default:
                    return this.CreateUnknown(treeNode);
            }
        }

        public CppParseTreeNodeBase CreateNode(ITreeNode treeNode)
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

                //case "IMPORT_DIRECTIVE":     return new CppImportTreeNode(treeNode);
                //case "USING_DIRECTIVE":      return new CppUsingTreeNode(treeNode);
                //case "SIMPLE_DECLARATION":   return new CppSimpleDeclarationTreeNode(treeNode);
                //case "MACRO_CALL":           return new CppMacroCallTreeNode(treeNode);

                default:
                    return this.CreateUnknown(treeNode);
            }
        }

        private CppParseTreeNodeBase CreateUnknown(ITreeNode treeNode)
        {
            string nodeType = treeNode.NodeType.ToString();

            if ( !this.NodeSchemaMap.ContainsKey( nodeType ) )
            {
                this.NodeSchemaMap.Add(nodeType, nodeType);
            }

            return new CppGenericTreeNode(treeNode);
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
