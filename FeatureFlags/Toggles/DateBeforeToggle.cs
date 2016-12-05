using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags.Providers;

namespace FeatureFlags.Toggles
{
    public abstract class DateBeforeToggle : FeatureToggle, IFeatureToggle
    {
        protected  DateBeforeToggle() : this(new AppSettingsProvider()) { }
        protected DateBeforeToggle(IFeatureProvider provider)
        {
            _provider = provider;
        }

        public bool Enabled
        {
            get
            {
                var now = DateTime.Now;

                var value = base.GetKey();

                DateTime timeToCompare;
                if (DateTime.TryParse(value, out timeToCompare))
                {
                    return now < timeToCompare;
                }
                return false;
            }
        }
    }
}
