using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace Environment.PowerShell
{

    public class AppSettingEnvironmentWriter
    {
        public string Path { get; protected set; }

        public AppSettingEnvironmentWriter(string path)
        {
            Path = path;
        }

        public void SetEnvironment(string environmentValue)
        {

            // if appSettings doesn't exist add it
            //
            // if environment key doesn't exist add it
            // if environment key exists set value

            var xml = XDocument.Load(Path);

            var appSettings = xml.Descendants("configuration").Descendants("appSettings").FirstOrDefault();

            var config = xml.Descendants("configuration").FirstOrDefault();

            if (config == null)
            {
                throw new InvalidOperationException("missing root configuration node.  Somethings horribly wrong with machine.config");
            }


            if (appSettings == null)
            {

                config.Add(AddAppSettings(xml, environmentValue));

            }
            else
            {
                var appSetting = appSettings.Descendants("add").Where(e => e.Attribute("key").Value == "Environment").FirstOrDefault();

                if (appSetting == null)
                {
                    // create
                    //
                    appSetting = CreateAppSetting(environmentValue);

                    appSettings.Add(appSetting);
                }
                else
                {
                    // update
                    appSetting.Attribute("value").Value = environmentValue;

                }
            }


            config.Save(Path);

        }

        XElement CreateAppSetting(string environmentValue)
        {

            var appSetting = new XElement("add");

            appSetting.Add(new XAttribute("key", "Environment"));
            appSetting.Add(new XAttribute("value", environmentValue));

            return appSetting;
        }


        XElement AddAppSettings(XDocument xml, string environmentValue)
        {


            var appSettings = new XElement("appSettings");

            var appSetting = new XElement("add");

            appSetting.Add(new XAttribute("key", "Environment"));
            appSetting.Add(new XAttribute("value", environmentValue));

            appSettings.Add(appSetting);


            return appSettings;

        }


    }


}
