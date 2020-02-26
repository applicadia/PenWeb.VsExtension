using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    public enum PenWebWidgetType
    {
        Skip = 0,
        PenWebRootWidget = 1,
        PageWidget = 2,
        PanelWidget = 3,
        MenuWidget = 4,
        LabelWidget = 5,
        TextWidget = 6,
        CheckboxWidget = 7,
        ComboBoxWidget = 8,
        ListboxWidget = 9,
        GroupBoxWidget = 10,
        DateTimeWidget = 11,
        NumericWidget = 12,
        ButtonWidget = 13,
        RadioButtonWidget = 14,
        GridWidget = 15,
        IconWidget = 16,
//        ScreenWidget = 17,
        KeyboardWidget = 18,
        FormattedWidget = 19,
        SliderWidget = 20,
        BreastWidget = 21,
        FieldSetWidget = 22,

        BlockTextWidget = 23,

        TopHeaderWidget = 24,
        ScreenRootWidget = 25,
        ClockfaceWidget = 26,

        UnknownWidget = 99
    }
                  

    [JsonObject(MemberSerialization=MemberSerialization.OptIn,IsReference =false)]
    public class DDXMethodsSchemaSummary
    {
        [JsonProperty] public string MethodName { get; set;  }
        [JsonProperty] public SortedDictionary<string, int> TypeList { get; } = new SortedDictionary<string, int>();
    }

    [JsonObject(MemberSerialization=MemberSerialization.OptIn,IsReference =false)]
    public class DDXMethodSchema
    {
        [JsonProperty] public string MethodName { get; set; }
        [JsonProperty] public SortedDictionary<string, int> TypeList { get; set; } = new SortedDictionary<string, int>();

        [JsonProperty] public SortedDictionary<string, List<int>> DialogList { get; set; } = new SortedDictionary<string, List<int>>();
    }


    [JsonObject(MemberSerialization=MemberSerialization.OptIn,IsReference =false)]
    public class ClassMethodSchema
    {
        [JsonProperty] public string ClassName { get; set; }
        [JsonProperty] public SortedDictionary<string, int> MethodList { get; set; } = new SortedDictionary<string, int>();

        [JsonProperty] public SortedDictionary<string, int> DialogList { get; set; } = new SortedDictionary<string, int>();
    }

    public class WidgetTypeFinder
    {
        public static SortedDictionary<string, ClassMethodSchema> ClassMethodSchemaMap = new SortedDictionary<string, ClassMethodSchema>();
        public static HashSet<string> HasCreateList = new HashSet<string>();
        public static List<WidgetTypeBinding> IncompleteWidgetTypeBindings { get; } = new List<WidgetTypeBinding>();

        public static SortedDictionary<string, int> HasBindings           = new SortedDictionary<string, int>();
        public static SortedDictionary<string, int> HasIncompleteBindings = new SortedDictionary<string, int>();

        public static SortedDictionary<string, DDXMethodSchema> DDXMethods = new SortedDictionary<string, DDXMethodSchema>();

        private CppCodeContext   CppCodeContext   { get; set; }
        private CppHeaderContext CppHeaderContext { get; set; }

        private bool HasBinding = false;
        private bool HasIncompleteBinding = false;

        public List<WidgetTypeBinding> WidgetTypeBindings { get; } = new List<WidgetTypeBinding>();

        public WidgetTypeFinder(CppCodeContext cppCodeContext, CppHeaderContext cppHeaderContext)
        {
            this.CppCodeContext   = cppCodeContext;
            this.CppHeaderContext = cppHeaderContext;
        }

        public void DoAnalytics()
        {
            this.ScanForCreate();
            this.ScanDDX();
            this.WriteFileAnalytics();
        }

        private void ScanDDX()
        {
            foreach (var methodCall in this.CppCodeContext.ParseResults.DDxCalls)
            {
                if (!String.IsNullOrWhiteSpace(methodCall.Method))
                {
                    if (methodCall.MacroReferences.Count == 0 || methodCall.ParameterList.Count == 0)
                    {
                        continue;

                    }
                    else
                    {
                        this.CatalogWidgetTypeFromDDX(methodCall);
                    }
                }
            }
        }

        private void CreateWidgetTypeFromDDX(PenWebCallExpression methodCall)
        {
            string typeName = methodCall.ParameterList[1].TypeName;

            switch (methodCall.Method)
            {
                case "DDX_CBIndex": 
                case "DDX_CBString": 
                case "DDX_CBStringExact": 
                case "DDX_LBIndex": 
                case "DDX_LBString":
                case "DDX_LBStringExact":
                case "DDX_Radio": 
                case "DDX_Text":
                case "DDX_Check": 
                    break;

                case "DDX_PenradDate":
                    break;

                case "DDX_Control":
                    switch (typeName)
                    {
                        case "CDateEdit":
                            break;

                        case "CPhoneEdit":
                        case "CPhoneExtEdit":
                        case "CSsnEdit":

                            break;

                        case "CExListBox":
                        case "CListBox":
                        case "CSliderCtrl":
                            break;

                        case "CAnimateCtrl":
                        case "CButton":
                        case "CButton[4]":
                        case "CCalDayStatic":
                        case "CComboBox":
                        case "CDibWnd":
                        case "CEdit":
                        case "CEditWithWheelNotify":
                        case "CSpinButtonCtrl":
                        case "CStatic":
                        case "CStatic[4]":
                            break;
                    }

                    break;

                case "DDX_ManagedControl":
                    switch (typeName)
                    {
                        case "Microsoft::VisualC::MFC::CWinFormsControl<CDataGridView>":
                            break;

                        case "Microsoft::VisualC::MFC::CWinFormsControl<CrystalDecisions::Windows::Forms::CrystalReportViewer>": 
                            break;
                            
                        case "Microsoft::VisualC::MFC::CWinFormsControl<Penrad::PenCsLib::Browser>":
                            break;

                        case "Microsoft::VisualC::MFC::CWinFormsControl<System::Windows::Forms::ListView>":
                            break;

                        case "Microsoft::VisualC::MFC::CWinFormsControl<TXTextControl::TextControl>":
                            break;

                        case "Microsoft::VisualC::MFC::CWinFormsControl<Penrad::PenCsLib::ZoomPicBox>":
                        case "Microsoft::VisualC::MFC::CWinFormsControl<System::Windows::Forms::Button>":
                        case "Microsoft::VisualC::MFC::CWinFormsControl<System::Windows::Forms::ComboBox>":
                        case "Microsoft::VisualC::MFC::CWinFormsControl<System::Windows::Forms::Label>":
                        case "Microsoft::VisualC::MFC::CWinFormsControl<System::Windows::Forms::PictureBox>":
                        case "Microsoft::VisualC::MFC::CWinFormsControl<System::Windows::Forms::TextBox>":
                        case "Microsoft::VisualC::MFC::CWinFormsControl<System::Windows::Forms::TrackBar>":
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

        private void CatalogWidgetTypeFromDDX(PenWebCallExpression methodCall)
        {
            string typeName = methodCall.ParameterList[1].TypeName;

            DDXMethodSchema ddXMethodSchema;

            if (DDXMethods.ContainsKey(methodCall.Method))
            {
                ddXMethodSchema = DDXMethods[methodCall.Method];
            }
            else
            {
                ddXMethodSchema = new DDXMethodSchema()
                {
                    MethodName = methodCall.Method
                };
                
                DDXMethods.Add(methodCall.Method, ddXMethodSchema);
            }

            if (ddXMethodSchema.TypeList.ContainsKey(typeName))
            {
                ddXMethodSchema.TypeList[typeName]++;
            }
            else
            {
                ddXMethodSchema.TypeList.Add(typeName, 1);
            }

            if (ddXMethodSchema.DialogList.ContainsKey(this.CppCodeContext.DialogClassName))
            {
                ddXMethodSchema.DialogList[this.CppCodeContext.DialogClassName].Add(methodCall.Location.StartLine);
            }
            else
            {
                ddXMethodSchema.DialogList.Add(this.CppCodeContext.DialogClassName, new List<int>(){ methodCall.Location.StartLine });
            }
        }

        private void ScanForCreate()
        {
            foreach (var methodCall in this.CppCodeContext.ParseResults.MethodCalls)
            {
                if (!String.IsNullOrWhiteSpace(methodCall.Class)&&!String.IsNullOrWhiteSpace(methodCall.Method))
                {
                    ClassMethodSchema classMethodSchema;
                    if (ClassMethodSchemaMap.ContainsKey(methodCall.Class))
                    {
                        classMethodSchema = ClassMethodSchemaMap[methodCall.Class];
                    }
                    else
                    {
                        classMethodSchema = new ClassMethodSchema() {ClassName = methodCall.Class};
                        ClassMethodSchemaMap.Add(methodCall.Class, classMethodSchema);
                    }

                    if (classMethodSchema.MethodList.ContainsKey(methodCall.Method))
                    {
                        classMethodSchema.MethodList[methodCall.Method]++;
                    }
                    else
                    {
                        classMethodSchema.MethodList.Add(methodCall.Method, 1);
                    }

                    if (classMethodSchema.DialogList.ContainsKey(this.CppCodeContext.DialogClassName))
                    {
                        classMethodSchema.DialogList[this.CppCodeContext.DialogClassName]++;
                    }
                    else
                    {
                        classMethodSchema.DialogList.Add(this.CppCodeContext.DialogClassName, 1);
                    }

                    switch (methodCall.Method.Trim().ToLower())
                    {
                        case "create":
                            this.ProcessCreate(methodCall);
                            break;
                    }
                }
            }
        }

        private void ProcessCreate(PenWebCallExpression methodCall)
        {
            HasCreateList.Add(methodCall.Class);

            switch (methodCall.Class)
            {
                case "CHorizontalSlider":
                case "CVerticalSlider":
                    this.CreateWidgetTypeBinding(methodCall, PenWebWidgetType.SliderWidget);
                    break;
                          
                case "CBreastRightSideWnd":
                case "CBreastRightFrontWnd":
                case "CBreastLeftFrontWnd":
                case "CBreastLeftSideWnd":    
                case "CBreastWnd":    
                    this.CreateWidgetTypeBinding(methodCall, PenWebWidgetType.BreastWidget);
                    break;

                case "CLungMaskedAbnormalityWnd":
                    break;

                case "CHitRegion":
                    break;

                case "CMaskedAbnormalityWnd":
                    break;

                case "CGiMaskedAbnormalityWnd":
                    break;

                case "CDictaPhoneDlg":
                    break;


                case "CClock":
                    break;

                case "CDraggerStatic":
                    break;

                case "CStatic":
                    break;

                case "CButton": 
                    break;

                case "CPenradTextWnd":
                    break;
                
                case "CPenradLogoWnd":
                    break;
            }
        }

        private void CreateWidgetTypeBinding(PenWebCallExpression methodCall, PenWebWidgetType penWebWidgetType)
        {
            string resourceLabel = null;

            ResourceIdContext resourceIdContext = null;

            switch (methodCall.ResourceIdContexts.Count)
            {
                case 0:
                    break;

                case 1:
                    resourceIdContext = methodCall.ResourceIdContexts[0];
                    break;

                default:
                    resourceIdContext = methodCall.ResourceIdContexts[0];
                    break;
            }

            WidgetTypeBinding widgetTypeBinding = new WidgetTypeBinding();

            widgetTypeBinding.FileName = this.CppCodeContext.FileName;
            widgetTypeBinding.LineNumber = methodCall.Location.StartLine;
            widgetTypeBinding.PenWebWidgetType = penWebWidgetType;
            widgetTypeBinding.ResourceLabel = resourceLabel;

            if (resourceIdContext == null)
            {
                IncompleteWidgetTypeBindings.Add(widgetTypeBinding);

                if (!HasIncompleteBindings.ContainsKey(this.CppCodeContext.DialogClassName))
                {
                    HasIncompleteBindings.Add(this.CppCodeContext.DialogClassName, 1);
                    HasIncompleteBinding = true;
                }
                else
                {
                    HasIncompleteBindings[this.CppCodeContext.DialogClassName]++;
                }

                return;
            }

            if (resourceIdContext != null)
            {
                if (!HasBinding)
                {
                    HasBindings.Add(this.CppCodeContext.DialogClassName, 1);
                    HasBinding = true;
                }
                else
                {
                    HasBindings[this.CppCodeContext.DialogClassName]++;
                }

                widgetTypeBinding.ResourceId = resourceIdContext.ResourceId;
                this.WidgetTypeBindings.Add(widgetTypeBinding);
            }
            else
            {
                IncompleteWidgetTypeBindings.Add(widgetTypeBinding);

                if (!HasIncompleteBinding)
                {
                    HasIncompleteBindings.Add(this.CppCodeContext.DialogClassName, 1);
                    HasIncompleteBinding = true;
                }
                else
                {
                    HasIncompleteBindings[this.CppCodeContext.DialogClassName]++;
                }

                IncompleteWidgetTypeBindings.Add(widgetTypeBinding);
            }
        }

        public void WriteFileAnalytics()
        {
            string filePath = CppParseManager.CreateAnalyticsFilePath(this.CppCodeContext.DialogClassName, "WidgetTypeBindings.json");

            string jsonData = JsonConvert.SerializeObject(this.WidgetTypeBindings, Formatting.Indented);

            File.WriteAllText(filePath, jsonData);
        }

        public static void WriteSchemaAnalytics()
        {
            string filePath = CppParseManager.CreateAnalyticsFilePath("ClassMethodSchemaMap.json");

            string jsonData = JsonConvert.SerializeObject(ClassMethodSchemaMap, Formatting.Indented);

            File.WriteAllText(filePath, jsonData);

            filePath = CppParseManager.CreateAnalyticsFilePath("HasCreateList.json");

            jsonData = JsonConvert.SerializeObject(HasCreateList, Formatting.Indented);

            File.WriteAllText(filePath, jsonData);

            filePath = CppParseManager.CreateAnalyticsFilePath("IncompleteBindings.json");

            jsonData = JsonConvert.SerializeObject(IncompleteWidgetTypeBindings, Formatting.Indented);

            File.WriteAllText(filePath, jsonData);

            filePath = CppParseManager.CreateAnalyticsFilePath("HasBindings.json");

            jsonData = JsonConvert.SerializeObject(HasBindings, Formatting.Indented);

            File.WriteAllText(filePath, jsonData);

            filePath = CppParseManager.CreateAnalyticsFilePath("DDXMethods.json");

            jsonData = JsonConvert.SerializeObject(DDXMethods, Formatting.Indented);

            File.WriteAllText(filePath, jsonData);

            SortedDictionary<string, DDXMethodsSchemaSummary> dDXMethodsSchemaSummary = new SortedDictionary<string, DDXMethodsSchemaSummary>();

            foreach (var dDXMethodSchemaItem in DDXMethods)
            {
                var newSummary = new DDXMethodsSchemaSummary() {MethodName = dDXMethodSchemaItem.Value.MethodName};

                foreach (var typeItem in dDXMethodSchemaItem.Value.TypeList)
                {
                    newSummary.TypeList.Add(typeItem.Key, typeItem.Value);
                }

                dDXMethodsSchemaSummary.Add(dDXMethodSchemaItem.Key, newSummary);
            }

            filePath = CppParseManager.CreateAnalyticsFilePath("DDXMethodsSummary.json");

            jsonData = JsonConvert.SerializeObject(dDXMethodsSchemaSummary, Formatting.Indented);

            File.WriteAllText(filePath, jsonData);
        }
    }
}
