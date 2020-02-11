using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Cpp.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PenWeb.ASTPlugin;
using JetBrains.ReSharper.Psi.Tree;
using Newtonsoft.Json;

namespace Penweb.CodeAnalytics
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn,IsReference=false)]
    public class FunctionNodes
    {
        [JsonProperty] public List<PenWebClassSpecifier> ClassDefs    { get; } = new List<PenWebClassSpecifier>();
        [JsonProperty] public List<CppParseTreeNodeBase> MessageMap   { get; } = new List<CppParseTreeNodeBase>();
        [JsonProperty] public List<PenWebDeclaration>    VariableDefs { get; } = new List<PenWebDeclaration>();
        [JsonProperty] public List<PenWebQualifiedReference> VariableRefs { get; } = new List<PenWebQualifiedReference>();
        [JsonProperty] public List<CppParseTreeNodeBase> MethodDefs   { get; } = new List<CppParseTreeNodeBase>();
        [JsonProperty] public List<CppParseTreeNodeBase> MethodCalls  { get; } = new List<CppParseTreeNodeBase>();
        [JsonProperty] public List<CppParseTreeNodeBase> ScreenDefs   { get; } = new List<CppParseTreeNodeBase>();
        [JsonProperty] public List<CppParseTreeNodeBase> EnumDefs     { get; } = new List<CppParseTreeNodeBase>();
        [JsonProperty] public List<CppParseTreeNodeBase> ListDefs     { get; } = new List<CppParseTreeNodeBase>();
        [JsonProperty] public List<CppParseTreeNodeBase> Uncatagorized  { get; } = new List<CppParseTreeNodeBase>();

        [JsonProperty] public List<CppParseTreeNodeBase> All            { get; } = new List<CppParseTreeNodeBase>();

    }

    public abstract class CppFileContextBase
    {
        public IProjectFile ProjectFile { get; set; }

        public CppFile CppFile { get; set; }

        public string FileName { get; set; }
        public string FullName { get; set; }
        public string Ext      { get; set; }

        public string DialogClassName { get; set; }

        //protected TextWriter LogWriter { get; set; }

        public List<CppParseTreeNodeBase> ChildNodes { get; } = new List<CppParseTreeNodeBase>();

        public FunctionNodes SaveTreeNodes { get; } = new FunctionNodes();



        public CppFileContextBase(string fileName, string dialogClassName)
        {
            this.FullName = fileName;

            this.DialogClassName = dialogClassName;

            this.FileName = Path.GetFileNameWithoutExtension(this.FullName);

            this.Ext = Path.GetExtension(this.FullName).ToLower().Replace(".","");

            this.Init();
        }

        public virtual void Init()
        {
            string logPath = Path.Combine(CppCodeAnalysis.RsAnalyticsDir, this.FileName);

            Directory.CreateDirectory(logPath);

            logPath = Path.Combine(logPath, $"{this.FileName}-{this.Ext}.log");

            //this.LogWriter = File.CreateText(logPath);

            this.CppFile = CppCodeAnalysis.PenradProject.GetCppFile(this.FullName);

            foreach ( ITreeNode childTreeNode in this.CppFile.Children() )
            {
                CppParseTreeNodeBase childParseTreeNode = CppParseTreeNodeFactory.Self.CreateTypedNode(null, childTreeNode);

                if ( childParseTreeNode != null )
                {
                    this.ChildNodes.Add(childParseTreeNode);
                }
            }

            //this.CppFile.ContainingNodes<ClassSpecifier>();

            this.CppFile = null;
        }

        public void InitNodes()
        {
            foreach (CppParseTreeNodeBase treeNode in this.ChildNodes)
            {
                treeNode.Init();
            }
        }


        public abstract void WriteSavedNodes();

        public abstract void ProcessResults();

        public virtual void DumpFile(TextWriter textWriter)
        {
            foreach ( CppParseTreeNodeBase child in this.ChildNodes )
            {
                child.WriteLog("", textWriter);
            }
        }

        public virtual void Finalize()
        {
            /*
            if (this.LogWriter != null)
            {
                this.LogWriter.Flush();
                this.LogWriter.Close();
            }
            */
        }
    }
}
