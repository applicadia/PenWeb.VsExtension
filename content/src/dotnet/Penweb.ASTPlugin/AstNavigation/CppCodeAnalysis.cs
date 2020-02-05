using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using JetBrains.Application.UI.UIAutomation;
using JetBrains.DataFlow;
using JetBrains.ProjectModel;
using JetBrains.Lifetimes;
using System.IO;
using JetBrains.ReSharper.Psi.Tree;
using System;

namespace PenWeb.ASTPlugin
{

    public abstract class CppFileContextBase
    {
        public IProjectFile ProjectFile { get; set; }

        public string FileName { get; set; }
        public string FullName { get; set; }

        public string Ext { get; set; }

        public CppFileContextBase(string fileName)
        {
            this.FullName = fileName;  

            this.FileName = Path.GetFileNameWithoutExtension(this.FullName);

            this.Ext = Path.GetExtension(this.FullName).ToLower().Replace(".","");
        }
    }

    public class CppCodeContext : CppFileContextBase
    {
        public CppHeaderContext CppHeaderContext { get; set; }

        public CppCodeContext( string fileName ) : base( fileName )
        {

        }
    }

    public class CppHeaderContext : CppFileContextBase
    {
        public CppCodeContext CppCodeContext { get; set; }
        public CppHeaderContext( string fileName ) : base( fileName )
        {

        }
    }

    public static class CppCodeAnalysis
    {
        public static string RsAnalyticsDir { get; } = @"c:\PenGit2\RsAnalytics";

        public static IProject PenradProject { get; set; }

        public static SortedDictionary<string, CppCodeContext> PenadCppFiles      { get; } = new SortedDictionary<string, CppCodeContext>();
        public static SortedDictionary<string, CppHeaderContext> PenadHeaderFiles { get; } = new SortedDictionary<string, CppHeaderContext>();

        public static IProperty<IEnumerable<string>> ReferencedElementsNamesList { get; set; }
        public static IProperty<int> SelectedReferencedElement { get; set; }

        public static void DoAnalytics(Lifetime lifetime)
        {
            var solutionStateTracker = SolutionStateTracker.Instance;

            solutionStateTracker?.SolutionName.Change.Advise_HasNew(lifetime, () =>
            {
                try
                {
                    var solution = solutionStateTracker?.Solution;
                    if (solution == null) return;

                    List<IProject> projects = solution.GetProjectsByName("penrad").ToList();

                    if ( projects.Count == 1 )
                    {
                        PenradProject = projects.First();

                        List<IProjectFile> projectFiles = PenradProject.GetAllProjectFiles().ToList();

                        foreach ( IProjectFile projectFile in projectFiles )
                        {
                            string ext = Path.GetExtension(projectFile.Name).ToLower();

                            switch ( ext )
                            {
                                case ".cpp":
                                    CppCodeContext cppCodeContext = new CppCodeContext(projectFile.Name);
                                    cppCodeContext.ProjectFile = projectFile;
                                    PenadCppFiles.Add(cppCodeContext.FileName.ToLower(), cppCodeContext);
                                    break;

                                case ".h":
                                    CppHeaderContext cppHeaderContext = new CppHeaderContext(projectFile.Name);
                                    cppHeaderContext.ProjectFile = projectFile;
                                    PenadHeaderFiles.Add(cppHeaderContext.FileName.ToLower(), cppHeaderContext);
                                    break;
                            }
                        }

                        LinkFiles();     
                        AnalyzeHeaderFiles();
                        AnalyzeCodeFiles();
                    }
                }
                catch ( Exception ex )
                {
                    Console.WriteLine($"Excpetion {ex.Message}");
                }
            });
        }

        public static void LinkFiles()
        {
            foreach ( CppCodeContext cppCodeContext in PenadCppFiles.Values )
            {
                string fileName = cppCodeContext.FileName.ToLower();

                if ( PenadHeaderFiles.ContainsKey(fileName))
                {
                    CppHeaderContext cppHeaderContext = PenadHeaderFiles[fileName];

                    cppHeaderContext.CppCodeContext = cppCodeContext;
                    cppCodeContext.CppHeaderContext = cppHeaderContext;
                }
            }
        }

        public static void AnalyzeClasses()
        {
            //IProject project = PenradProject.GetComponent<>

            //ITreeNode treeNode = null;

        }

        public static void AnalyzeHeaderFiles()
        {
            foreach ( CppHeaderContext cppHeaderContext in PenadHeaderFiles.Values )
            {
                AnalyzeHeaderContext(cppHeaderContext);
            }
        }

        public static void AnalyzeHeaderContext(CppHeaderContext cppHeaderContext)
        {
            IProjectFile projectFile = cppHeaderContext.ProjectFile;

            using ( TextWriter writer = File.CreateText(CreateAnalyticsFilePath($"{cppHeaderContext.FileName}-h.txt")))
            {
                projectFile.Dump(writer, DumpFlags.FULL);
            }
        }


        public static void AnalyzeCodeFiles()
        {
            foreach ( CppCodeContext cppCodeContext in PenadCppFiles.Values )
            {
                AnalyzeCodeContext(cppCodeContext);
            }
        }

        public static void AnalyzeCodeContext(CppCodeContext cppCodeContext)
        {
            IProjectFile projectFile = cppCodeContext.ProjectFile;

            using ( TextWriter writer = File.CreateText(CreateAnalyticsFilePath($"{cppCodeContext.FileName}-cpp.txt")))
            {
                projectFile.Dump(writer, DumpFlags.FULL);
            }
        }

        public static string CreateAnalyticsFilePath(string fileName)
        {
            return Path.Combine(RsAnalyticsDir, fileName);
        }
    }
}