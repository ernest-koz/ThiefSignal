using UnityEngine;

namespace ThiefSignal
{
    public class SceneBootstrap : MonoBehaviour
    {
        private const float OutsideOffset = 3f;
        private const float BottomOffset = 1f;

        [SerializeField] private HouseInterior _housePrefab;
        [SerializeField] private AlarmController _alarmPrefab;
        [SerializeField] private Thief _thiefPrefab;

        private HouseInterior _house;
        private AlarmController _alarm;

        private void Awake()
        {
            _house = Instantiate(_housePrefab);
            _alarm = Instantiate(_alarmPrefab);

            Thief thief = Instantiate(_thiefPrefab);

            thief.Init(BuildWaypoints(_house.Size));
        }

        private void OnEnable()
        {
            _house.ThiefEntered += _alarm.Activate;
            _house.ThiefExited += _alarm.Deactivate;
        }

        private void OnDisable()
        {
            if (_house == null || _alarm == null)
                return;

            _house.ThiefEntered -= _alarm.Activate;
            _house.ThiefExited -= _alarm.Deactivate;
        }

        private static Vector2[] BuildWaypoints(Vector2 houseSize)
        {
            float halfWidth = houseSize.x * 0.5f;
            float outsideX = halfWidth + OutsideOffset;
            float bottomY = -houseSize.y - BottomOffset;

            return new Vector2[]
            {
                new Vector2(-outsideX, 0f),
                new Vector2(0f, 0f),
                new Vector2(outsideX, 0f),
                new Vector2(outsideX, bottomY),
                new Vector2(-outsideX, bottomY)
            };
        }
    }
}
