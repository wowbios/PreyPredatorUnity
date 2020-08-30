using System;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public class CarnivoreBehavior : CreatureBehavior
    {
        public CarnivoreState State = CarnivoreState.Searching;
        public GameObject RadiusSprite;
        public float CarnivoreChangeStrategySeconds = 2;

        private CarnivoreHuntStrategy _huntStrategy;
        [SerializeField]
        private CarnivoreSearchStrategy _searchStrategy;

        public override void Start()
        {
            _huntStrategy = new CarnivoreHuntStrategy();
            _searchStrategy = new CarnivoreSearchStrategy(CarnivoreChangeStrategySeconds);

            base.Start();

            RescaleRadiusSprite();
        }

        private void RescaleRadiusSprite()
        {
            Vector2 spriteSize = RadiusSprite.GetComponent<SpriteRenderer>().size;
            Vector2 scale = new Vector2(VisionRadius, VisionRadius) / spriteSize;
            RadiusSprite.transform.localScale = scale;
        }

        public void FixedUpdate()
        {
            GetStrategy()?.Apply(this);
        }

        private IStrategy<CarnivoreBehavior> GetStrategy()
        {
            switch (State)
            {
                case CarnivoreState.Searching: return _searchStrategy;
                case CarnivoreState.Chasing: return _huntStrategy;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Herbivore")) Destroy(collision.gameObject);
        }

        public void ChangeState(CarnivoreState state) => State = state;
    }
}
