using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureFlags.Toggles;
using NUnit.Framework;

namespace FeatureFlags.Tests
{
    public class BoolFeatureToggleTests
    {
        [Test]
        public void ClassNameIsConfigKey()
        {
            // Arrange
            var toggle = new TestFeatureToggle_Bool();

            // Act

            // Assert
            Assert.That(toggle.Key, Is.EqualTo("FeatureFlags.TestFeatureToggle_Bool"));
        }

        [Test]
        public void MissingConfigThrowsException()
        {
            // Arrange
            var toggle = new MissingFeatureToggle();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => { var isEnabled = toggle.Enabled; });
        }

        [Test]
        public void FoundConfig_TestTrueValue()
        {
            // Arrange
            var toggle = new TestFeatureTrueToggle_Bool();

            // Act

            // Assert
            Assert.That(toggle.Key, Is.EqualTo("FeatureFlags.TestFeatureTrueToggle_Bool"));
            Assert.That(toggle.Enabled, Is.True);
        }

        [Test]
        public void FoundConfig_TestFalseValue()
        {
            // Arrange
            var toggle = new TestFeatureFalseToggle_Bool();

            // Act

            // Assert
            Assert.That(toggle.Key, Is.EqualTo("FeatureFlags.TestFeatureFalseToggle_Bool"));
            Assert.That(toggle.Enabled, Is.False);
        }

        [Test]
        public void FoundConfig_Test1Value()
        {
            // Arrange
            var toggle = new TestFeature1Toggle_Bool();

            // Act

            // Assert
            Assert.That(toggle.Key, Is.EqualTo("FeatureFlags.TestFeature1Toggle_Bool"));
            Assert.That(toggle.Enabled, Is.True);
        }

        [Test]
        public void FoundConfig_Test0Value()
        {
            // Arrange
            var toggle = new TestFeature0Toggle_Bool();

            // Act

            // Assert
            Assert.That(toggle.Key, Is.EqualTo("FeatureFlags.TestFeature0Toggle_Bool"));
            Assert.That(toggle.Enabled, Is.False);
        }

        [Test]
        public void FoundConfig_TestInvalidValue()
        {
            // Arrange
            var toggle = new TestFeatureInvalidToggle_Bool();

            // Act

            // Assert
            Assert.That(toggle.Key, Is.EqualTo("FeatureFlags.TestFeatureInvalidToggle_Bool"));
            Assert.Throws<FormatException>(() => { var isEnabled = toggle.Enabled; });
        }

        

        public class MissingFeatureToggle : BoolFeatureToggle { }

        public class TestFeatureToggle_Bool : BoolFeatureToggle
        {
        }

        public class TestFeatureTrueToggle_Bool : BoolFeatureToggle { }
        public class TestFeatureFalseToggle_Bool : BoolFeatureToggle { }
        public class TestFeature1Toggle_Bool : BoolFeatureToggle { }
        public class TestFeature0Toggle_Bool : BoolFeatureToggle { }
        public class TestFeatureInvalidToggle_Bool : BoolFeatureToggle { }
    }
}
