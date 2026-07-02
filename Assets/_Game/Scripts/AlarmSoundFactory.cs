using UnityEngine;

namespace ThiefSignal
{
    public class AlarmSoundFactory
    {
        private const int SampleRate = 44100;
        private const float Duration = 2f;
        private const float LowFrequency = 600f;
        private const float HighFrequency = 900f;
        private const float TonePeriod = 0.5f;
        private const float Amplitude = 0.25f;

        public AudioClip CreateSiren()
        {
            int totalSamples = Mathf.RoundToInt(SampleRate * Duration);
            float[] samples = new float[totalSamples];

            for (int i = 0; i < totalSamples; i++)
                samples[i] = SampleValueAt(i);

            AudioClip clip = AudioClip.Create("AlarmSiren", totalSamples, 1, SampleRate, false);
            clip.SetData(samples, 0);

            return clip;
        }

        private float SampleValueAt(int sampleIndex)
        {
            float time = sampleIndex / (float)SampleRate;
            int toneIndex = Mathf.FloorToInt(time / TonePeriod);

            bool isLowTone = toneIndex % 2 == 0;
            float frequency = isLowTone ? LowFrequency : HighFrequency;

            float phase = (time * frequency) % 1f;
            float wave = phase < 0.5f ? 1f : -1f;

            return wave * Amplitude;
        }
    }
}
