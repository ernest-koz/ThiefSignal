using NUnit.Framework;
using UnityEngine;

namespace ThiefSignal.Tests
{
    [TestFixture]
    public class AlarmSoundFactoryTests
    {
        [Test]
        public void CreateSiren_ReturnsNonNullClip()
        {
            AudioClip clip = AlarmSoundFactory.CreateSiren();

            Assert.NotNull(clip);
        }

        [Test]
        public void CreateSiren_HasStandardSampleRate()
        {
            AudioClip clip = AlarmSoundFactory.CreateSiren();

            Assert.AreEqual(44100, clip.frequency);
        }

        [Test]
        public void CreateSiren_IsMonoChannel()
        {
            AudioClip clip = AlarmSoundFactory.CreateSiren();

            Assert.AreEqual(1, clip.channels);
        }

        [Test]
        public void CreateSiren_HasPositiveLength()
        {
            AudioClip clip = AlarmSoundFactory.CreateSiren();

            Assert.Greater(clip.length, 0f);
        }

        [Test]
        public void CreateSiren_SampleCountMatchesRateAndDuration()
        {
            AudioClip clip = AlarmSoundFactory.CreateSiren();

            int expectedSamples = Mathf.RoundToInt(clip.frequency * clip.length);

            Assert.AreEqual(expectedSamples, clip.samples);
        }
    }
}
