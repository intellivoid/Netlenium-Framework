using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Netlenium
{
    public class Logging
    {
        private static bool _AllowLogging = false;

        private static string _OutputFile = string.Empty;

        public static bool AllowLogging
        {
            get
            {
                return _AllowLogging;
            }
            set
            {
                _AllowLogging = value;
            }
        }

        public static string OutputFile
        {
            get
            {
                return _OutputFile;
            }
            set
            {
                _OutputFile = value;
            }
        }

        public static void WriteEntry(Types.LogType LoggingType, string ModuleName, string EntryText)
        {
            if(_AllowLogging == false)
            {
                return;
            }
            
            string Timestamp = DateTime.Now.ToString(@"h\:mm tt");

            

            switch (LoggingType)
            {
                case Types.LogType.Information:
                    if (_OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(_OutputFile, $"[INFORMATION][{Timestamp}]: {ModuleName} > {EntryText}{Environment.NewLine}");
                        }
                        catch(Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("[ ~~~ ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{Timestamp}]: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{ModuleName} > ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(EntryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case Types.LogType.Warning:
                    if (_OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(_OutputFile, $"[WARNING][{Timestamp}]: {ModuleName} > {EntryText}{Environment.NewLine}");
                        }
                        catch (Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[  !  ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{Timestamp}]: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{ModuleName} > ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(EntryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case Types.LogType.Error:
                    if (_OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(_OutputFile, $"[ERROR][{Timestamp}]: {ModuleName} > {EntryText}{Environment.NewLine}");
                        }
                        catch (Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[  X  ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{Timestamp}]: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{ModuleName} > ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(EntryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case Types.LogType.Debug:
                    if (_OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(_OutputFile, $"[DEBUG][{Timestamp}]: {ModuleName} > {EntryText}{Environment.NewLine}");
                        }
                        catch (Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("[ ~~~ ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{Timestamp}]: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{ModuleName} > ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(EntryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

            }
        }
    }
}
