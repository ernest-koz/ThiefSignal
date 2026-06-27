using UnityEngine;

namespace ThiefSignal
{
    [RequireComponent(typeof(AudioSource))]
    public class AlarmController : MonoBehaviour
    {
        private const float MaximumVolume = 1f;
        private const float MinimumVolume = 0f;

        private AudioSource _audioSource;
        private float _rampSpeed;
        private float _targetVolume;

        public float CurrentVolume => _audioSource.volume;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = MinimumVolume;
            _targetVolume = MinimumVolume;
        }

        private void Update()
        {
            _audioSource.volume = Mathf.MoveTowards(
                _audioSource.volume,
                _targetVolume,
                _rampSpeed * Time.deltaTime);
        }

        public void Configure(AudioClip clip, float rampSpeed)
        {
            _rampSpeed = rampSpeed;
            _audioSource.clip = clip;
            _audioSource.loop = true;
            _audioSource.playOnAwake = false;
            _audioSource.Play();
        }

        public void SetAlarmActive(bool isActive) =>
            _targetVolume = isActive ? MaximumVolume : MinimumVolume;
    }
}
