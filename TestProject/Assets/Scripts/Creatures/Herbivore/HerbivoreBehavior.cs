using System;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public class HerbivoreBehavior : CreatureBehavior
    {
        public HerbivoreState State = HerbivoreState.Searching;

        private GameObject _enemy;

        public void FixedUpdate()
        {
            switch (State)
            {
                case HerbivoreState.Searching: SearchForEat(); break;
                case HerbivoreState.Running: RunFromEnemy(); break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RunFromEnemy()
        {
            if (Vector2.Distance(transform.position, _enemy.transform.position) > VisionRadius)
            {
                _enemy = null;
                ChangeState(HerbivoreState.Searching);
                return;
            }

            MoveFrom(_enemy.transform);
        }

        private GameObject FindClosestEnemy() => FindClosest("Carnivore");

        private void SearchForEat()
        {
            Search();

            _enemy = FindClosestEnemy();

            if (_enemy != null) ChangeState(HerbivoreState.Running);
        }

        private void ChangeState(HerbivoreState state) => State = state;
    }
}
