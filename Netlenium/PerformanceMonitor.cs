﻿using System.Diagnostics;

namespace Netlenium
{
    /// <summary>
    /// Performance Monitor Class which monitors CPU/RAM usage of a process
    /// </summary>
    public class PerformanceMonitor
    {
        /// <summary>
        /// The Memory Counter
        /// </summary>
        private PerformanceCounter MemoryCounter { get; }

        /// <summary>
        /// The CPU Counter
        /// </summary>
        private PerformanceCounter CpuCounter { get; }

        /// <summary>
        /// Returns the Memory Usage in MB
        /// </summary>
        public double MemoryUsage => (MemoryCounter.NextValue() / 1024 / 1024);

        /// <summary>
        /// Returns the CPU Usage (0-100)
        /// </summary>
        public double CpuUsage => CpuCounter.NextValue();

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="processId"></param>
        public PerformanceMonitor(int processId)
        {
            MemoryCounter = new PerformanceCounter("Process", "Working Set", GetProcessInstanceName(processId));
            CpuCounter = new PerformanceCounter("Process", "% Processor Time", GetProcessInstanceName(processId));
        }

        /// <summary>
        /// Gets the process instance name by Process ID
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        private static string GetProcessInstanceName(int pid)
        {
            var cat = new PerformanceCounterCategory("Process");

            var instances = cat.GetInstanceNames();
            foreach (var instance in instances)
            {

                using (var cnt = new PerformanceCounter("Process", "ID Process", instance, true))
                {
                    var val = (int)cnt.RawValue;
                    
                    if (val == pid)
                    {
                        return instance;
                    }
                }
            }

            return null;
        }
    }
}
