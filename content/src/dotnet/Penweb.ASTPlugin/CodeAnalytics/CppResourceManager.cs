using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    public class ResourceIdContext
    {
        [JsonProperty] public int ResourceId { get; set; }
        [JsonProperty] public string ResourceLabel { get; set; }

        public ResourceIdContext(int resourceId, string resourceLabel)
        {
            this.ResourceId = resourceId;
            this.ResourceLabel = resourceLabel;
        }
    }

    public class CppResourceManager
    {
        public static CppResourceManager Self;

        private List<string> MessageMapMacroList = new List<string>
        {
            "ON_MESSAGE",
            "ON_COMMAND",
            "ON_NOTIFY",
            "ON_CONTROL",
            "ON_NOTIFY_EX",
            "ON_BN_CLICKED",
            "ON_BN_DOUBLECLICKED",

            "ON_CBN_SELCHANGE",
            "ON_CBN_DROPDOWN",
            "ON_CBN_CLOSEUP",

            "ON_LBN_DBLCLK",
            "ON_LBN_SELCHANGE",
            "ON_LBN_SETFOCUS",
            "ON_LBN_KILLFOCUS",

            "ON_EN_CHANGE",
            "ON_EN_KILLFOCUS",
            "ON_EN_SETFOCUS",

            "ON_STN_CLICKED",

            "ON_THREAD_MESSAGE",
            "ON_CONTROL_REFLECT",


            "ON_WM_DRAWITEM",
            "ON_WM_MEASUREITEM",
            "ON_WM_CTLCOLOR",
            "ON_WM_MEASUREITEM",
            "ON_WM_CREATE",
            "ON_WM_CLOSE",
            "ON_WM_SETFOCUS",
            "ON_WM_ACTIVATE",
            "ON_WM_ACTIVATEAPP",
            "ON_WM_MOUSEWHEEL",
            "ON_WM_DESTROY",
            "ON_WM_ERASEBKGND",
            "ON_WM_TIMER",
            "ON_WM_LBUTTONDOWN",
            "ON_WM_PAINT",
            "ON_WM_MOUSEMOVE",
            "ON_WM_LBUTTONUP",
            "ON_WM_ENABLE",
            "ON_WM_LBUTTONDBLCLK",
            "ON_WM_RBUTTONDOWN",
            "ON_WM_RBUTTONUP",
            "ON_WM_HSCROLL",
            "ON_WM_MOUSEHOVER",
            "ON_WM_KEYDOWN",
            "ON_WM_KEYUP",
            "ON_NOTIFY_EX_RANGE",
            "ON_WM_MOVE",
            "ON_WM_SIZE",
            "ON_WM_SYSCOMMAND",
            "ON_WM_CHAR",
            "ON_WM_SHOWWINDOW"
        };

        [JsonProperty] public SortedDictionary<int, ResourceIdContext>    ResourceIds { get; }   = new SortedDictionary<int, ResourceIdContext>();

        public HashSet<string> MessageMapMacroHash { get; } = new HashSet<string>();

        public HashSet<string> UnknownMacroNames { get; } = new HashSet<string>();

        public SortedDictionary<string, ResourceIdContext> ResourceNames { get; } = new SortedDictionary<string, ResourceIdContext>();

        public SortedDictionary<string, ResourceIdContext> DialogResourceMap  { get; } = new SortedDictionary<string, ResourceIdContext>();

        private CppResourceManager()
        {
            Self = this;

            this.LoadResourceIdList();

            foreach (string macroName in this.MessageMapMacroList)
            {
                this.MessageMapMacroHash.Add(macroName);
            }
        }

        public void LoadResourceIdList()
        {
            string filePath = System.IO.Path.Combine(PenradCppManager.Self.CppResourceDirectory, "resource.h");

            using ( TextReader reader = File.OpenText( filePath ) )
            {
                string line;

                int lineNumbner = 0;
                while ( (line = reader.ReadLine() ) != null )
                {
                    lineNumbner++;

                    line = line.Trim().Replace("\t", " ");

                    if ( !String.IsNullOrWhiteSpace(line) )
                    {
                        string[] elementsRaw = line.Split(' ');

                        List<string> elements = new List<string>();

                        foreach (string rawElement in elementsRaw)
                        {
                            if (!String.IsNullOrWhiteSpace(rawElement))
                            {
                                elements.Add(rawElement);
                            }
                        }

                        if ( elements.Count == 3 )
                        {
                            int defId = Int32.Parse( elements[2] );
                            string resourceLabel = elements[1];

                            switch ( resourceLabel )
                            {
                                case "IDC_BUTTON_7":
                                    break;

                                case "HSLIDER_ABNORMALITY":
                                    break;
                            }

                            string prefix = resourceLabel.Substring(0, 4);

                            if (prefix == "IDD_")
                            {
                                this.AddDialogResourceId(defId, resourceLabel);
                            }
                            else
                            {
                                this.AddResrouceId(defId, resourceLabel);
                            }
                        }
                        else
                        {
                            LogManager.Self.Log($"resource.h - bad format at [{lineNumbner}] '{line}'");
                        }
                    }
                }
            }
        }

        public ResourceIdContext AddDialogResourceId(int resourceId, string resourceLabel)
        {
            ResourceIdContext resourceIdContext = new ResourceIdContext(resourceId, resourceLabel);

            if ( this.DialogResourceMap.ContainsKey(resourceLabel) )
            {
                //this.DuplicatesIds.Add(resourceId);
            }
            else
            {
                this.DialogResourceMap.Add(resourceLabel, resourceIdContext);
            }

            return resourceIdContext;
        }

        public ResourceIdContext AddResrouceId(int resourceId, string resourceLabel)
        {
            ResourceIdContext resourceIdContext = new ResourceIdContext(resourceId, resourceLabel);

            if ( this.ResourceIds.ContainsKey(resourceId))
            {
                //this.DuplicatesIds.Add(resourceId);
            }
            else
            {
                this.ResourceIds.Add(resourceId, resourceIdContext);
            }

            switch ( resourceLabel)
            {
                case "SSM_CLICK":
                    break;
            }

            if ( this.ResourceNames.ContainsKey(resourceLabel))
            {
                switch ( resourceLabel)
                {
                    case "CMaskedAbnormalityWnd::MSG_MASK_AB_MOVE":
                        break;
                }

                //this.DuplicatesLabels.Add(resourceLabel);
            }
            else
            {
                this.ResourceNames.Add(resourceLabel,resourceIdContext);
            }

            return resourceIdContext;
        }

        public PenWebMacroType GetMacroType(string macroId)
        {
            if (this.MessageMapMacroHash.Contains(macroId))
            {
                return PenWebMacroType.MessageMapEntry;
            }
            else if (this.DialogResourceMap.ContainsKey(macroId))
            {
                return PenWebMacroType.ScreenIdDef;
            }
            else if (this.ResourceNames.ContainsKey(macroId))
            {
                return PenWebMacroType.ResourceId;
            }
            else
            {
                this.UnknownMacroNames.Add(macroId);
                return PenWebMacroType.Unknown;
            }
        }

        public ResourceIdContext GetResourceIdContextById(int resourceId)
        {
            if ( this.ResourceIds.ContainsKey(resourceId))
            {
                return this.ResourceIds[resourceId];
            }
            else
            {
                /*
                if ( !this.MissingResourceIdDefinitions.ContainsKey(resourceId))
                {
                    this.MissingResourceIdDefinitions.Add(resourceId, resourceId);
                }
                */

                return null;
            }
        }

        public ResourceIdContext GetResourceIdContextByLabel(string resourceLabel)
        {
            resourceLabel = resourceLabel.Trim();

            if ( this.ResourceNames.ContainsKey(resourceLabel))
            {
                return this.ResourceNames[resourceLabel];
            }
            else
            {
                /*
                if ( !this.MissingResourceLabelDefinitions.ContainsKey(resourceLabel))
                {
                    this.MissingResourceLabelDefinitions.Add(resourceLabel, resourceLabel);
                }
                */

                return null;
            }
        }

        public static void Start()
        {
            new CppResourceManager();
        }
    }
}
