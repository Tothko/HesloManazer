using VisiWin.ApplicationFramework;

namespace HMI
{
    [ExportView("NumericTouchpadView")]
    public partial class NumericTouchpadView :  VisiWin.Controls.View
    {
        public NumericTouchpadView()
        {
            this.InitializeComponent();
        }
    }
}