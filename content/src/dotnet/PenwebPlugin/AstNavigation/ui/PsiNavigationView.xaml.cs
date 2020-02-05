using JetBrains.Application.UI.Automation;

namespace PenwebPlugin
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