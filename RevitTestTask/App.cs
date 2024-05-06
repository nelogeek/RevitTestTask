using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;
using System.Linq;

namespace RevitTestTask
{
    [Transaction(TransactionMode.Manual)]
    class App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            // Создаем экземпляр вашей панели
            MyControl control = new MyControl();

            // Получаем данные провайдера для вашей панели
            DockablePaneProviderData data = new DockablePaneProviderData();
            data.FrameworkElement = control;

            // Регистрируем панель с помощью данных провайдера
            application.RegisterDockablePane(MyControl.PaneId, MyControl.PaneName, control);

            // Подписываемся на событие SelectionChanged
            application.SelectionChanged += (sender, e) => OnSelectionChanged(control, e);

            return Result.Succeeded;
        }



        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var document = e.GetDocument();
            var elementIds = e.GetSelectedElements();
            double totalLength = 0.0;
            string allElementNames = "";

            foreach (var elementId in elementIds)
            {
                var element = document.GetElement(elementId);
                var lengthParam = element.LookupParameter("Длина");

                // Проверяем, есть ли параметр длины у текущего элемента
                if (lengthParam != null)
                {
                    // Получаем значение параметра длины
                    double length = lengthParam.AsDouble();

                    // Добавляем длину к общей сумме
                    totalLength += length;
                }

                // Добавляем имя элемента к строке с названиями
                allElementNames += $"\n - {element.Name}";
            }

            // Обновляем текст Label на пользовательском элементе управления
            MyControl control = sender as MyControl;
            if (control != null)
            {
                control.ElementNamesText = allElementNames;
                control.TotalLengthText = $"Сумма всех длин: {totalLength:F3} м";
            }





            //var document = e.GetDocument();
            //var elementIds = e.GetSelectedElements();
            //double totalLength = 0.0;
            //string allElementNames = "";

            //foreach (var elementId in elementIds)
            //{
            //    var element = document.GetElement(elementId);
            //    var lengthParam = element.LookupParameter("Длина");

            //    // Проверяем, есть ли параметр длины у текущего элемента
            //    if (lengthParam != null)
            //    {
            //        // Получаем значение параметра длины
            //        double length = lengthParam.AsDouble();

            //        // Добавляем длину к общей сумме
            //        totalLength += length;
            //    }

            //    // Добавляем имя элемента к строке с названиями
            //    allElementNames += $"\n - {element.Name}";
            //}

            //// Выводим названия всех объектов и их общую длину в диалоговое окно
            //TaskDialog.Show("Выбранные элементы", $"Названия всех объектов: {allElementNames}\nСумма всех длин: {totalLength:F3} м");
        }

    }
}
