using JetBrains.Application.UI.Automation;

namespace PenWebVSPlugin
{
    public partial class PsiNavigationView : IView<PsiNavigationViewModel>
    {
        public new string Name => "PSI Navigation";

        public PsiNavigationView()
        {
            InitializeComponent();
        }
    }
}