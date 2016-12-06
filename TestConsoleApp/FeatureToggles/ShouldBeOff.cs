using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags.Providers;
using FeatureFlags.Toggles;

namespace TestConsoleApp.FeatureToggles
{
    public class ShouldBeOff : BoolFeatureToggle
    {
        public ShouldBeOff() : base(new MssqlSettingsProvider()) { }
    }
}
