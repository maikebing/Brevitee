using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace laotzu
{
    [Serializable]
    public class History
    {
        public History()
        {
            _genConfigs = new Dictionary<string, GenConfig>();
        }

        public void Add(GenConfig config)
        {
            if (!_genConfigs.ContainsKey(config.Name))
            {
                _genConfigs.Add(config.Name, config);
            }
            else
            {
                _genConfigs[config.Name] = config;
            }
        }

        Dictionary<string, GenConfig> _genConfigs;
        public GenConfig[] GenConfigs
        {
            get
            {
                return _genConfigs.Values.ToArray();
            }
            set
            {
                _genConfigs.Clear();
                foreach (GenConfig c in value)
                {
                    Add(c);
                }
            }
        }

    }
}
