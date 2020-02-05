using JetBrains.Application.DataContext;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Util;

namespace PenWeb.ASTPlugin
{
    [Action("ActionShowMessageBox", "Show message box", Id = 543210)]
    public class ShowMessageBoxAction : SampleAction, IExecutableAction
    {
        public override void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            var solution = context.GetData(JetBrains.ProjectModel.DataContext.ProjectModelDataConstants.SOLUTION);
            MessageBox.ShowInfo(solution?.SolutionFile != null
                ? $"{solution.SolutionFile?.Name} solution is opened"
                : "No solution is opened");
        }
    }
}