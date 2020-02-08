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

namespace Penweb.CodeAnalytics
{
    public abstract class CppFileContextBase
    {
        public IProjectFile ProjectFile { get; set; }

        public CppFile CppFile { get; set; }

        public string FileName { get; set; }
        public string FullName { get; set; }
        public string Ext      { get; set; }

        public List<CppParseTreeNodeBase> ChildNodes { get; } = new List<CppParseTreeNodeBase>();

        public List<CppParseTreeNodeBase> SaveTreeNodes { get; } = new List<CppParseTreeNodeBase>();

        public CppFileContextBase(string fileName)
        {
            this.FullName = fileName;  

            this.FileName = Path.GetFileNameWithoutExtension(this.FullName);

            this.Ext = Path.GetExtension(this.FullName).ToLower().Replace(".","");

            this.Init();
        }

        public void Init()
        {
            this.CppFile = CppCodeAnalysis.PenradProject.GetCppFile(this.FullName);

            foreach ( ITreeNode childTreeNode in this.CppFile.Children() )
            {
                CppParseTreeNodeBase childParseTreeNode = CppParseTreeNodeFactory.Self.CreateTypedNode(null, childTreeNode);

                if ( childParseTreeNode != null )
                {
                    this.ChildNodes.Add(childParseTreeNode);
                }
            }

            this.CppFile = null;
        }

        public void InitNodes()
        {
            foreach (CppParseTreeNodeBase treeNode in this.ChildNodes)
            {
                treeNode.Init();
            }
        }

        private void InitSavedNodes()
        {
            foreach (CppParseTreeNodeBase treeNode in SaveTreeNodes)
            {
                treeNode.Init();
            }
        }

        public abstract void WriteSavedNodes();


        public virtual void DumpFile(TextWriter textWriter)
        {
            foreach ( CppParseTreeNodeBase child in this.ChildNodes )
            {
                child.WriteLog("", textWriter);
            }
        }
    }
}
