using JetBrains.Application.UI.Automation;

namespace PenWebVSPlugin
{
    [View]
    public partial class OptionsPageView : IView<OptionsPageViewModel>
    {
        public new string Name => "Options";

        public OptionsPageView()
        {
            InitializeComponent();
        }
    }
}
