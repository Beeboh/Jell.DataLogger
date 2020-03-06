using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jell.DataLogger.Core.Models;
using Microsoft.Win32;
using System.IO;

namespace Jell.DataLogger.Gui.Services
{
    public class CsvExporter
    {
        CsvStringFormatter CsvFormatter { get; } = new CsvStringFormatter();

        public void Export(IEnumerable<ParData> ParData)
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
