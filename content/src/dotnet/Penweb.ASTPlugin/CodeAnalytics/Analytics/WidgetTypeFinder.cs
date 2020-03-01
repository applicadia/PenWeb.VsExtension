using Newtonsoft.Json;
using PenWeb.ClientAPI;
using System;
using System.Collections.Generic;
using System.IO;

namespace Penweb.CodeAnalytics
{
    /*
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
    */

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

        //public List<WidgetTypeBinding> WidgetTypeBindings { get; } = new List<WidgetTypeBinding>();

        public Dictionary<string, WidgetTypeBinding> WidgetTypeBindingsMap { get; } = new Dictionary<string, WidgetTypeBinding>();

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
                        this.CreateWidgetTypeFromDDX(methodCall);
                    }
                }
            }
        }

        private void CreateWidgetTypeFromDDX(PenWebCallExpression methodCall)
        {
            string typeName = methodCall.ParameterList[1].TypeName;

            string resourceLabel = null;

            int resourceId = -1;
            if (methodCall.MacroReferences.Count == 1)
            {
                resourceLabel = methodCall.MacroReferences[0];
                /*
                ResourceIdContext resourceIdContext = CppResourceManager.Self.GetResourceIdContextByLabel(resourceLabel);

                if (resourceIdContext != null)
                {

                }
                else
                {
                    Console.WriteLine($"Missing Resource Id Reference {resourceLabel}");
                    return;
                }
                */
            }
            else
            {
                Console.WriteLine($"Missing Macro Reference");
                return;
            }

            WidgetTypeBinding widgetTypeBinding = new WidgetTypeBinding();

            widgetTypeBinding.FileName = this.CppCodeContext.FileName;
            widgetTypeBinding.LineNumber = methodCall.Location.StartLine;
            widgetTypeBinding.ResourceLabel = resourceLabel;
            //widgetTypeBinding.ResourceId = resourceId;
            widgetTypeBinding.PenWebWidgetType = PenWebWidgetType.UnknownWidget;

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
                    return;

                case "DDX_PenradDate":
                    widgetTypeBinding.PenWebWidgetType = PenWebWidgetType.DateTimeWidget;
                    break;

                case "DDX_Control":
                    switch (typeName)
                    {
                        case "CDateEdit":
                            widgetTypeBinding.PenWebWidgetType = PenWebWidgetType.DateTimeWidget;
                            break;

                        case "CPhoneEdit":
                        case "CPhoneExtEdit":
                        case "CSsnEdit":
                            widgetTypeBinding.PenWebWidgetType = PenWebWidgetType.FormattedWidget;
                            break;

                        case "CExListBox":
                        case "CListBox":
                            widgetTypeBinding.PenWebWidgetType = PenWebWidgetType.ListboxWidget;
                            break;

                        case "CSliderCtrl":
                            widgetTypeBinding.PenWebWidgetType = PenWebWidgetType.SliderWidget;
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
                            widgetTypeBinding.PenWebWidgetType = PenWebWidgetType.GridWidget;
                            break;

                        case "Microsoft::VisualC::MFC::CWinFormsControl<CrystalDecisions::Windows::Forms::CrystalReportViewer>": 
                            widgetTypeBinding.PenWebWidgetType = PenWebWidgetType.CrystalViewer;
                            break;
                            
                        case "Microsoft::VisualC::MFC::CWinFormsControl<Penrad::PenCsLib::Browser>":
                            widgetTypeBinding.PenWebWidgetType = PenWebWidgetType.HtmlViewer;
                            break;

                        case "Microsoft::VisualC::MFC::CWinFormsControl<System::Windows::Forms::ListView>":
                            widgetTypeBinding.PenWebWidgetType = PenWebWidgetType.ListboxWidget;
                            break;

                        case "Microsoft::VisualC::MFC::CWinFormsControl<TXTextControl::TextControl>":
                            widgetTypeBinding.PenWebWidgetType = PenWebWidgetType.TxTextControl;
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

            if (widgetTypeBinding.PenWebWidgetType != PenWebWidgetType.UnknownWidget)
            {
                this.RegisterTypeBinding(widgetTypeBinding);
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

                default:
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

            if (resourceIdContext == null)
            {
                if (methodCall.MacroReferences.Count > 0)
                {
                    resourceLabel = methodCall.MacroReferences[0];
                }
                else
                {
                }
            }

            WidgetTypeBinding widgetTypeBinding = new WidgetTypeBinding();

            widgetTypeBinding.FileName = this.CppCodeContext.FileName;
            widgetTypeBinding.LineNumber = methodCall.Location.StartLine;
            widgetTypeBinding.PenWebWidgetType = penWebWidgetType;
            widgetTypeBinding.ResourceLabel = resourceLabel;

            if (!String.IsNullOrWhiteSpace(resourceLabel))
            {
                this.RegisterTypeBinding(widgetTypeBinding);
            }

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

                //widgetTypeBinding.ResourceId = resourceIdContext.ResourceId;

                this.RegisterTypeBinding(widgetTypeBinding);

                //this.WidgetTypeBindings.Add(widgetTypeBinding);
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

        private void RegisterTypeBinding(WidgetTypeBinding widgetTypeBinding)
        {
            if (String.IsNullOrWhiteSpace(widgetTypeBinding.ResourceLabel))
            {
                return;
            }

            if (this.WidgetTypeBindingsMap.ContainsKey(widgetTypeBinding.ResourceLabel))
            {
                WidgetTypeBinding existingBinding = this.WidgetTypeBindingsMap[widgetTypeBinding.ResourceLabel];

                if (widgetTypeBinding.PenWebWidgetType == existingBinding.PenWebWidgetType)
                {
                    return;
                }
                else
                {
                    Console.WriteLine($"Mismatched Binding");
                }
            }
            else
            {
                this.WidgetTypeBindingsMap.Add(widgetTypeBinding.ResourceLabel, widgetTypeBinding);

                CppResultsManager.Self.AddWidgetTypeBinding(this.CppCodeContext.DialogClassName, this.CppCodeContext.FileName, widgetTypeBinding);
            }
        }

        public void WriteFileAnalytics()
        {
            string filePath = CppParseManager.CreateAnalyticsFilePath(this.CppCodeContext.DialogClassName, "WidgetTypeBindings.json");

            string jsonData = JsonConvert.SerializeObject(this.WidgetTypeBindingsMap, Formatting.Indented);

            File.WriteAllText(filePath, jsonData);
        }

        public static void WriteAnalytics()
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

            SortedDictionary<string, DDXMethods> dDXMethodsSchemaSummary = new SortedDictionary<string, DDXMethods>();

            foreach (var dDXMethodSchemaItem in DDXMethods)
            {
                var newSummary = new DDXMethods() {MethodName = dDXMethodSchemaItem.Value.MethodName};

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
