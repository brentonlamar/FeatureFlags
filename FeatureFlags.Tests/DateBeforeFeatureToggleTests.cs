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
            var toggle = new DateRangeToggle_InvalidDate(new DatesProvider());
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
            var toggle = new DateRangeToggle_ToggleTimeIsAfterNow(new DatesProvider());
            Assert.That(toggle.Enabled, Is.True);
        }

        [Test]
        public void ToggleTimeIsBeforeNow()
        {
            var toggle = new DateRangeToggle_ToggleTimeIsBeforeNow(new DatesProvider());
            Assert.That(toggle.Enabled, Is.False);
        }


        private class DatesProvider : IFeatureProvider
        {
            public DatesProvider()
            {
                var invalidNumberOfDates = new DateRangeToggle_InvalidDate();

                var afterNowToggle = new DateRangeToggle_ToggleTimeIsAfterNow();
                var beforeNowToggle = new DateRangeToggle_ToggleTimeIsBeforeNow();


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

        class DateRangeToggle_InvalidDate : DateBeforeFeatureToggle
        {
            public DateRangeToggle_InvalidDate() : base()
            {
            }

            public DateRangeToggle_InvalidDate(IFeatureProvider provider) : base(provider)
            {
            }
        }


        class DateRangeToggle_ToggleTimeIsAfterNow : DateBeforeFeatureToggle
        {
            public DateRangeToggle_ToggleTimeIsAfterNow() : base()
            {
            }

            public DateRangeToggle_ToggleTimeIsAfterNow(IFeatureProvider provider) : base(provider)
            {
            }
        }

        class DateRangeToggle_ToggleTimeIsBeforeNow : DateBeforeFeatureToggle
        {
            public DateRangeToggle_ToggleTimeIsBeforeNow() : base()
            {
            }

            public DateRangeToggle_ToggleTimeIsBeforeNow(IFeatureProvider provider) : base(provider)
            {
            }
        }
    }
}
