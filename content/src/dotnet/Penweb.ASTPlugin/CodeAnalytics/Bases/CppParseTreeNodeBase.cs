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
    public class HierarchySnapshot
    {
        public List<string> AncestorChain { get; } = new List<string>();

        public Dictionary<string, int> Descendants { get; } = new Dictionary<string, int>();

        public HierarchySnapshot(CppParseTreeNodeBase focusNode)
        {
            CppParseTreeNodeBase node = focusNode.ParentNode;

            while (node != null)
            {
                AncestorChain.Add(node.GetType().Name);
                node = node.ParentNode;
            }

            foreach (CppParseTreeNodeBase descendant in focusNode.WalkAllDescendants())
            {
                string childType = descendant.GetType().Name;

                if (this.Descendants.ContainsKey(childType))
                {
                    int currentCnt = this.Descendants[childType];

                    this.Descendants[childType] = currentCnt + 1;
                }
                else
                {
                    this.Descendants.Add(childType, 1);
                }
            }
        }
    }

    public static class LocationUtils
    {
        public static CppLocation GetLocation( this ITreeNode treeNode)
        {
            return new CppLocation(treeNode);
        }
    }

    public class CppLocation
    {
        private static int NextId = 1;

        public int Id          { get; private set; }

        public string SourceFile { get; set; }
        public int StartLine   { get; set; }
        public int StartColumn { get; set; }
        public int EndLine     { get; set; }
        public int EndColumn   { get; set; }

        public bool IsSingleLine => StartLine == EndLine;

        public CppLocation(ITreeNode treeNode)
        {
            try
            {
                this.SourceFile = treeNode.GetSourceFile().DisplayName;
                this.Id         = NextId++;

                DocumentRange documentRange = treeNode.GetNavigationRange();

                DocumentCoords start = documentRange.StartOffset.ToDocumentCoords();
                DocumentCoords end   = documentRange.EndOffset.ToDocumentCoords();

                string startLineStr = start.Line.ToString();

                startLineStr = startLineStr.Replace(",", "");

                int startLine = 0;

                if (  Int32.TryParse( startLineStr, out startLine ) )
                {
                    StartLine = startLine + 1;
                }
                else
                {
                    LogManager.Self.Log($"CoordParse Failed: {startLineStr}");
                }

                string startColStr = start.Column.ToString();

                startColStr = startColStr.Replace(",", "");

                int startCol = 0;

                if (  Int32.TryParse( startColStr, out startCol ) )
                {
                    StartColumn = startCol + 1;
                }
                else
                {
                    LogManager.Self.Log($"CoordParse Failed: {startColStr}");
                }

                string endLineStr = end.Line.ToString();

                endLineStr = endLineStr.Replace(",", "");

                int endLine = 0;

                if (  Int32.TryParse( endLineStr, out endLine ) )
                {
                    EndLine = endLine + 1;
                }
                else
                {
                    LogManager.Self.Log($"CoordParse Failed: {endLineStr}");
                }

                string endColStr = end.Column.ToString();

                endColStr = endColStr.Replace(",", "");

                int endCol = 0;

                if (  Int32.TryParse( endColStr, out endCol ) )
                {
                    EndColumn = endCol + 1;
                }
                else
                {
                    LogManager.Self.Log($"CoordParse Failed: {endColStr}");
                }
            }
            catch ( Exception ex )
            {
                LogManager.Self.Log($"Location parse error");
            }
        }

        public override string ToString()
        {
            return $"{SourceFile}:{Id}    {StartLine}-{StartColumn}:{EndLine}-{EndColumn}";
        }
    }

    public enum CppFunctionCatagory
    {
        None,
        ClassDef,
        ScreenDef,
        EnumDef,
        ListDef,
        MessageMap,
        VariableDef,
        VariableRef,
        MethodDef,
        MethodCall,
    }

    public enum AstState
    {
        Root,
        InClass,
        InEnum,
        InMethod,
        InMessageMap,
    }

    [JsonObject(MemberSerialization=MemberSerialization.OptIn,IsReference=false)]
    public abstract class CppParseTreeNodeBase
    {
        protected bool saveToJson;
        public bool SaveToJson
        {
            get => this.GetSaveToJson();
            set => this.SetSaveToJson(value);
        }

        public CppFunctionCatagory CppFunctionCatagory { get; set; } = CppFunctionCatagory.None;

        public ITreeNode TreeNode { get; set; }

        public string NodeType { get; set; }
        public CppLocation Location { get; set; }

        public string SingleLineText = "";

        [JsonProperty] public string LocationStr => this.Location.ToString();
        [JsonProperty] public string NodeTypeStr => this.GetType().Name;
        [JsonProperty] public string MyToString    => this.ToString();

        public List<CppParseTreeNodeBase> ChildNodes { get; } = new List<CppParseTreeNodeBase>();

        public CppParseTreeNodeBase ParentNode { get; set; }

        public AstState AstState      { get; set; } = AstState.Root;
        public string CurrentType     { get; set; } = "";
        public string CurrentMethod   { get; set; } = "";

        public CppParseTreeNodeBase(CppParseTreeNodeBase parentNode, ITreeNode treeNode)
        {
            try
            {
                this.ParentNode = parentNode;

                if (this.ParentNode == null)
                {
                    this.AstState = AstState.Root;
                    this.CurrentType = "";
                    this.CurrentMethod = "";
                }
                else
                {
                    this.AstState      = parentNode.AstState;
                    this.CurrentType   = parentNode.CurrentType;
                    this.CurrentMethod = parentNode.CurrentMethod;
                }

                //this.TreeNode = treeNode;

                this.NodeType = treeNode.NodeType.ToString();

                this.Location = treeNode.GetLocation();

                if (this.Location.IsSingleLine)
                {
                    string text = treeNode.GetText();

                    if (!text.Contains("\r") && !text.Contains("\n"))
                    {
                        this.SingleLineText = treeNode.GetText();
                    }
                }

                foreach (ITreeNode childTreeNode in treeNode.Children())
                {
                    CppParseTreeNodeBase childParseTreeNode =
                        CppParseTreeNodeFactory.Self.CreateTypedNode(this, childTreeNode);

                    if (childParseTreeNode != null)
                    {
                        this.ChildNodes.Add(childParseTreeNode);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.Self.Log($"{this.GetType().Name}() - Excpetion ", ex);
            }
        }

        public virtual void Init()
        {
            try
            {
                foreach (CppParseTreeNodeBase childNode in this.ChildNodes)
                {
                    childNode.Init();
                }
            }
            catch (Exception ex)
            {
                LogManager.Self.Log($"{this.GetType().Name}.Init() - Excpetion ", ex);
            }
        }

        protected bool GetSaveToJson()
        {
            return this.saveToJson;
        }

        protected void SetSaveToJson(bool value)
        {
            if (value && !this.saveToJson )
            {
                CppCodeAnalysis.SaveTreeNode(this);
            }

            this.saveToJson = value;
        }

        public IEnumerable<CppParseTreeNodeBase> WalkAllDescendants()
        {
            foreach (CppParseTreeNodeBase child in this.ChildNodes)
            {
                foreach (CppParseTreeNodeBase grandChild in child.WalkAllDescendantsInternal())
                {
                    yield return grandChild;
                }
            }
        }

        protected IEnumerable<CppParseTreeNodeBase> WalkAllDescendantsInternal()
        {
            yield return this;

            foreach (CppParseTreeNodeBase child in this.ChildNodes)
            {
                foreach (CppParseTreeNodeBase grandChild in child.WalkAllDescendantsInternal())
                {
                    yield return grandChild;
                }
            }
        }

        public void VisitAllDescendants(Action<CppParseTreeNodeBase> callback, bool isChild = false)
        {
            if (isChild)
            {
                callback(this);
            }

            foreach (CppParseTreeNodeBase child in this.ChildNodes)
            {
                child.VisitAllDescendants(callback, true);

            }
        }

        public List<TNodeType> GetAllChildrenByTypeAsList<TNodeType>() where TNodeType : CppParseTreeNodeBase
        {
            try
            {
                List<TNodeType> nodeList = new List<TNodeType>();

                foreach (TNodeType node in this.GetAllChildrenByType<TNodeType>())
                {
                    nodeList.Add(node);
                }

                return nodeList;
            }
            catch (Exception ex)
            {
                LogManager.Self.Log($"{this.GetType().Name}.GetAllChildrenByTypeAsList<{typeof(TNodeType).Name}>() - Excpetion ", ex);
                return null;
            }
        }

        public IEnumerable<TNodeType> GetAllChildrenByType<TNodeType>() where TNodeType : CppParseTreeNodeBase
        {
            if (this is TNodeType)
            {
                yield return this as TNodeType;
            }

            foreach (var child in this.ChildNodes)
            {
                foreach (var grandChild in child.GetAllChildrenByType<TNodeType>())
                {
                    yield return grandChild;
                }
            }
        }

        public TNodeType GetChildByType<TNodeType>() where TNodeType : CppParseTreeNodeBase
        {
            try
            {
                if (this is TNodeType)
                {
                    return this as TNodeType;
                }

                foreach (var child in this.ChildNodes)
                {
                    TNodeType nodeType = child.GetChildByType<TNodeType>();

                    if (nodeType != null)
                    {
                        return nodeType;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                LogManager.Self.Log($"{this.GetType().Name}.GetChildByType<{typeof(TNodeType).Name}>() - Excpetion ", ex);
                return null;
            }
        }

        public TNodeType GetParentByType<TNodeType>() where TNodeType : CppParseTreeNodeBase
        {
            try
            {
                if (this.ParentNode == null)
                {
                    return null;
                }
                else if (this.ParentNode is TNodeType)
                {
                    return this.ParentNode as TNodeType;
                }
                else
                {
                    return this.ParentNode.GetParentByType<TNodeType>();
                }
            }
            catch (Exception ex)
            {
                LogManager.Self.Log($"{this.GetType().Name}.GetParentByType<{typeof(TNodeType).Name}>() - Excpetion ", ex);
                return null;
            }
        }

        public TNodeType GetTopParentByType<TNodeType>(TNodeType currentTopParent) where TNodeType : CppParseTreeNodeBase
        {
            try
            {
                if (this.ParentNode == null)
                {
                    return currentTopParent;
                }
                else if (this.ParentNode is TNodeType)
                {
                    return this.ParentNode.GetTopParentByType<TNodeType>(this.ParentNode as TNodeType); ;
                }
                else
                {
                    return this.ParentNode.GetTopParentByType<TNodeType>(currentTopParent);
                }
            }
            catch (Exception ex)
            {
                LogManager.Self.Log($"{this.GetType().Name}.GetTopParentByType<{typeof(TNodeType).Name}>() - Excpetion ", ex);
                return null;
            }
        }

        public override string ToString()
        {
            return $"[{this.Location.ToString()}]  {this.GetType().Name} |{SingleLineText}|";
        }

        public virtual void WriteLog(string tabStr, TextWriter textWriter)
        {
            textWriter.WriteLine($"{tabStr}{this.ToString()}");

            foreach ( var child in this.ChildNodes )
            {
                child.WriteLog($"    {tabStr}", textWriter);
            }
        }
    }

    public class PenWebAccessSpecifier : CppParseTreeNodeBase
    {
        public PenWebAccessSpecifier( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.AccessSpecifier treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebArraySizeSpecifier : CppParseTreeNodeBase
    {
        public PenWebArraySizeSpecifier( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ArraySizeSpecifier treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebBracedInitList : CppParseTreeNodeBase
    {
        public PenWebBracedInitList( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.BracedInitList treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCatchSection : CppParseTreeNodeBase
    {
        public PenWebCatchSection( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CatchSection treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCompoundStatement : CppParseTreeNodeBase
    {
        public PenWebCompoundStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CompoundStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }


    public class PenWebContinueStatement : CppParseTreeNodeBase
    {
        public PenWebContinueStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ContinueStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCppChameleonCompoundStatement : CppParseTreeNodeBase
    {
        public PenWebCppChameleonCompoundStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CppChameleonCompoundStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCppChameleonCtorBlock : CppParseTreeNodeBase
    {
        public PenWebCppChameleonCtorBlock( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CppChameleonCtorBlock treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCxxCliPropertyDeclaration : CppParseTreeNodeBase
    {
        public PenWebCxxCliPropertyDeclaration( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CxxCliPropertyDeclaration treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebCxxCliPropertyOrEventDeclarator : CppParseTreeNodeBase
    {
        public PenWebCxxCliPropertyOrEventDeclarator( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.CxxCliPropertyOrEventDeclarator treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebDefaultStatement : CppParseTreeNodeBase
    {
        public PenWebDefaultStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.DefaultStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }


    public class PenWebDoStatement : CppParseTreeNodeBase
    {
        public PenWebDoStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.DoStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebDoStatementBody : CppParseTreeNodeBase
    {
        public PenWebDoStatementBody( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.DoStatementBody treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebEmptyDeclaration : CppParseTreeNodeBase
    {
        public PenWebEmptyDeclaration( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.EmptyDeclaration treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebEmptyStatement : CppParseTreeNodeBase
    {
        public PenWebEmptyStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.EmptyStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebEnumerator : CppParseTreeNodeBase
    {
        public PenWebEnumerator( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.Enumerator treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebForStatement : CppParseTreeNodeBase
    {
        public PenWebForStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ForStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebImportDirective : CppParseTreeNodeBase
    {
        public PenWebImportDirective( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ImportDirective treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebLinkageSpecification : CppParseTreeNodeBase
    {
        public PenWebLinkageSpecification( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.LinkageSpecification treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebMemInitializer : CppParseTreeNodeBase
    {
        public PenWebMemInitializer( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MemInitializer treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebMemInitializerName : CppParseTreeNodeBase
    {
        public PenWebMemInitializerName( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MemInitializerName treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebMSAttributes : CppParseTreeNodeBase
    {
        public PenWebMSAttributes( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MSAttributes treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebMSForeachStatement : CppParseTreeNodeBase
    {
        public PenWebMSForeachStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.MSForeachStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebNamespaceAliasDefinition : CppParseTreeNodeBase
    {
        public PenWebNamespaceAliasDefinition( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.NamespaceAliasDefinition treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebNamespaceDefinition : CppParseTreeNodeBase
    {
        public PenWebNamespaceDefinition( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.NamespaceDefinition treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebNamespaceDefinitionName : CppParseTreeNodeBase
    {
        public PenWebNamespaceDefinitionName( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.NamespaceDefinitionName treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebPPPragmaDirective : CppParseTreeNodeBase
    {
        public PenWebPPPragmaDirective( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.PPPragmaDirective treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebPragmaDirective : CppParseTreeNodeBase
    {
        public PenWebPragmaDirective( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.PragmaDirective treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebReturnStatement : CppParseTreeNodeBase
    {
        public PenWebReturnStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.ReturnStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebTemplateArgumentList : CppParseTreeNodeBase
    {
        public PenWebTemplateArgumentList( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.TemplateArgumentList treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebTryStatement : CppParseTreeNodeBase
    {
        public PenWebTryStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.TryStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebUsingDeclaration : CppParseTreeNodeBase
    {
        public PenWebUsingDeclaration( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.UsingDeclaration treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebUsingDeclarator : CppParseTreeNodeBase
    {
        public PenWebUsingDeclarator( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.UsingDeclarator treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebUsingDirective : CppParseTreeNodeBase
    {
        public PenWebUsingDirective( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.UsingDirective treeNode ) : base(parentNode, treeNode)
        {
        }
    }

    public class PenWebWhileStatement : CppParseTreeNodeBase
    {
        public PenWebWhileStatement( CppParseTreeNodeBase parentNode, JetBrains.ReSharper.Psi.Cpp.Tree.WhileStatement treeNode ) : base(parentNode, treeNode)
        {
        }
    }
}
