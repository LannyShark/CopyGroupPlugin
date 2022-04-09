using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyGroupPlugin
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CopyGroup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDocument = commandData.Application.ActiveUIDocument;
            Document document = uiDocument.Document;

            Reference reference = uiDocument.Selection.PickObject(ObjectType.Element, "Выберите группу объектов");
            Element element = document.GetElement(reference);
            Group group = element as Group;

            XYZ point = uiDocument.Selection.PickPoint("Выберите точку вставки");

            Transaction transaction = new Transaction(document);
            transaction.Start("Копирование группы объектов");
            document.Create.PlaceGroup(point, group.GroupType);
            transaction.Commit();

            return Result.Succeeded;
        }
    }
}
