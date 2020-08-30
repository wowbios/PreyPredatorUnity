using System;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    [Serializable]
    public class SearchStrategy : IStrategy<CreatureBehavior>
    {
        [SerializeField]
        private readonly float _changeInterval;
        [SerializeField]
        private DateTime _lastChangeStrategyTime;
        [SerializeField]
        private Vector2 _searchingDirection;

        public SearchStrategy(float changeInterval)
        {
            _changeInterval = changeInterval;

            InitializeSearch();
        }

        public virtual void Apply(CreatureBehavior creature)
        {
            var diff = (DateTime.Now - _lastChangeStrategyTime).TotalSeconds;
            // Debug.Log("Diff " + diff + " , Interval = " + _changeInterval);
            if (diff > _changeInterval)
                InitializeSearch();

            creature.MoveTo(_searchingDirection);
        }

        private void InitializeSearch()
        {
            // Debug.Log("Change strategy");
            _lastChangeStrategyTime = DateTime.Now;
            _searchingDirection = WorldController.Instance.GetRandomPosition();
        }
    }
}