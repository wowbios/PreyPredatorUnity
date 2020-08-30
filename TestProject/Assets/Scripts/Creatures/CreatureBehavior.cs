using System;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public abstract class CreatureBehavior : MonoBehaviour
    {
        public float Speed = .1f;
        public float VisionRadius = 2f;

        public virtual void Start()
        {
        }

        public GameObject FindClosest(EntityType type)
        {
            string targetTag = GetTagByEntityType(type);
            GameObject[] entities = GameObject.FindGameObjectsWithTag(targetTag);
            GameObject closestEntity = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (GameObject entity in entities)
            {
                float distance = Vector3.Distance(entity.transform.position, currentPos);
                if (distance < minDist && distance <= VisionRadius)
                {
                    closestEntity = entity;
                    minDist = distance;
                }
            }

            if (minDist > VisionRadius)
                return null;

            return closestEntity;
        }

        private string GetTagByEntityType(EntityType type) => type.ToString();

        public void Move(Vector2 targetPos, float speedModifier = 1)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPos,
                Speed * Time.deltaTime * speedModifier);
        }

        public void MoveTo(Vector2 targetPosition)
        {
            Move(targetPosition);
            RotateToTarget(targetPosition);
        }

        public void MoveTo(GameObject target) => MoveTo(target.transform.position);

        public void MoveFrom(GameObject target) => MoveFrom(target.transform.position);

        private void MoveFrom(Vector2 targetPosition)
        {
            Move(targetPosition, -1);
            RotateFromTarget(targetPosition);
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
    }
}