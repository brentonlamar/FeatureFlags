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
            var toggle = new AfterDateRangeToggleInvalidAfterDate(new DatesProvider());
            // ACT

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var result = toggle.Enabled;
            });
        }



        [Test]
        public void ToggleTimeIsAfterNow_ReturnsFase()
        {
            var toggle = new AfterDateRangeToggleNow(new DatesProvider());
            Assert.That(toggle.Enabled, Is.False);
        }

        [Test]
        public void ToggleTimeIsBeforeNow_ReturnsTrue()
        {
            var toggle = new AfterDateRangeToggleBeforeNow(new DatesProvider());
            Assert.That(toggle.Enabled, Is.True);
        }


        private class DatesProvider : IFeatureProvider
        {
            public DatesProvider()
            {
                var invalidNumberOfDates = new AfterDateRangeToggleInvalidAfterDate();

                var afterNowToggle = new AfterDateRangeToggleNow();
                var beforeNowToggle = new AfterDateRangeToggleBeforeNow();


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

        class AfterDateRangeToggleInvalidAfterDate : AfterDateFeatureToggle
        {
            public AfterDateRangeToggleInvalidAfterDate() : base() { }
            public AfterDateRangeToggleInvalidAfterDate(IFeatureProvider provider) : base(provider) { }
        }


        class AfterDateRangeToggleNow : AfterDateFeatureToggle
        {
            public AfterDateRangeToggleNow() : base() { }
            public AfterDateRangeToggleNow(IFeatureProvider provider) : base(provider) { }
        }
        class AfterDateRangeToggleBeforeNow : AfterDateFeatureToggle
        {
            public AfterDateRangeToggleBeforeNow() : base() { }
            public AfterDateRangeToggleBeforeNow(IFeatureProvider provider) : base(provider) { }
        }
    }
}
