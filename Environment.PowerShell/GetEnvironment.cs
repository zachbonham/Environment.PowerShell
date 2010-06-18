using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.Xml.Linq;


namespace Environment.PowerShell
{


    [Cmdlet("Get", "Environment")]
    public class GetEnvironment : PSCmdlet
    {
        protected override void ProcessRecord()
        {

            foreach (var path in AvailableMachineConfigs.MachineConfigs.OrderBy(s => s))
            {
                var tokens = path.Split('\\');

                var frameworkVersion = string.Format(@"{0}\{1}", tokens[3], tokens[4]);

                var xml = XDocument.Load(path);

                var appSetting = xml.Descendants("configuration")
                    .Descendants("appSettings")
                    .Descendants("add")
                    .Where(n => n.Attribute("key")
                        .Value == "Environment")
                        .FirstOrDefault();

                string environmentValue = string.Empty;

                if (appSetting == null)
                {
                    environmentValue = "Environment not set";
                }

                else
                {
                    environmentValue = appSetting.Attribute("value").Value;
                }

                WriteObject(frameworkVersion + "\t= " + environmentValue);
            }

            
        }
    }
}
