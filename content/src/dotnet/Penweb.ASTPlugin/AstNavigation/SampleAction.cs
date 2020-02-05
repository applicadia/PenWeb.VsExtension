using JetBrains.Application.DataContext;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Application.UI.ActionSystem.ActionsRevised.Menu;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.Util;

namespace PenWeb.ASTPlugin
{
    //[Action("SampleAction", "Do Something")]
    public class SampleAction : IActionWithExecuteRequirement, IExecutableAction
    {
        public IActionRequirement GetRequirement(IDataContext dataContext)
        {
            IActionRequirement actionRequirement = CommitAllDocumentsRequirement.TryGetInstance(dataContext);

            return actionRequirement;
        }

        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            return true;
        }

        public virtual void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            MessageBox.ShowInfo("Info!");
        }
    }
}