using System;
using FeatureFlags.Providers;

namespace FeatureFlags.Toggles
{
    public abstract class AfterDateFeatureToggle : FeatureToggle, IFeatureToggle
    {
        protected AfterDateFeatureToggle() : this(new AppSettingsProvider()) { }
        protected AfterDateFeatureToggle(IFeatureProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Compares the current date with the toggle.  Returns true if 
        /// the current datetime is after the specified date.  Useful to
        /// say a feature is valid after a date.
        /// </summary>
        public bool Enabled
        {
            get
            {
                var now = DateTime.Now;

                var value = GetKey();

                DateTime parsedDate;
                if (!DateTime.TryParse(value, out parsedDate))
                {
                    throw new ArgumentException($"Invalid date: {value}");
                }

                return now > parsedDate;
            }
        }
    }
}
