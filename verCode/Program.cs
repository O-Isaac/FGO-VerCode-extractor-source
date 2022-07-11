using System.Reflection;
using Newtonsoft.Json;
using verCode.console;

namespace VerCode
{

    class ClientData
    {
        public string verCode;
        public string appVer;
        
        public ClientData()
        {
            this.verCode = "Unknow";
            this.appVer = "Unknow";
        }
    }

    class Extractor
    {
        public static ClientData getVerCode(string dllPath)
        {

            Assembly gameDll = Assembly.LoadFrom(dllPath);

            var test = gameDll.CreateInstance("ManagerConfig");
            var member = test.GetType().GetFields();

            ClientData client = new ClientData();

            foreach (var field in member)
            {
                if (field.Name == "verCode")
                {
                    var VerCodeField = field.GetValue(field);

                    if(VerCodeField != null)
                    {
                        Logger.Info("Extractor", "ClientData", "VerCode: {0}", VerCodeField);
                        client.verCode = VerCodeField.ToString();
                    } else
                    {
                        Logger.Warn("Extractor", "ClientData", "Field VerCode is null! using default value: Unknow");
                    }

                }

                if (field.Name == "AppVer")
                {
                    var AppVerField = field.GetValue(field);

                    if(AppVerField != null)
                    {
                        Logger.Info("Extractor", "ClientData", "AppVer: {0}", AppVerField);
                        client.appVer = AppVerField.ToString();
                    } else
                    {
                        Logger.Warn("Extractor", "ClientData", "Field AppVer is null! using default value: Unknow");
                    }

                }
            }

            return client;

        }

    }

    class ConfigGenerator
    {
        public static void generateVerCodeConfig(ClientData client)
        {
            var data = JsonConvert.SerializeObject(client);
            string currentPath = Directory.GetCurrentDirectory();

            System.IO.File.WriteAllText(@$"{currentPath}/VerCode.json", data);
            Logger.Info("ConfigGenerator", "VerCode", "File VerCode.json save in {0}", currentPath);
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Logger.Info("Program", "Main", "Starting process to get VerCode!");
            string path = string.Join(" ", args);
            ClientData client = Extractor.getVerCode(@$"{path}");
            ConfigGenerator.generateVerCodeConfig(client);
        }
    }
}