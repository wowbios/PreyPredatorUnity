using System;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public abstract class CreatureBehavior : MonoBehaviour
    {
        public float Speed = .1f;
        public float VisionRadius = 2f;
        public float ChangeStrategyIntervalSeconds = 2;

        private DateTime _lastChangeStrategyTime;
        private Vector2 _searchingDirection;
        private Rigidbody2D _rigidbody;

        public virtual void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>() ?? throw new Exception("Rigidbody2D required");
            _lastChangeStrategyTime = DateTime.Now;
            _searchingDirection = GetNewSearchingDirection();
        }

        protected void Search()
        {
            if ((DateTime.Now - _lastChangeStrategyTime).Seconds > ChangeStrategyIntervalSeconds)
            {
                _lastChangeStrategyTime = DateTime.Now;
                _searchingDirection = GetNewSearchingDirection();
            }

            transform.position = Vector2.MoveTowards(transform.position, _searchingDirection, Speed * Time.deltaTime);
            RotateToTarget(_searchingDirection);
        }

        protected GameObject FindClosest(string targetTag)
        {
            GameObject[] victims = GameObject.FindGameObjectsWithTag(targetTag);
            GameObject closest = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (GameObject creature in victims)
            {
                float distance = Vector3.Distance(creature.transform.position, currentPos);
                if (distance < minDist && distance <= VisionRadius)
                {
                    closest = creature;
                    minDist = distance;
                }
            }

            return closest;
        }

        protected void MoveTo(Transform targetTransform)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetTransform.position,
                Speed * Time.deltaTime);
            RotateToTarget(targetTransform.position);
        }

        protected void MoveFrom(Transform targetTransform)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetTransform.position,
                -Speed * Time.deltaTime);
            RotateFromTarget(targetTransform.position);
        }

        private void RotateToTarget(Vector2 target)=> RotateTo(target, 270f);

        private void RotateFromTarget(Vector2 target) => RotateTo(target, 180f);

        private void RotateTo(Vector2 target, float offset)
        {
            Vector2 direction = (Vector2)target - (Vector2)transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        }

        private static Vector2 GetNewSearchingDirection() => WorldController.Instance.GetRandomPosition();
    }
}