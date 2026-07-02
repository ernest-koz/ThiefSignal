using NUnit.Framework;
using UnityEngine;

namespace ThiefSignal.Tests
{
    [TestFixture]
    public class AlarmSoundFactoryTests
    {
        private readonly AlarmSoundFactory _factory = new AlarmSoundFactory();

        [Test]
        public void CreateSiren_ReturnsNonNullClip()
        {
            AudioClip clip = _factory.CreateSiren();

            Assert.NotNull(clip);
        }

        [Test]
        public void CreateSiren_HasStandardSampleRate()
        {
            AudioClip clip = _factory.CreateSiren();

            Assert.AreEqual(44100, clip.frequency);
        }

        [Test]
        public void CreateSiren_IsMonoChannel()
        {
            AudioClip clip = _factory.CreateSiren();

            Assert.AreEqual(1, clip.channels);
        }

        [Test]
        public void CreateSiren_HasPositiveLength()
        {
            AudioClip clip = _factory.CreateSiren();

            Assert.Greater(clip.length, 0f);
        }

        [Test]
        public void CreateSiren_SampleCountMatchesRateAndDuration()
        {
            AudioClip clip = _factory.CreateSiren();

            int expectedSamples = Mathf.RoundToInt(clip.frequency * clip.length);

            Assert.AreEqual(expectedSamples, clip.samples);
        }
    }
}
