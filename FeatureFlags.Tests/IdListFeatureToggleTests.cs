using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags.Toggles;
using NUnit.Framework;

namespace FeatureFlags.Tests
{
    public class IdListFeatureToggleTests
    {
        private static int SpaId = 119;

        [Test]
        public void IdList_NoIntegegers_ReturnsException()
        {
            var toggle = new IdList_NoItegers();

            Assert.Throws<ArgumentException>(() =>
            {
                var isEnabled = toggle.Enabled;
            });
        }

        [Test]
        public void IdList_SingleInteger_SpaInList_ReturnsTrue()
        {
            var toggle = new IdList_SingleInteger_IdInList();

            Assert.That(toggle.Enabled, Is.True);
        }

        [Test]
        public void IdList_ListOfIntegers_SpaInList_ReturnsTrue()
        {
            var toggle = new IdList_ListOfIntegers_IdInList();

            Assert.That(toggle.Enabled, Is.True);
        }

        [Test]
        public void IdList_SingleInteger_SpaNotInList_ReturnsFalse()
        {
            var toggle = new IdList_SingleInteger_IdNotInList();

            Assert.That(toggle.Enabled, Is.False);
        }

        [Test]
        public void IdList_ListOfIntegers_SpaNotInList_ReturnsFalse()
        {
            var toggle = new IdList_ListOfIntegers_IdNotInList();

            Assert.That(toggle.Enabled, Is.False);
        }











        class IdList_NoItegers : IdListFeatureToggle
        {
            public IdList_NoItegers(int spaId) : base(spaId) { }
            public IdList_NoItegers() : base(new IdListProvider(), SpaId, FeatureFlags.Seperator.Comma) { }
        }
        class IdList_SingleInteger_IdInList : IdListFeatureToggle
        {
            public IdList_SingleInteger_IdInList(int spaId) : base(spaId) { }
            public IdList_SingleInteger_IdInList() : base(new IdListProvider(), SpaId, FeatureFlags.Seperator.Comma) { }
        }

        class IdList_ListOfIntegers_IdInList : IdListFeatureToggle
        {
            public IdList_ListOfIntegers_IdInList(int spaId) : base(spaId) { }
            public IdList_ListOfIntegers_IdInList() : base(new IdListProvider(), SpaId, FeatureFlags.Seperator.Comma) { }
        }
        class IdList_SingleInteger_IdNotInList : IdListFeatureToggle
        {
            public IdList_SingleInteger_IdNotInList(int spaId) : base(spaId) { }
            public IdList_SingleInteger_IdNotInList() : base(new IdListProvider(), SpaId, FeatureFlags.Seperator.Comma) { }
        }

        class IdList_ListOfIntegers_IdNotInList : IdListFeatureToggle
        {
            public IdList_ListOfIntegers_IdNotInList(int spaId) : base(spaId) { }
            public IdList_ListOfIntegers_IdNotInList() : base(new IdListProvider(), SpaId, FeatureFlags.Seperator.Comma) { }
        }






        public class IdListProvider : IFeatureProvider
        {
            public IdListProvider()
            {
                var noItegers = new IdList_NoItegers(SpaId);
                var singleIntInList = new IdList_SingleInteger_IdInList(SpaId);
                var listOfIntsInList = new IdList_ListOfIntegers_IdInList(SpaId);
                var singleIntNotInList = new IdList_SingleInteger_IdNotInList(SpaId);
                var listOfIntsNotInList = new IdList_ListOfIntegers_IdNotInList(SpaId);

                kvp = new Dictionary<string, string>();
                kvp.Add(noItegers.Key, "NoIntegers");
                kvp.Add(singleIntInList.Key, "119");
                kvp.Add(listOfIntsInList.Key, "900,12,-2,119");
                kvp.Add(singleIntNotInList.Key, "200");
                kvp.Add(listOfIntsNotInList.Key, "900,12,-2,120");
            }

            private Dictionary<string, string> kvp;
            public string GetValue(string key)
            {
                return kvp[key];
            }
        }
    }
}
