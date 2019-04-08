using System;
using System.Diagnostics;
using System.IO;
using Netlenium.Types;

namespace Netlenium
{
    /// <summary>
    /// Logging Class
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// If set to True, general logging messages will be displayed in the CLI
        /// </summary>
        public static bool Enabled { get; set; }

        /// <summary>
        /// If set to True, alongside general logging messages; debugging messages will be shown on the CLI
        /// </summary>
        public static bool VerboseLogging { get; set; }

        /// <summary>
        /// The output file to output all the data to (AllowLogging doesn't need to be set to True for this to work)
        /// </summary>
        public static string OutputFile { get; set; } = string.Empty;

        /// <summary>
        /// Writes a vebrose Log Entry
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="entryText"></param>
        public static void WriteVerboseEntry(string moduleName, string entryText)
        {
            if (VerboseLogging == false)
            {
                return;
            }
            
            WriteEntry(LogType.Verbose, moduleName, entryText);
        }
        
        /// <summary>
        /// Writes a Log Entry
        /// </summary>
        /// <param name="loggingType"></param>
        /// <param name="moduleName"></param>
        /// <param name="entryText"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void WriteEntry(LogType loggingType, string moduleName, string entryText)
        {
            if(Enabled == false)
            {
                return;
            }
            
            var timestamp = DateTime.Now.ToString(@"h\:mm tt");

            switch (loggingType)
            {
                case LogType.Success:
                    if (OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(OutputFile, string.Format(GeneralLocalStrings.Logging_WriteEntry_SUCCESS_FileFormat, timestamp, moduleName, entryText, Environment.NewLine));
                        }
                        catch(Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_Warning);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_ModuleName, moduleName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                
                case LogType.Information:
                    if (OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(OutputFile, string.Format(GeneralLocalStrings.Logging_WriteEntry_INFORMATION_FileFormat, timestamp, moduleName, entryText, Environment.NewLine));
                        }
                        catch(Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_Information);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_ModuleName, moduleName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case LogType.Warning:
                    if (OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(OutputFile, string.Format(GeneralLocalStrings.Logging_WriteEntry_WARNING_FileFormat, timestamp, moduleName, entryText, Environment.NewLine));
                        }
                        catch (Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_Warning);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_ModuleName, moduleName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case LogType.Error:
                    if (OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(OutputFile, string.Format(GeneralLocalStrings.Logging_WriteEntry_ERROR_FileFormat, timestamp, moduleName, entryText, Environment.NewLine));
                        }
                        catch (Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_Error);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_ModuleName, moduleName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case LogType.Debug:
                    if (OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(OutputFile, string.Format(GeneralLocalStrings.Logging_WriteEntry_DEBUG_FileFormat, timestamp, moduleName, entryText, Environment.NewLine));
                        }
                        catch (Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_Debug);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_ModuleName, moduleName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                    
                case LogType.Verbose:
                    if (OutputFile != string.Empty)
                    {
                        try
                        {
                            File.AppendAllText(OutputFile, string.Format(GeneralLocalStrings.Logging_WriteEntry_VERBOSE_FileFormat, timestamp, moduleName, entryText, Environment.NewLine));
                        }
                        catch (Exception exception)
                        {
                            Debug.Print(exception.Message);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_VERBO);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(GeneralLocalStrings.Logging_WriteEntry_ModuleName, moduleName);
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
