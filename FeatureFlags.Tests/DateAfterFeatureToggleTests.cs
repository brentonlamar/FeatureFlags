using System;
using System.Collections.Generic;
using FeatureFlags.Toggles;
using NUnit.Framework;

namespace FeatureFlags.Tests
{
    class DateAfterFeatureToggleTests
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
        public void TimeNow_AfterDate()
        {
            var toggle = new DateRangeToggle_AfterNow(new DatesProvider());
            Assert.That(toggle.Enabled, Is.True);
        }

        [Test]
        public void TimeNow_BeforeDate()
        {
            var toggle = new DateRangeToggle_AfterNow(new DatesProvider());
            Assert.That(toggle.Enabled, Is.True);
        }


        private class DatesProvider : IFeatureProvider
        {
            public DatesProvider()
            {
                var invalidNumberOfDates = new DateRangeToggle_InvalidDate();

                var afterNowToggle = new DateRangeToggle_AfterNow();
                var beforeNowToggle = new DateRangeToggle_BeforeNow();


                var beforeNow = DateTime.Now.AddMinutes(-10).ToLongTimeString();
                var afterNow = DateTime.Now.AddMinutes(10).ToLongTimeString();

                _kvp = new Dictionary<string, string>
                {
                    {invalidNumberOfDates.Key, "InvalidDate"},
                    {afterNowToggle.Key, afterNow},
                    {beforeNowToggle.Key, beforeNow},

                };
            }

            private readonly Dictionary<string, string> _kvp;


            public string GetValue(string key)
            {
                return _kvp[key];
            }
        }

        class DateRangeToggle_InvalidDate : DateAfterFeatureToggle
        {
            public DateRangeToggle_InvalidDate() : base() { }
            public DateRangeToggle_InvalidDate(IFeatureProvider provider) : base(provider) { }
        }


        class DateRangeToggle_AfterNow : DateAfterFeatureToggle
        {
            public DateRangeToggle_AfterNow() : base() { }
            public DateRangeToggle_AfterNow(IFeatureProvider provider) : base(provider) { }
        }
        class DateRangeToggle_BeforeNow : DateAfterFeatureToggle
        {
            public DateRangeToggle_BeforeNow() : base() { }
            public DateRangeToggle_BeforeNow(IFeatureProvider provider) : base(provider) { }
        }
    }
}
