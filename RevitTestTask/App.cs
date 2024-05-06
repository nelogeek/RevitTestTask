using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTestTask
{
    [Transaction(TransactionMode.Manual)]
    class App : IExternalApplication
    {
        public static MyControl MyDockablePaneControl;

        

        public Result OnStartup(UIControlledApplication a)
        {
            a.ViewActivated += OnViewActivated;

            MyDockablePaneControl = new MyControl();

            MyDockablePaneViewModel dockablePaneViewModel =
               new MyDockablePaneViewModel();

            MyDockablePaneControl.DataContext = dockablePaneViewModel;

            if (!DockablePane.PaneIsRegistered(MyControl.PaneId))
            {
                a.RegisterDockablePane(MyControl.PaneId,
                    MyControl.PaneName,
                    MyDockablePaneControl);
            }


            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        private void OnViewActivated(object sender, ViewActivatedEventArgs e)
        {
            if (e.Document == null)
                return;

            var viewModel = MyDockablePaneControl.DataContext as MyDockablePaneViewModel;

            if (viewModel != null)
            {
                viewModel.DocumentTitle = e.Document.Title;
            }
        }

    }
}
