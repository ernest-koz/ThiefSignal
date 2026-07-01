using System.Collections;
using UnityEngine;

namespace ThiefSignal
{
    [RequireComponent(typeof(AudioSource))]
    public class AlarmController : MonoBehaviour
    {
        private const float MaximumVolume = 1f;
        private const float MinimumVolume = 0f;

        [SerializeField] private float _rampSpeed = 0.6f;

        private AudioSource _audioSource;
        private Coroutine _fadeRoutine;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = AlarmSoundFactory.CreateSiren();
            _audioSource.loop = true;
            _audioSource.playOnAwake = false;
            _audioSource.volume = MinimumVolume;
            _audioSource.Play();
        }

        public void Activate()
        {
            StopFade();

            _fadeRoutine = StartCoroutine(FadeRoutine(MaximumVolume));
        }

        public void Deactivate()
        {
            StopFade();

            _fadeRoutine = StartCoroutine(FadeRoutine(MinimumVolume));
        }

        private void StopFade()
        {
            if (_fadeRoutine == null)
                return;

            StopCoroutine(_fadeRoutine);

            _fadeRoutine = null;
        }

        private IEnumerator FadeRoutine(float targetVolume)
        {
            while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
            {
                _audioSource.volume = Mathf.MoveTowards(
                    _audioSource.volume, targetVolume, _rampSpeed * Time.deltaTime);

                yield return null;
            }

            _fadeRoutine = null;
        }
    }
}
