using System.Collections.Generic;

namespace Cypal.MultiTenants
{
    public class Tenant
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public List<string> Hosts { get; set; }
        public IDictionary<string,string> Settings { get; set; }

        public string Setting(string key, string defaultValue = null) 
            => Settings.ContainsKey(key)
                ? Settings[key]
                : null;
        public int? IntSetting(string key, int? defaultValue = null) 
            => Settings.ContainsKey(key)
                ? int.Parse(Settings[key])
                : defaultValue;
    }

}

