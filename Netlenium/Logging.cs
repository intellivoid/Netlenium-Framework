using System;
using System.Diagnostics;
using System.IO;

namespace Netlenium
{
    /// <summary>
    /// Logging Class
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// If set to True, general logging messages will be displayed in the CLI
        /// </summary>
        public static bool Enabled { get; set; } = false;

        /// <summary>
        /// If set to True, alongside general logging messages; debugging messages will be shown on the CLI
        /// </summary>
        public static bool VerboseLogging { get; set; } = false;

        /// <summary>
        /// The output file to output all the data to (AllowLogging doesn't need to be set to True for this to work)
        /// </summary>
        public static string OutputFile { get; set; } = string.Empty;

        /// <summary>
        /// Writes a vebrose Log Entry
        /// </summary>
        /// <param name="loggingType"></param>
        /// <param name="moduleName"></param>
        /// <param name="entryText"></param>
        public static void WriteVerboseEntry(Types.LogType loggingType, string moduleName, string entryText)
        {
            if (VerboseLogging == false)
            {
                return;
            }
            
            WriteEntry(loggingType, $"{entryText} (VERBO)", moduleName);
        }
        
        /// <summary>
        /// Writes a Log Entry
        /// </summary>
        /// <param name="loggingType"></param>
        /// <param name="moduleName"></param>
        /// <param name="entryText"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void WriteEntry(Types.LogType loggingType, string moduleName, string entryText)
        {
            if(Enabled == false)
            {
                return;
            }
            
            var timestamp = DateTime.Now.ToString(@"h\:mm tt");

            switch (loggingType)
            {
                case Types.LogType.Success:
                    if (OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(OutputFile, $@"[SUCCESS][{timestamp}]: {moduleName} > {entryText}{Environment.NewLine}");
                        }
                        catch(Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(@"[  !  ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($@"[{timestamp}]: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($@"{moduleName} > ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                
                case Types.LogType.Information:
                    if (OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(OutputFile, $@"[INFORMATION][{timestamp}]: {moduleName} > {entryText}{Environment.NewLine}");
                        }
                        catch(Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(@"[ ~~~ ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($@"[{timestamp}]: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($@"{moduleName} > ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case Types.LogType.Warning:
                    if (OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(OutputFile, $@"[WARNING][{timestamp}]: {moduleName} > {entryText}{Environment.NewLine}");
                        }
                        catch (Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(@"[  !  ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($@"[{timestamp}]: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($@"{moduleName} > ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case Types.LogType.Error:
                    if (OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(OutputFile, $@"[ERROR][{timestamp}]: {moduleName} > {entryText}{Environment.NewLine}");
                        }
                        catch (Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(@"[  X  ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($@"[{timestamp}]: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($@"{moduleName} > ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case Types.LogType.Debug:
                    if (OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(OutputFile, $@"[DEBUG][{timestamp}]: {moduleName} > {entryText}{Environment.NewLine}");
                        }
                        catch (Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(@"[DEBUG]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($@"[{timestamp}]: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($@"{moduleName} > ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(loggingType), loggingType, null);
            }
        }
    }
}
