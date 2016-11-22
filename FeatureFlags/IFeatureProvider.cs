using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags
{
    public interface IFeatureProvider
    {
        string GetValue(string key);
    }
}
