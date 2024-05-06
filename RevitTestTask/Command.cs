using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTestTask
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApp = commandData.Application;
            UIDocument uidoc = uIApp.ActiveUIDocument;
            Application app = uIApp.Application;
            Document doc = uidoc.Document;

            Reference reference = uidoc.Selection.PickObject(ObjectType.Element);
            Element element = uidoc.Document.GetElement(reference);
            using(Transaction tx = new Transaction(doc))
            {
                tx.Start("transaction");
                TaskDialog.Show($"title :)", element.Name);
                tx.Commit();
            }
            return Result.Succeeded;
        }
    }
}
