using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags
{
    public abstract class FeatureToggle
    {
        protected IFeatureProvider _provider { get; set; }
        public string Key => $"FeatureFlags.{this.GetType().Name}";

        protected string GetKey()
        {
            var value = _provider.GetValue(Key);
            if (value == null)
            {
                throw new ArgumentNullException($"Key {Key} is missing from the configuration file.");
            }
            return value;
        }
    }
}
