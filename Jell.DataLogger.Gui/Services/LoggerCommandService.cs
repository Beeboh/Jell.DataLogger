using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows;
using Jell.DataLogger.Core.Models;
using Jell.DataLogger.Core.Exceptions;
using Jell.DataLogger.Gui.Parsers;
using Jell.DataLogger.Gui.Formatters;


namespace Jell.DataLogger.Gui.Services
{
    public class LoggerCommandService
    {
        private string PortName { get; }

        public LoggerCommandService(string portname)
        {
            PortName = portname;
        }
        public LoggerInfo RequestLoggerInfo()
        {
            SerialPort port;
            try
            {
                port = GetSerialPort();
            }
            catch
            {
                throw new InvalidPortException();
            }
            string datastring;
            try
            {
                port.Open();
                port.WriteLine("0001");
                datastring = port.ReadLine();
                port.Close();
            }
            catch
            {
                throw new TimeoutException();
            }
            try
            {
                DataParser DataParser = new DataParser();
                return DataParser.Parse(datastring);
            }
            catch
            {
                throw new ParsingException();
            }
        }
    
        public void SetParameters(DateTime starttime, DateTime endtime, int samplerate)
        {
            SerialPort port = GetSerialPort();

            ParameterStringFormatter formatter = new ParameterStringFormatter();
            string parameterstring = formatter.GetParameterString(starttime, endtime, samplerate);
            port.Open();
            port.WriteLine(parameterstring);
            port.Close();
        }
        private SerialPort GetSerialPort()
        {
            SerialPort port = new SerialPort(PortName, 9600);
            port.DtrEnable = true;
            port.ReadTimeout = 5000;
            port.WriteTimeout = 5000;
            return port;
        }
    }
}
