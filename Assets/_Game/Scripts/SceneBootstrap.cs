using UnityEngine;

namespace ThiefSignal
{
    public class SceneBootstrap : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private float _cameraSize = 6f;
        [SerializeField] private Vector3 _cameraPosition = new Vector3(0f, -1f, -10f);
        [SerializeField] private Color _backgroundColor = new Color(0.15f, 0.18f, 0.22f);

        [Header("House")]
        [SerializeField] private Vector2 _houseCenter = Vector2.zero;
        [SerializeField] private Vector2 _houseSize = new Vector2(6f, 4f);
        [SerializeField] private float _wallThickness = 0.3f;
        [SerializeField] private float _doorWidth = 1.2f;

        [Header("Thief")]
        [SerializeField] private float _thiefSpeed = 2.5f;
        [SerializeField] private float _thiefSize = 0.6f;

        [Header("Alarm")]
        [SerializeField] private float _alarmRampSpeed = 0.6f;

        private void Start()
        {
            SpriteAssets assets = new SpriteAssets();

            CameraBuilder.Configure(_cameraSize, _cameraPosition, _backgroundColor);

            AlarmController alarm = AlarmBuilder.Build(_alarmRampSpeed);

            HouseConfig houseConfig = new HouseConfig(_houseCenter, _houseSize, _wallThickness, _doorWidth);
            HouseBuilder.Build(assets, houseConfig, alarm);

            ThiefConfig thiefConfig = new ThiefConfig(_thiefSpeed, _thiefSize);
            ThiefBuilder.Build(assets, thiefConfig, _houseSize);
        }
    }
}
