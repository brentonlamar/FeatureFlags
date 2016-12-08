using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags.Toggles;
using NUnit.Framework;

namespace FeatureFlags.Tests
{
    public class DateBeforeFeatureToggleTests
    {

        [Test]
        public void DateRange_InvalidDate_ReturnsException()
        {
            // Arrange 
            var toggle = new BeforeDateRangeToggleInvalidBeforeDate(new DatesProvider());
            // ACT 

            // Assert 
            Assert.Throws<ArgumentException>(() =>
            {
                var result = toggle.Enabled;
            });
        }
        

        [Test]
        public void ToggleTimeIsAfterNow()
        {
            var toggle = new BeforeDateRangeToggleToggleTimeIsAfterNow(new DatesProvider());
            Assert.That(toggle.Enabled, Is.True);
        }

        [Test]
        public void ToggleTimeIsBeforeNow()
        {
            var toggle = new BeforeDateRangeToggleToggleTimeIsNow(new DatesProvider());
            Assert.That(toggle.Enabled, Is.False);
        }


        private class DatesProvider : IFeatureProvider
        {
            public DatesProvider()
            {
                var invalidNumberOfDates = new BeforeDateRangeToggleInvalidBeforeDate();

                var afterNowToggle = new BeforeDateRangeToggleToggleTimeIsAfterNow();
                var beforeNowToggle = new BeforeDateRangeToggleToggleTimeIsNow();


                var keyThatIsBeforeNow = DateTime.Now.AddMinutes(-10).ToLongTimeString();
                var keyThatIsAfterNow = DateTime.Now.AddMinutes(10).ToLongTimeString();

                _kvp = new Dictionary<string, string>
                {
                    {invalidNumberOfDates.Key, "InvalidDate"},
                    {afterNowToggle.Key, keyThatIsAfterNow},
                    {beforeNowToggle.Key, keyThatIsBeforeNow},

                };
            }

            private readonly Dictionary<string, string> _kvp;


            public string GetValue(string key)
            {
                return _kvp[key];
            }
        }

        class BeforeDateRangeToggleInvalidBeforeDate : BeforeDateFeatureToggle
        {
            public BeforeDateRangeToggleInvalidBeforeDate() : base()
            {
            }

            public BeforeDateRangeToggleInvalidBeforeDate(IFeatureProvider provider) : base(provider)
            {
            }
        }


        class BeforeDateRangeToggleToggleTimeIsAfterNow : BeforeDateFeatureToggle
        {
            public BeforeDateRangeToggleToggleTimeIsAfterNow() : base()
            {
            }

            public BeforeDateRangeToggleToggleTimeIsAfterNow(IFeatureProvider provider) : base(provider)
            {
            }
        }

        class BeforeDateRangeToggleToggleTimeIsNow : BeforeDateFeatureToggle
        {
            public BeforeDateRangeToggleToggleTimeIsNow() : base()
            {
            }

            public BeforeDateRangeToggleToggleTimeIsNow(IFeatureProvider provider) : base(provider)
            {
            }
        }
    }
}
