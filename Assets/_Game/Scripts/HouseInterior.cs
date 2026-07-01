using System;
using UnityEngine;

namespace ThiefSignal
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class HouseInterior : MonoBehaviour
    {
        private BoxCollider2D _collider;

        public event Action ThiefEntered;
        public event Action ThiefExited;

        public Vector2 Size => _collider.bounds.size;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Thief _) == false)
                return;

            ThiefEntered?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Thief _) == false)
                return;

            ThiefExited?.Invoke();
        }
    }
}
