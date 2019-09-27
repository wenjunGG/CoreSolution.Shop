using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreSolution.Tools
{
   public class WriteLogHelper
    {
        public static void WriteLogMany(string text)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = System.IO.Path.Combine(path
            , "Logs\\");

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string fileFullName = System.IO.Path.Combine(path
            , string.Format("{0}.txt", DateTime.Now.ToString("yyyyMMdd-HHmm")));


            using (StreamWriter output = System.IO.File.AppendText(fileFullName))
            {
                output.WriteLine(text);

                output.Close();
            }
        }


        public static void WriteLogDoc(string documentName, string msg,string folderName)
        {
            string errorLogFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
            if (!System.IO.Directory.Exists(errorLogFilePath))
            {
                System.IO.Directory.CreateDirectory(errorLogFilePath);
            }
            string logFile = System.IO.Path.Combine(errorLogFilePath, documentName + "@" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt");
            bool writeBaseInfo = System.IO.File.Exists(logFile);
            StreamWriter swLogFile = new StreamWriter(logFile, true, Encoding.Unicode);
            swLogFile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t" + msg);
            swLogFile.Close();
            swLogFile.Dispose();


        }
    }
}
