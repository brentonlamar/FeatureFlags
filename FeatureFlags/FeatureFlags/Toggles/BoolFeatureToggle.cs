using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags.Providers;

namespace FeatureFlags.Toggles
{
    public abstract class BoolFeatureToggle : FeatureToggle, IFeatureToggle
    {
        protected BoolFeatureToggle() : this(new AppSettingsProvider()) { }
        protected BoolFeatureToggle(IFeatureProvider provider)
        {
            _provider = provider;
        }

        public bool Enabled
        {
            get
            {
                var value = base.GetKey();

                switch (value)
                {
                    case "0":
                        return false;
                    case "1":
                        return true;
                    default:
                        return Convert.ToBoolean(value);
                }
            }
        }
    }
}
