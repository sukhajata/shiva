using ShivaShared3.Data;
using ShivaShared3.DataControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace ShivaWPF3.UtilityWPF
{
    public class ExcelPropertyComparer
    {
        public ExcelPropertyComparer()
        {
            Excel.Application excelApp = new Excel.Application();

            string workbookPath = GlobalData.Singleton.IOHelper.GetXMLFilePath("Control Definitions.xlsx");
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);

            Excel.Sheets excelSheets = excelWorkbook.Worksheets;

            //control types
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item("Control Types");
            Excel.Range range = excelWorksheet.UsedRange;

            string controlName;

            for (int row = 2; row <= range.Rows.Count; row++)
            {
                controlName = (string)(range.Cells[row, 1] as Excel.Range).Value2;
                Type controlType =  ControlTypeMapping.GetControlType(controlName);

            }

            excelWorkbook.Close(true, null, null);
            excelApp.Quit();

            ReleaseObject(excelSheets);
            ReleaseObject(excelWorkbook);
            ReleaseObject(excelApp);

        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
