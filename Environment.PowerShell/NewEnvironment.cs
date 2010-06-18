using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.Xml.Linq;


namespace Environment.PowerShell
{

    
    [Cmdlet("New", "Environment")]
    public class NewEnvironment : PSCmdlet
    {
        [Parameter(Mandatory=true, Position=0)]
        public string Value { get; set; }
        protected override void ProcessRecord()
        {
            foreach (var path in AvailableMachineConfigs.MachineConfigs)
            {
                var writer = new AppSettingEnvironmentWriter(path);
                writer.SetEnvironment(Value);
            }
        }
    }
}
