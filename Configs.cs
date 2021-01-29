using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XS.Core;

namespace UpZips
{
    public class ConfigsModel
    { 
        public string LastPath { get; set; }

    }

    public class Configs
    {
        public readonly static Configs Instance = new Configs();
        public JsonFile<ConfigsModel> Cf;
        public Configs()
        {
            string sPath = string.Concat(Application.StartupPath, "\\Configs.json");
            Cf = new JsonFile<ConfigsModel>(sPath);
        }
    }
}
