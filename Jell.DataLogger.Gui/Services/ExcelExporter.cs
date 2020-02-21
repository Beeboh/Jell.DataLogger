using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace Jell.DataLogger.Gui.Services
{
    //Currently not used. But, this solution is ideal if the viewmodel can obtain access to the Datagrid.
    public class ExcelExporter
    {
        public void DataGridToExcel(DataGrid datagrid)
        {
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            for(int i = 0; i < datagrid.Columns.Count; i++)
            {
                ExcelApp.Cells[1, i+1] = datagrid.Columns[i].Header;
            }
            for(int i = 0; i < datagrid.Items.Count; i++)
            {
                for(int j = 0; i < datagrid.Columns.Count; i++)
                {
                    ExcelApp.Cells[i + 2, j + 1] = (datagrid.Items[i] as DataRowView).Row.ItemArray[j].ToString();
                }
            }
            ExcelApp.Columns.AutoFit();
            ExcelApp.Visible = true;
        }
    }
}
