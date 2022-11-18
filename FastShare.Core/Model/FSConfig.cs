using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FastShare.Core.Model
{
    public class FSConfig
    {

        public static FSConfig Parse(string config)
        {
            return JsonSerializer.Deserialize<FSConfig>(config);
        }

        public static FSConfig GetDefaults()
        {
            return new FSConfig
            {
                DefaultSavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
        }

        public string DefaultSavePath { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
