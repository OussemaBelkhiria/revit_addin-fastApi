using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddinWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RevitAddin
{
    public class AddIn : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            application.CreateRibbonTab("Test");

            RibbonPanel rp = application.CreateRibbonPanel("Test", "Demo");

            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            PushButtonData buttonData = new PushButtonData("cmdOpenUI", "Show Panel", assemblyPath, "RevitAddin.btn_click"); 
            PushButton button = (PushButton) rp.AddItem(buttonData);
         
            Uri imageUri = new Uri("pack://application:,,,/RevitAddin;component/Resources/cool.png");
            BitmapImage bitmap = new BitmapImage(imageUri);
            button.LargeImage = bitmap;
          

            var paneId = new DockablePaneId(new Guid("249D4A2B-497F-4EC6-8675-B5F0E979BF8C"));
            application.RegisterDockablePane(paneId, "Dockable Pane", new DockablePaneProvider());
            
            return Result.Succeeded;
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class btn_click : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            DockablePaneId paneId = new DockablePaneId(
             new Guid("249D4A2B-497F-4EC6-8675-B5F0E979BF8C"));

            DockablePane pane = commandData.Application.GetDockablePane(paneId);
            

            if (pane.IsShown())
                pane.Hide();
            else
                pane.Show();
            return Result.Succeeded;
        }
    }

}