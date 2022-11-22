using FastShare.Core.Model;
using FastShare.UI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastShare.UI.WinUI.Storage
{
    internal class FSStorage : IStorage
    {
        private static FSStorage _instance = null;

        public static FSStorage Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new FSStorage();
                }

                return _instance;
            }
        }

        public FSConfig Config { get; } = GetConfigOrCreate();

        private static string _appDataPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
        private static string configFilePath = Path.Combine(_appDataPath, "config.json");

        private FSStorage() { }

        private static FSConfig GetConfigOrCreate()
        {
            Debug.WriteLine(configFilePath);
            if(!File.Exists(configFilePath))
            {
                File.WriteAllText(configFilePath, FSConfig.GetDefaults().ToString());
            }

            return FSConfig.Parse(File.ReadAllText(configFilePath));
        }

        public void Save()
        {
            File.WriteAllText(configFilePath, Config.ToString());
        }
    }
}
