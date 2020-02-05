using JetBrains.Application.UI.Automation;

namespace PenWeb.ASTPlugin
{
    public partial class PsiNavigationView : IView<PsiNavigationViewModel>
    {
        public string Name => "PSI Navigation";

        public PsiNavigationView()
        {
            //$InitializeComponent();
        }
    }
}