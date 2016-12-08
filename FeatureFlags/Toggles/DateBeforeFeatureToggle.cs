using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags.Providers;

namespace FeatureFlags.Toggles
{
    public abstract class DateBeforeFeatureToggle : FeatureToggle, IFeatureToggle
    {
        protected  DateBeforeFeatureToggle() : this(new AppSettingsProvider()) { }
        protected DateBeforeFeatureToggle(IFeatureProvider provider)
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
                
                throw new ArgumentException($"Invalid date: {value}");
            }
        }
    }
}
