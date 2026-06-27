using UnityEngine;

namespace ThiefSignal
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Thief : MonoBehaviour
    {
        private const float ArrivalThreshold = 0.05f;
        private const float ArrivalThresholdSquared = ArrivalThreshold * ArrivalThreshold;

        private Vector2[] _waypoints = System.Array.Empty<Vector2>();
        private float _speed;
        private int _currentWaypointIndex;
        private bool _isMoving;

        private void Update()
        {
            if (_isMoving == false)
                return;

            MoveTowardsCurrentWaypoint();
        }

        public void Configure(Vector2[] waypoints, float speed)
        {
            _waypoints = waypoints;
            _speed = speed;
            InitializeMovement();
        }

        private void InitializeMovement()
        {
            _isMoving = _waypoints.Length > 0;

            if (_isMoving)
                transform.position = _waypoints[0];
        }

        private void MoveTowardsCurrentWaypoint()
        {
            Vector2 target = _waypoints[_currentWaypointIndex];

            Vector2 newPosition = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            transform.position = newPosition;

            Vector2 toTarget = target - newPosition;

            if (toTarget.sqrMagnitude <= ArrivalThresholdSquared)
                AdvanceWaypoint();
        }

        private void AdvanceWaypoint()
        {
            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
        }
    }
}
