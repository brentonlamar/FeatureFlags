using System;
using FeatureFlags.Providers;

namespace FeatureFlags.Toggles
{
    public abstract class DateAfterFeatureToggle : FeatureToggle, IFeatureToggle
    {
        protected DateAfterFeatureToggle() : this(new AppSettingsProvider()) { }
        protected DateAfterFeatureToggle(IFeatureProvider provider)
        {
            _provider = provider;
        }
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

                return parsedDate > now;
            }
        }
    }
}
