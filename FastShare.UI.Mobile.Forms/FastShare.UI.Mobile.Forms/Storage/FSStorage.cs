using FastShare.Core.Model;
using FastShare.UI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FastShare.UI.Mobile.Forms.Storage
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

        private static string _appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string _configFilePath = Path.Combine(_appDataPath, "config.json");

        private FSStorage()
        {
        }

        public static FSConfig GetConfigOrCreate()
        {
            if(!File.Exists(_configFilePath))
            {
                File.WriteAllText(_configFilePath, FSConfig.GetDefaults().ToString());
            }
            return FSConfig.Parse(File.ReadAllText(_configFilePath));
        }

        public void Save()
        {
            File.WriteAllText(_configFilePath, Config.ToString());
        }
    }
}
