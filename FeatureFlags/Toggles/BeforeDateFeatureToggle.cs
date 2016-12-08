using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags.Providers;

namespace FeatureFlags.Toggles
{
    public abstract class BeforeDateFeatureToggle : FeatureToggle, IFeatureToggle
    {
        protected  BeforeDateFeatureToggle() : this(new AppSettingsProvider()) { }
        protected BeforeDateFeatureToggle(IFeatureProvider provider)
        {
            _provider = provider;
        }
        /// <summary>
        /// Compares the current date with the toggle.  Returns true if 
        /// the current datetime is before the specified date.  Useful to
        /// say a feature is valid until a date.
        /// </summary>
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
