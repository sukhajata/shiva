using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisIOHelper
    {
       // StreamReader GetStreamReader(string path);
        //Stream GetXMLStream(string name);
        TextWriter GetTextWriter();
        void LogError(string error);
        void LogEvent(string gnosisEventString);
        void LogSourceExpression(string line);
        string GetXMLFilePath(string fileName);
    }
}
