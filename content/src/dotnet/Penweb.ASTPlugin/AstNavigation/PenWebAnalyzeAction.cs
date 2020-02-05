using JetBrains.Application.DataContext;
using JetBrains.Application.Settings.Implementation;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.Actions.MenuGroups;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Application.UI.ActionSystem.ActionsRevised.Menu;
using JetBrains.Application.UI.Components.Theming;
using JetBrains.Application.UI.ToolWindowManagement;
using JetBrains.DataFlow;
using JetBrains.Lifetimes;

namespace PenWeb.ASTPlugin
{
    [Action("ActionPenWebAnalyze", "PenWeb Analyze", Id = 543211)]
    public class PenWebAnalyzeAction : SampleAction, IInsertLast<MainMenuFeaturesGroup>
    {
        public override void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            var lifetime = context.GetComponent<Lifetime>();
            var settingsStore = context.GetComponent<SettingsStore>();

            CppCodeAnalysis.DoAnalytics(lifetime);
        }
    }
}