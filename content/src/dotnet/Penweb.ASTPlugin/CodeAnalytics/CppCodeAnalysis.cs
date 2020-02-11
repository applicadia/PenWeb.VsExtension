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
using JetBrains.ReSharper.Psi.Cpp.Language;
using JetBrains.ReSharper.Psi.Cpp.Tree;
using PenWeb.ASTPlugin;
using Newtonsoft.Json;
using Penweb.CodeAnalytics.CodeGen;

namespace Penweb.CodeAnalytics
{

    public static class CppCodeAnalysis
    {
        public static string RsAnalyticsDir { get; } = @"c:\PenGit2\RsAnalytics";

        public static IProject PenradProject { get; set; }

        public static SortedDictionary<string, CppCodeContext>   PenradCppFiles    { get; } = new SortedDictionary<string, CppCodeContext>();
        public static SortedDictionary<string, CppHeaderContext> PenradHeaderFiles { get; } = new SortedDictionary<string, CppHeaderContext>();

        public static SortedDictionary<string,IProjectFile> CppFileMap { get;  } = new SortedDictionary<string, IProjectFile>();
        public static SortedDictionary<string,IProjectFile> HeaderFileMap { get;  } = new SortedDictionary<string, IProjectFile>();

        public static IProperty<IEnumerable<string>> ReferencedElementsNamesList { get; set; }
        public static IProperty<int> SelectedReferencedElement { get; set; }

        private static int HeaderCnt = 100;
        private static int CodeCnt   = 100;

        public static CppFileContextBase CurrentFileContext { get; set; }

        public static void DoAnalytics(Lifetime lifetime)
        {
            var solutionStateTracker = SolutionStateTracker.Instance;

            /*
            solutionStateTracker?.SolutionName.Change.Advise_HasNew(lifetime, () =>
            {
            });
            */

            try
            {
                PenradCppFiles.Clear();
                PenradHeaderFiles.Clear();
                CppFileMap.Clear();
                HeaderFileMap.Clear();
                CppResultsManager.Self.Clear();

                var solution = solutionStateTracker?.Solution;
                if (solution == null) return;

                List<IProject> projects = solution.GetProjectsByName("penrad").ToList();

                if ( projects.Count == 1 )
                {
                    PenradProject = projects.First();

                    MapProjectFiles();
                    ParsePenradDialogs();

                    CppResultsManager.Self.WriteResults();

                    //CppParseTreeNodeFactory.Self.DumpTreeSchema();
                    //CodeGenerator.Self.GenerateCode();
                }
            }
            catch ( Exception ex )
            {
                LogManager.Self.Log($"Excpetion {ex.Message}");
            }
        }

        private static void LoopClasse()
        {
            //IDeclaration declaration = PenradProject.GetComponent<IDeclaration>();

            IProjectItem projectElement = null;
            IModule module1 = null;

            foreach (var module in PenradProject.GetThisAndReferencedProjects())
            {

            }

        }

        private static void MapProjectFiles()
        {
            List<IProjectFile> projectFiles = PenradProject.GetAllProjectFiles().ToList();

            foreach ( IProjectFile projectFile in projectFiles )
            {
                string ext = Path.GetExtension(projectFile.Name).ToLower();

                switch ( ext )
                {
                    case ".cpp":
                        CppFileMap.Add(projectFile.Name.ToLower(), projectFile);
                        break;

                    case ".h":
                        HeaderFileMap.Add(projectFile.Name.ToLower(), projectFile);
                        break;
                }
            }
        }

        private static void ParsePenradDialogs()
        {
            foreach ( CppDialogEntry cppDialogEntry in PenradCppManager.Self.DialogMap.Values )
            {
                ProcessHeaderFile(cppDialogEntry.HeaderFile);
                ProcessCppFile(cppDialogEntry.CodeFile);
            }
        }

        private static void ProcessHeaderFile(string fileName)
        {
            if (HeaderFileMap.ContainsKey(fileName.ToLower()))
            {
                IProjectFile projectFile = HeaderFileMap[fileName.ToLower()];

                CppHeaderContext cppHeaderContext = new CppHeaderContext(projectFile.Name);

                CurrentFileContext = cppHeaderContext;

                cppHeaderContext.ProjectFile = projectFile;

                cppHeaderContext.Init();

                CurrentFileContext = cppHeaderContext;

                AnalyzeHeaderContext(cppHeaderContext);

                cppHeaderContext.WriteSavedNodes();

                cppHeaderContext.ProcessResults();

                cppHeaderContext.Finalize();
            }
            else
            {
                LogManager.Self.Log($"Missing Header File in map: {fileName}");
            }
        }


        private static void ProcessCppFile(string fileName)
        {
            if (CppFileMap.ContainsKey(fileName.ToLower()))
            {
                IProjectFile projectFile = CppFileMap[fileName.ToLower()];

                CppCodeContext cppCodeContext = new CppCodeContext(fileName);

                CurrentFileContext = cppCodeContext;

                cppCodeContext.ProjectFile = projectFile;

                cppCodeContext.Init();

                CurrentFileContext = cppCodeContext;

                AnalyzeCodeContext(cppCodeContext);

                cppCodeContext.WriteSavedNodes();

                cppCodeContext.ProcessResults();

                cppCodeContext.Finalize();
            }
            else
            {
                LogManager.Self.Log($"Missing Cpp File in map: {fileName}");
            }
        }

        private static void ParseAllProjectFiles()
        {
            List<IProjectFile> projectFiles = PenradProject.GetAllProjectFiles().ToList();

            foreach ( IProjectFile projectFile in projectFiles )
            {
                string ext = Path.GetExtension(projectFile.Name).ToLower();

                switch ( ext )
                {
                    case ".cpp":
                        if ( CodeCnt-- > 0 )
                        {
                            ProcessCppFile(projectFile.Name);
                            break;
                        }
                        else
                        {
                            continue;
                        }

                    case ".h":
                        if ( HeaderCnt -- > 0 )
                        {
                            ProcessHeaderFile(projectFile.Name);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                }
            }
        }

        public static void SaveTreeNode(CppParseTreeNodeBase cppParseTreeNode)
        {
            if (CurrentFileContext != null)
            {
                switch (cppParseTreeNode.CppFunctionCatagory)
                {
                    case CppFunctionCatagory.ClassDef:
                        CurrentFileContext.SaveTreeNodes.ClassDefs.Add(cppParseTreeNode as PenWebClassSpecifier);
                        break;

                    case CppFunctionCatagory.MessageMap:
                        CurrentFileContext.SaveTreeNodes.MessageMap.Add(cppParseTreeNode);
                        break;

                    case CppFunctionCatagory.MethodDef:
                        CurrentFileContext.SaveTreeNodes.MethodDefs.Add(cppParseTreeNode);
                        break;

                    case CppFunctionCatagory.MethodCall:
                        CurrentFileContext.SaveTreeNodes.MethodCalls.Add(cppParseTreeNode);
                        break;

                    case CppFunctionCatagory.VariableDef:
                        CurrentFileContext.SaveTreeNodes.VariableDefs.Add(cppParseTreeNode as PenWebDeclaration);
                        break;

                    case CppFunctionCatagory.VariableRef:
                        CurrentFileContext.SaveTreeNodes.VariableRefs.Add(cppParseTreeNode as PenWebQualifiedReference);
                        break;

                    case CppFunctionCatagory.ScreenDef:
                        CurrentFileContext.SaveTreeNodes.ScreenDefs.Add(cppParseTreeNode);
                        break;

                    case CppFunctionCatagory.EnumDef:
                        CurrentFileContext.SaveTreeNodes.EnumDefs.Add(cppParseTreeNode);
                        break;

                    case CppFunctionCatagory.ListDef:
                        CurrentFileContext.SaveTreeNodes.ListDefs.Add(cppParseTreeNode);
                        break;

                    default:
                        CurrentFileContext.SaveTreeNodes.Uncatagorized.Add(cppParseTreeNode);
                        break;
                }

                CurrentFileContext.SaveTreeNodes.All.Add(cppParseTreeNode);
            }
        }

        public static void AnalyzeHeaderContext(CppHeaderContext cppHeaderContext)
        {
            IProjectFile projectFile = cppHeaderContext.ProjectFile;

            using ( TextWriter writer = File.CreateText(CreateAnalyticsFilePath($"{cppHeaderContext.FileName}-h.txt")))
            {
                cppHeaderContext.DumpFile(writer);
            }
        }


        public static void AnalyzeCodeContext(CppCodeContext cppCodeContext)
        {
            IProjectFile projectFile = cppCodeContext.ProjectFile;

            using ( TextWriter writer = File.CreateText(CreateAnalyticsFilePath($"{cppCodeContext.FileName}-cpp.txt")))
            {
                cppCodeContext.DumpFile(writer);
            }
        }

        public static string CreateAnalyticsFilePath(string fileName)
        {
            return Path.Combine(RsAnalyticsDir, fileName);
        }

        public static void DumpJson(string fileName, object jsonObject)
        {
            string dumpPath = Path.Combine(CppCodeAnalysis.RsAnalyticsDir, fileName);

            string jsonData = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            File.WriteAllText(dumpPath, jsonData);
        }

        public static void DumpFileJson(string codeFileName, string jsonFileName, object jsonObject)
        {
            string dumpPath = Path.Combine(CppCodeAnalysis.RsAnalyticsDir, codeFileName);

            Directory.CreateDirectory(dumpPath);

            dumpPath = Path.Combine(dumpPath, jsonFileName);

            string jsonData = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            File.WriteAllText(dumpPath, jsonData);
        }

    }
}