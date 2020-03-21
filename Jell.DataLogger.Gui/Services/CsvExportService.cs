using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using Jell.DataLogger.Gui.Formatters;
using Jell.DataLogger.Gui.Models;

namespace Jell.DataLogger.Gui.Services
{
    internal class CsvExportService
    {
        CsvStringFormatter CsvFormatter { get; } = new CsvStringFormatter();

        public void Export(IEnumerable<ViewableParData> ParData)
        {
            string csvString = CsvFormatter.GetString(ParData);
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".csv";
            save.Filter = "CSV|*.csv";
            if (save.ShowDialog() == true)
            {
                File.WriteAllText(save.FileName, csvString);
            }
        }
        
    }
}
