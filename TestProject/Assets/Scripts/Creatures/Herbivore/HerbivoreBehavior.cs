using System;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public class HerbivoreBehavior : CreatureBehavior
    {
        public HerbivoreState State = HerbivoreState.Searching;
        public float HerbivoreChangeStrategySeconds = 2;

        private HerbivoreRunStrategy _runStrategy;
        private HerbivoreSearchStrategy _searchStrategy;
        private HerbivoreGoForEatStrategy _goForEatStrategy;

        public override void Start()
        {
            _runStrategy = new HerbivoreRunStrategy();
            _searchStrategy = new HerbivoreSearchStrategy(HerbivoreChangeStrategySeconds);
            _goForEatStrategy = new HerbivoreGoForEatStrategy();

            base.Start();
        }

        public void FixedUpdate()
        {
            GetStrategy()?.Apply(this);
        }

        private IStrategy<HerbivoreBehavior> GetStrategy()
        {
            switch (State)
            {
                case HerbivoreState.Running:
                    return _runStrategy;
                case HerbivoreState.Searching:
                    return _searchStrategy;
                case HerbivoreState.GoForEat:
                    return _goForEatStrategy;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Food")) Destroy(collision.gameObject);
        }

        public void ChangeState(HerbivoreState state) => State = state;
    }
}
