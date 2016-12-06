using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsoleApp.FeatureToggles;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Please review app.config to see the queries involved.
             * Assumes two different tables, one with a schema of ID (Feature) and IsOn (bit),
             * Second is UserId, FeatureId for a many-to-many type 
             * relationship (which would map to the BusinessLevelProFeatureToggle feature). 
             */

            var shouldBeOn = new ShouldBeOn();
            var shouldBeOff = new ShouldBeOff();
            var user100 = 100;
            var user200 = 200;

            var isOnForUser100 = new BusinessLevelProFeatureToggle(user100, 1);
            if (isOnForUser100.Enabled)
            {
                Console.WriteLine("The feature is on for user 100");
            }
            var isOnForUser200 = new BusinessLevelProFeatureToggle(user200, 2);
            if (isOnForUser200.Enabled)
            {
                Console.WriteLine("The feature is on for user 200");
            }


            if (shouldBeOn.Enabled)
            {
                Console.WriteLine("The feature [ShouldBeOn] is on, as it should be");
            }
            else
            {
                Console.WriteLine("There's a serious problem with 'ShouldBeOn'");
            }

            if (!shouldBeOff.Enabled)
            {
                Console.WriteLine("The feature [ShouldBeOff] is off, as it should be");
            }
            else
            {
                Console.WriteLine("There's a serious problem with 'ShouldBeOn'");
            }

            Console.ReadLine();
        }
    }
}
