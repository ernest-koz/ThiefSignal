using UnityEngine;

namespace ThiefSignal
{
    [RequireComponent(typeof(Collider2D))]
    public class HouseInterior : MonoBehaviour
    {
        [SerializeField] private AlarmController _alarm;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Thief thief) == false)
                return;

            _alarm.SetAlarmActive(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Thief thief) == false)
                return;

            _alarm.SetAlarmActive(false);
        }

        public void Init(AlarmController alarm) =>
            _alarm = alarm;
    }
}
