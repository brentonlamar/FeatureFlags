using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags.Providers;

namespace FeatureFlags.Toggles
{
    public class IdListFeatureToggle : FeatureToggle, IFeatureToggle
    {
        private int _id;
        public char Seperator { get; set; }

        protected IdListFeatureToggle(int id) : this(new AppSettingsProvider(), id, FeatureFlags.Seperator.Comma) { }

        protected IdListFeatureToggle(int id, char seperator) : this(new AppSettingsProvider(), id, seperator) { }
        protected IdListFeatureToggle(IFeatureProvider provider, int id, char seperator)
        {
            _provider = provider;
            _id = id;
            Seperator = seperator;
        }

        public bool Enabled
        {
            get
            {
                var stringValue = base.GetKey();
                var values = stringValue.Split(Seperator);
                foreach (var value in values)
                {
                    int intValue;
                    if (!int.TryParse(value, out intValue))
                    {
                        throw new ArgumentException($"Invalid value: {value}");
                    }

                    if (intValue == _id)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
