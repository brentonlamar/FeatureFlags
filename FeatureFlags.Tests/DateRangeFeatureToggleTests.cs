using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags.Toggles;
using NUnit.Framework;

namespace FeatureFlags.Tests
{
    class DateRangeFeatureToggleTests
    {
        [Test]
        public void DateRange_InvalidNumberOfDates_ReturnsException()
        {
            // Arrange
            var toggle = new DateRangeToggle_InvalidNumberOfDates(new DatesProvider());
            // ACT

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var result = toggle.Enabled;
            });
        }

        [Test]
        public void DateRange_InvalidDates_ReturnsException()
        {
            // Arrange
            var toggle = new DateRangeToggle_InvalidDates(new DatesProvider());
            // ACT

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var result = toggle.Enabled;
            });
        }

        [Test]
        public void TimeNow_AfterFirstDate_BeforeSecondDate()
        {
            var toggle = new DateRangeToggle_TimeNowAfterFirstDateBeforeSecondDate(new DatesProvider());
            Assert.That(toggle.Enabled, Is.True);
        }

        [Test]
        public void TimeNow_AfterSecondDate_BeforeFirstDate()
        {
            var toggle = new DateRangeToggle_TimeNowAfterSecondDateBeforeFirstDate(new DatesProvider());
            Assert.That(toggle.Enabled, Is.True);
        }

        [Test]
        public void TimeNow_Before_Range()
        {
            var toggle = new DateRangeToggle_TimeNowBeforeRange(new DatesProvider());
            Assert.That(toggle.Enabled, Is.False);
        }

        [Test]
        public void TimeNow_After_Range()
        {
            var toggle = new DateRangeToggle_TimeNowAfterRange(new DatesProvider());
            Assert.That(toggle.Enabled, Is.False);
        }


        public class DatesProvider : IFeatureProvider
        {
            public DatesProvider()
            {
                var invalidNumberOfDates = new DateRangeToggle_InvalidNumberOfDates();
                var invalidDates = new DateRangeToggle_InvalidDates();

                var nowAfterFirstBeforeSecond = new DateRangeToggle_TimeNowAfterFirstDateBeforeSecondDate();
                var nowAfterSecondBeforeFirst = new DateRangeToggle_TimeNowAfterSecondDateBeforeFirstDate();

                var timeNowBeforeRange = new DateRangeToggle_TimeNowBeforeRange();
                var timeNowAfterRange = new DateRangeToggle_TimeNowAfterRange();

                var beforeNow = DateTime.Now.AddYears(-10).ToShortDateString();
                var afterNow = DateTime.Now.AddYears(10).ToLongDateString();

                kvp = new Dictionary<string, string>();
                kvp.Add(invalidNumberOfDates.Key, "2016-10-20");
                kvp.Add(invalidDates.Key, "2016-10-20|Invalid Date");
                kvp.Add(nowAfterFirstBeforeSecond.Key, $"{beforeNow}|{afterNow}");
                kvp.Add(nowAfterSecondBeforeFirst.Key, $"{afterNow}|{beforeNow}");
                kvp.Add(timeNowBeforeRange.Key, $"{afterNow}|{afterNow}");
                kvp.Add(timeNowAfterRange.Key, $"{beforeNow}|{beforeNow}");
            }

            public Dictionary<string, string> kvp;


            public string GetValue(string key)
            {
                return kvp[key];
            }
        }

        class DateRangeToggle_InvalidNumberOfDates : DateRangeFeatureToggle
        {
            public DateRangeToggle_InvalidNumberOfDates() : base() { }
            public DateRangeToggle_InvalidNumberOfDates(IFeatureProvider provider) : base(provider, FeatureFlags.Seperator.Pipe) { }
        }

        class DateRangeToggle_InvalidDates : DateRangeFeatureToggle
        {
            public DateRangeToggle_InvalidDates() : base() { }
            public DateRangeToggle_InvalidDates(IFeatureProvider provider) : base(provider, FeatureFlags.Seperator.Pipe) { }
        }
        class DateRangeToggle_TimeNowAfterFirstDateBeforeSecondDate : DateRangeFeatureToggle
        {
            public DateRangeToggle_TimeNowAfterFirstDateBeforeSecondDate() : base() { }
            public DateRangeToggle_TimeNowAfterFirstDateBeforeSecondDate(IFeatureProvider provider) : base(provider, FeatureFlags.Seperator.Pipe) { }
        }

        class DateRangeToggle_TimeNowAfterSecondDateBeforeFirstDate : DateRangeFeatureToggle
        {
            public DateRangeToggle_TimeNowAfterSecondDateBeforeFirstDate() : base() { }
            public DateRangeToggle_TimeNowAfterSecondDateBeforeFirstDate(IFeatureProvider provider) : base(provider, FeatureFlags.Seperator.Pipe) { }
        }

        class DateRangeToggle_TimeNowBeforeRange : DateRangeFeatureToggle
        {
            public DateRangeToggle_TimeNowBeforeRange() : base() { }
            public DateRangeToggle_TimeNowBeforeRange(IFeatureProvider provider) : base(provider, FeatureFlags.Seperator.Pipe) { }
        }

        class DateRangeToggle_TimeNowAfterRange : DateRangeFeatureToggle
        {
            public DateRangeToggle_TimeNowAfterRange() : base() { }
            public DateRangeToggle_TimeNowAfterRange(IFeatureProvider provider) : base(provider, FeatureFlags.Seperator.Pipe) { }
        }
    }
}
