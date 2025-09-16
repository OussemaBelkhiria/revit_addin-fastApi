using Autodesk.Revit.UI;
using RevitAddin;

namespace RevitAddinWPF
{
    public class DockablePaneProvider : IDockablePaneProvider
    {
        private HomeworkUI control;

        public DockablePaneProvider()
        {
            control = new HomeworkUI();
            
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = control;
            data.InitialState = new DockablePaneState();
            data.InitialState.DockPosition = DockPosition.Right;
            data.VisibleByDefault = false;
        }
    }
}
