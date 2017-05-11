using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.Mapping
{
    public static class ConfigurationMapper
    {
        public static Model.Configuration TranslateDTOConfigurationToModelConfiguration(DTO.Configuration c)
        {
            if (c == null)
                return null;

            var mc = new Model.Configuration
            {
                ConfigurationEntries = new Dictionary<string, string>()
            };

            foreach (var item in c.ConfigurationEntries)
            {
                mc.ConfigurationEntries.Add(item.Key, item.Value);
            }

            return mc;
        }

        public static DTO.Configuration TranslateModelConfigurationToDTOConfiguration(Model.Configuration c)
        {
            if (c == null)
                return null;

            var dtoc = new DTO.Configuration
            {
                ConfigurationEntries = new Dictionary<string, string>()
            };

            foreach (var item in c.ConfigurationEntries)
            {
                dtoc.ConfigurationEntries.Add(item.Key, item.Value);
            }

            return dtoc;
        }
    }
}
