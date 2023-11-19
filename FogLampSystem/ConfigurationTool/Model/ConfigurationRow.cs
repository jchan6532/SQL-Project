using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationTool.Model
{
    public class ConfigurationRow
    {
        public string ConfigurationKey { get; set; }
        public int ConfigurationValue_Int { get; set; }
        public float ConfigurationValue_Float { get; set; }
        public string ConfigurationValue_String { get; set; }
    }
}
