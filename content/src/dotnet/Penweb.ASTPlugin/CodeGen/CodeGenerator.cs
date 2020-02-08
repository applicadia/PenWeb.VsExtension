using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics.CodeGen
{
    public class CodeGenerator
    {
        public static CodeGenerator Self;

        protected List<string> TypeList { get; set; } = new List<string>();

        protected TextWriter SwitchWriter { get; set; }
        protected TextWriter ClassWriter { get; set; }

        private CodeGenerator()
        {
            Self = this;

            this.LoadList();
        }

        private void LoadList()
        {
            string typeFilePath = Path.Combine(CppCodeAnalysis.RsAnalyticsDir, "TreeTypes.json");

            if ( File.Exists( typeFilePath))
            {
                string jsonData = File.ReadAllText(typeFilePath);

                this.TypeList = JsonConvert.DeserializeObject<List<string>>(jsonData);
            }
        }

        public void GenerateCode()
        {
            this.TypeList = CppParseTreeNodeFactory.Self.NodeTypeMap.Values.ToList();

            string switchFilePath = Path.Combine(CppCodeAnalysis.RsAnalyticsDir, "SwitchCode.cs");
            this.SwitchWriter = File.CreateText(switchFilePath);

            string classFilePath = Path.Combine(CppCodeAnalysis.RsAnalyticsDir, "ClassCode.cs");
            this.ClassWriter = File.CreateText(classFilePath);

            foreach ( string type in this.TypeList )
            {
                this.GenClassCodeForType(type);
                this.GenSwitchCodeForType(type);

            }

            this.SwitchWriter.Flush();
            this.SwitchWriter.Close();

            this.ClassWriter.Flush();
            this.ClassWriter.Close();
        }

        protected void GenClassCodeForType( string typeStr )
        {
            string[] pathAr = typeStr.Split('.');
            string className = pathAr.Last();

            this.ClassWriter.WriteLine($"    public class PenWeb{className} : CppParseTreeNodeBase");
            this.ClassWriter.WriteLine("    {");
            this.ClassWriter.WriteLine($"        public PenWeb{className}( {typeStr} treeNode ) : base(treeNode)");
            this.ClassWriter.WriteLine("        {");
            this.ClassWriter.WriteLine("        }");
            this.ClassWriter.WriteLine("    }");
            this.ClassWriter.WriteLine("");
        }

        protected void GenSwitchCodeForType( string typeStr )
        {
            string[] pathAr = typeStr.Split('.');
            string className = pathAr.Last() + ":";

            this.SwitchWriter.WriteLine($"                case {typeStr} penWeb{className,-80} return new PenWeb{className}( penWeb{className} );");
        }

        public static void Start()
        {
            new CodeGenerator();
        }
    }
}
