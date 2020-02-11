using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Penweb.CodeAnalytics
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn,IsReference=false)]
    public class CppDialogEntry
    {
        [JsonProperty] public string CodeFile   { get; set; }

        [JsonProperty] public string HeaderFile { get; set; }
    }

    public class PenradCppManager
    {
        public static PenradCppManager Self;

        public static bool NewMammoOnly = true;

        public string PenWebData = @"c:\PenGit2\Mit\PenWebData";
        public string CppResourceDirectory;

        public SortedDictionary<string,CppDialogEntry> DialogMap { get; set; } = new SortedDictionary<string, CppDialogEntry>();

        private PenradCppManager()
        {
            Self = this;

            this.CppResourceDirectory  = this.CheckDir( Path.Combine(PenWebData, @"Resources", "PenradCpp"));

            if (NewMammoOnly)
            {
                this.AddNewMammoOnly();
            }
            else
            {
                this.LoadDialogMap();
            }
        }

        protected string CheckDir(string path)
        {
            if ( !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        protected void AddNewMammoOnly()
        {
            this.DialogMap.Add("CNewMammogramDlg", new CppDialogEntry {CodeFile = "NewMammogramDlg.cpp", HeaderFile = "NewMammogramDlg.H"});
        }

        protected void LoadDialogMap()
        {
            string filePath = Path.Combine(this.CppResourceDirectory, "DialogMap.json");

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);

                this.DialogMap = JsonConvert.DeserializeObject<SortedDictionary<string, CppDialogEntry>>(jsonData);

            }
        }

        public static void Start()
        {
            new PenradCppManager();
        }
    }
}
