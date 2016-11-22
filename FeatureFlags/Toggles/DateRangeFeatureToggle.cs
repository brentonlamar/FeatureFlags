using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags.Providers;

namespace FeatureFlags.Toggles
{
    public abstract class DateRangeFeatureToggle : FeatureToggle, IFeatureToggle
    {
        public char Seperator { get; set; }

        protected DateRangeFeatureToggle() : this(new AppSettingsProvider(), FeatureFlags.Seperator.Pipe) { }
        protected DateRangeFeatureToggle(IFeatureProvider provider, char seperator)
        {
            _provider = provider;
            Seperator = seperator;
        }
        public bool Enabled
        {
            get
            {
                var now = DateTime.Now;

                var value = base.GetKey();

                var dateRangeValues = value.Split(Seperator);
                if (dateRangeValues.Length != 2)
                {
                    throw new ArgumentException("Invalid date range");
                }

                var dates = new List<DateTime>();
                foreach (var date in dateRangeValues)
                {
                    DateTime dateTime;

                    if (!DateTime.TryParse(date, out dateTime))
                    {
                        throw new ArgumentException($"Invalid date: {date}");
                    }

                    dates.Add(dateTime);
                }

                var nowIsBetweenFirstAndSecondDates = dates[0] <= now && now <= dates[1];
                var nowIsBetweenSecondAndFirstDates = dates[1] <= now && now <= dates[0];

                if (nowIsBetweenFirstAndSecondDates || nowIsBetweenSecondAndFirstDates)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
