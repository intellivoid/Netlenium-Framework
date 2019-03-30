using System.Diagnostics;

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
        private PerformanceCounter MemoryCounter { get; set; }

        /// <summary>
        /// The CPU Counter
        /// </summary>
        private PerformanceCounter CpuCounter { get; set; }

        /// <summary>
        /// Returns the Memory Usage in MB
        /// </summary>
        public double MemoryUsage
        {
            get
            {
                return (MemoryCounter.NextValue() / 1024 / 1024);
            }
        }

        /// <summary>
        /// Returns the CPU Usage (0-100)
        /// </summary>
        public double CpuUsage
        {
            get
            {
                return CpuCounter.NextValue();
            }
        }

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
            PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");

            string[] instances = cat.GetInstanceNames();
            foreach (string instance in instances)
            {

                using (PerformanceCounter cnt = new PerformanceCounter("Process",
                     "ID Process", instance, true))
                {
                    int val = (int)cnt.RawValue;
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
