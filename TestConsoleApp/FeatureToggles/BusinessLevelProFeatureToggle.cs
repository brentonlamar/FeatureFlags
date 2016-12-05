using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags;
using FeatureFlags.Providers;
using FeatureFlags.Toggles;

namespace TestConsoleApp.FeatureToggles
{
    public class BusinessLevelProFeatureToggle : IdListFeatureToggle
    {
        public BusinessLevelProFeatureToggle(int userId, int featureLevel) : base(featureLevel)
        {
            var kvp = new Dictionary<string, string>();
            kvp.Add("@userId", userId.ToString());

            base._provider = new MssqlSettingsProvider()
            {
                ParameterValues = kvp
            };
        }
    }
}
