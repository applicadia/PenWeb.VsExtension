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

    public static class CppParseManager
    {
        public static string RsAnalyticsDir { get; } = @"c:\PenGit2\RsAnalytics";

        public static IProject PenradProject { get; set; }

        public static SortedDictionary<string, CppCodeContext>   PenradCppFiles    { get; } = new SortedDictionary<string, CppCodeContext>();
        public static SortedDictionary<string, CppHeaderContext> PenradHeaderFiles { get; } = new SortedDictionary<string, CppHeaderContext>();

        public static SortedDictionary<string,IProjectFile> CppFileMap { get;  } = new SortedDictionary<string, IProjectFile>();
        public static SortedDictionary<string,IProjectFile> HeaderFileMap { get;  } = new SortedDictionary<string, IProjectFile>();

        public static IProperty<IEnumerable<string>> ReferencedElementsNamesList { get; set; }
        public static IProperty<int> SelectedReferencedElement { get; set; }

        private static int HeaderCnt = 1000;
        private static int CodeCnt   = 1000;

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
                    WidgetTypeFinder.WriteSchemaAnalytics();

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
                CppHeaderContext cppHeaderContext = ProcessHeaderFile(cppDialogEntry.HeaderFile, cppDialogEntry.DialogClassName);

                if (cppHeaderContext != null)
                {
                    ProcessCppFile(cppHeaderContext, cppDialogEntry.CodeFile, cppDialogEntry.DialogClassName);
                }
            }
        }

        private static CppHeaderContext ProcessHeaderFile(string fileName, string dialogClassName)
        {
            if (HeaderFileMap.ContainsKey(fileName.ToLower()))
            {
                IProjectFile projectFile = HeaderFileMap[fileName.ToLower()];

                CppHeaderContext cppHeaderContext = new CppHeaderContext(projectFile.Name, dialogClassName);

                CurrentFileContext = cppHeaderContext;

                cppHeaderContext.ProjectFile = projectFile;

                cppHeaderContext.Init();

                CurrentFileContext = cppHeaderContext;

                AnalyzeHeaderContext(cppHeaderContext);

                cppHeaderContext.WriteSavedNodes();

                cppHeaderContext.ProcessResults();

                cppHeaderContext.Finalize();

                return cppHeaderContext;
            }
            else
            {
                LogManager.Self.Log($"Missing Header File in map: {fileName}");
                return null;
            }
        }

        private static void ProcessCppFile(CppHeaderContext cppHeaderContext, string fileName, string dialogClassName)
        {
            if (CppFileMap.ContainsKey(fileName.ToLower()))
            {
                IProjectFile projectFile = CppFileMap[fileName.ToLower()];

                CppCodeContext cppCodeContext = new CppCodeContext(fileName, dialogClassName);

                cppCodeContext.CppHeaderContext = cppHeaderContext;
                cppHeaderContext.CppCodeContext = cppCodeContext;
                cppCodeContext.WidgetTypeFinder = new WidgetTypeFinder(cppCodeContext, cppHeaderContext);

                CurrentFileContext = cppCodeContext;

                cppCodeContext.ProjectFile = projectFile;

                cppCodeContext.Init();

                CurrentFileContext = cppCodeContext;

                AnalyzeCodeContext(cppCodeContext);

                cppCodeContext.WriteSavedNodes();

                cppCodeContext.ProcessResults();

                cppCodeContext.Finalize();

                cppCodeContext.SaveClassInfo();

                cppCodeContext.WidgetTypeFinder.DoAnalytics();
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
                            //ProcessCppFile(projectFile.Name);
                            break;
                        }
                        else
                        {
                            continue;
                        }

                    case ".h":
                        if ( HeaderCnt -- > 0 )
                        {
                            //ProcessHeaderFile(projectFile.Name);
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
                        CurrentFileContext.ParseResults.ClassDefs.Add(cppParseTreeNode as PenWebClassSpecifier);
                        break;

                    case CppFunctionCatagory.MessageMap:
                        CurrentFileContext.ParseResults.MessageMap.Add(cppParseTreeNode);
                        break;

                    case CppFunctionCatagory.MethodDef:
                        CurrentFileContext.ParseResults.MethodDefs.Add(cppParseTreeNode);
                        break;

                    case CppFunctionCatagory.MethodCall:
                        CurrentFileContext.ParseResults.MethodCalls.Add(cppParseTreeNode as PenWebCallExpression);
                        break;

                    case CppFunctionCatagory.DDXCall:
                        CurrentFileContext.ParseResults.DDxCalls.Add(cppParseTreeNode as PenWebCallExpression);
                        break;

                    case CppFunctionCatagory.VariableDef:
                        CurrentFileContext.ParseResults.VariableDefs.Add(cppParseTreeNode as PenWebDeclaration);
                        break;

                    case CppFunctionCatagory.VariableRef:
                        CurrentFileContext.ParseResults.VariableRefs.Add(cppParseTreeNode as PenWebQualifiedReference);
                        break;

                    case CppFunctionCatagory.ScreenDef:
                        CurrentFileContext.ParseResults.ScreenDefs.Add(cppParseTreeNode);
                        break;

                    case CppFunctionCatagory.EnumDef:
                        CurrentFileContext.ParseResults.EnumDefs.Add(cppParseTreeNode);
                        break;

                    case CppFunctionCatagory.ListDef:
                        CurrentFileContext.ParseResults.ListDefs.Add(cppParseTreeNode);
                        break;

                    default:
                        CurrentFileContext.ParseResults.Uncatagorized.Add(cppParseTreeNode);
                        break;
                }

                CurrentFileContext.ParseResults.All.Add(cppParseTreeNode);
            }
        }

        public static void AnalyzeHeaderContext(CppHeaderContext cppHeaderContext)
        {
            IProjectFile projectFile = cppHeaderContext.ProjectFile;

            using ( TextWriter writer = File.CreateText(CreateAnalyticsFilePath(cppHeaderContext.DialogClassName, $"{cppHeaderContext.FileName}-h.txt")))
            {
                cppHeaderContext.DumpFile(writer);
            }
        }

        public static void AnalyzeCodeContext(CppCodeContext cppCodeContext)
        {
            IProjectFile projectFile = cppCodeContext.ProjectFile;

            using ( TextWriter writer = File.CreateText(CreateAnalyticsFilePath(cppCodeContext.DialogClassName, $"{cppCodeContext.FileName}-cpp.txt")))
            {
                cppCodeContext.DumpFile(writer);
            }
        }

        public static string CreateAnalyticsFilePath(string fullFileName)
        {
            string path = Path.Combine(RsAnalyticsDir, fullFileName);
            return path;
        }

        public static string CreateAnalyticsFilePath(string dialogClassName, string fullFileName)
        {
            string path = Path.Combine(RsAnalyticsDir, dialogClassName);
            Directory.CreateDirectory(path);
            path = Path.Combine(RsAnalyticsDir, dialogClassName, fullFileName);

            return path;
        }

        public static void DumpJson(string fileName, object jsonObject)
        {
            string dumpPath = Path.Combine(CppParseManager.RsAnalyticsDir, fileName);

            string jsonData = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            File.WriteAllText(dumpPath, jsonData);
        }

        public static void DumpFileJson(string codeFileName, string jsonFileName, object jsonObject)
        {
            string dumpPath = Path.Combine(CppParseManager.RsAnalyticsDir, codeFileName);

            Directory.CreateDirectory(dumpPath);

            dumpPath = Path.Combine(dumpPath, jsonFileName);

            string jsonData = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            File.WriteAllText(dumpPath, jsonData);
        }
    }
}