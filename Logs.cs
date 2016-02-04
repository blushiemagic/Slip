using System;
using System.IO;

namespace Slip
{
    public static class Logs
    {
        public const string path = "Logs.txt";

        public static void Log(string text)
        {
            File.AppendAllText(path, text + Environment.NewLine);
        }

        public static void ClearLog()
        {
            using(FileStream stream = File.Create(path))
            {
            }
        }
    }
}
