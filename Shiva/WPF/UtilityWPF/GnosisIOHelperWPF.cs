using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using Shiva.Shared.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ShivaWPF3.UtilityWPF
{
    public class GnosisIOHelperWPF : IGnosisIOHelper
    {
        private static object locker = new Object();

        //public StreamReader GetStreamReader(string path)
        //{
        //    StreamReader sr = new StreamReader(path);
        //    return sr;
        //}

        public TextWriter GetTextWriter()
        {
            string path = GetXMLFilePath("layout.xml");
            TextWriter writer = new StreamWriter(path, false);
            return writer;
        }

        public static string GetIconPath(string iconName, bool enabled)
        {
            
            string fileName = IconNameMapper.GetIconFileName(iconName) + ".png";
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string projectPath = Directory.GetParent(exePath).Parent.Parent.FullName;
            string iconPath;

            if (!enabled)
            {
                iconPath = System.IO.Path.Combine(projectPath, "WPF", "Icons", "disabled", fileName);
            }
            else
            {
                iconPath = System.IO.Path.Combine(projectPath, "WPF", "Icons", fileName);
            }

            return iconPath;
        }

        public static string GetImagePath(string imageName, string extension)
        {
            string fileName = imageName + extension;
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string projectPath = Directory.GetParent(exePath).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectPath, "Icons", fileName);

            return imagePath;
        }

        private static string GetProjectPath()
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string projectPath = Directory.GetParent(exePath).Parent.Parent.FullName;

            return projectPath;
        }

        public string GetXMLFilePath(string filename)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string projectPath = Directory.GetParent(exePath).Parent.Parent.FullName;
            string XMLPath = System.IO.Path.Combine(projectPath, "WPF/XML", filename);

            return XMLPath;
        }


        public void LogEvent(string gnosisEventString)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string projectPath = Directory.GetParent(exePath).Parent.Parent.FullName;
            string eventLoggerPath = System.IO.Path.Combine(projectPath, "Logs", "eventLogger.txt");

            try
            {
                using (StreamWriter ws = new StreamWriter(eventLoggerPath, true))
                {
                    ws.WriteLineAsync(gnosisEventString);
                }
            }
            catch (IOException ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }
        }

        public void LogSourceExpression(string line)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string projectPath = Directory.GetParent(exePath).Parent.Parent.FullName;
            string sourceExpressionLog = System.IO.Path.Combine(projectPath, "Logs", "sourceExpressionLog.txt");

            try
            {
                using (StreamWriter ws = new StreamWriter(sourceExpressionLog, true))
                {
                    ws.WriteLineAsync(line);
                }
            }
            catch (IOException ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

        }

        public async void LogError(string error)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string projectPath = Directory.GetParent(exePath).Parent.Parent.FullName;
            string errorLogPath = System.IO.Path.Combine(projectPath, "WPF", "Logs", "error-log.txt");

            try
            {
                using (StreamWriter sw = new StreamWriter(errorLogPath, true))
                {
                    await sw.WriteLineAsync(error);
                }
            }
            catch (IOException ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }
        }

        public static void WriteXamlToFile(string xaml)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string projectPath = Directory.GetParent(exePath).Parent.Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Logs", "output.xaml");

            
            try
            {
                XElement xElement;

                using (StringReader sr = new StringReader(xaml))
                {
                    xElement = XElement.Load(sr);
                    xElement.Descendants().Where(e => e.Name.ToString().Contains(".Style")).Remove();

                }

                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                   
                    sw.WriteLine(xElement.ToString());
                }
            }
            catch (IOException ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }
        }
        //public static Stream GetXMLStream(string name)
        //{
        //    lock (locker)
        //    {
        //        FileStream stream = new FileStream(GetXMLFilePath(name), FileMode.Open);
        //        return stream;
        //    }
        //}
    }
}
