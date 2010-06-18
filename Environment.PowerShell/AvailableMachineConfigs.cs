using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Environment.PowerShell
{
    public class AvailableMachineConfigs
    {
        public static string[] MachineConfigs { get; set; }

        static AvailableMachineConfigs()
        {

            var dir = new List<string>();

            var netDir = string.Format(@"{0}\Microsoft.NET", System.Environment.GetEnvironmentVariable("WINDIR"));

            MachineConfigs = Directory.GetFiles(netDir, "machine.config", SearchOption.AllDirectories);

        }
    }

}
