using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netlenium
{
    public class PerformanceMonitor
    {
        private PerformanceCounter MemoryCounter { get; set; }

        private PerformanceCounter CpuCounter { get; set; }

        public double MemoryUsage
        {
            get
            {
                return (MemoryCounter.NextValue() / 1024 / 1024);
            }
        }

        public double CpuUsage
        {
            get
            {
                return CpuCounter.NextValue();
            }
        }

        public PerformanceMonitor(int processId)
        {
            MemoryCounter = new PerformanceCounter("Process", "Working Set", GetProcessInstanceName(processId));
            CpuCounter = new PerformanceCounter("Process", "% Processor Time", GetProcessInstanceName(processId));
        }

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
