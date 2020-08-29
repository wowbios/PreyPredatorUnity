using System;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Creatures
{
    public class CarnivoreBehavior : CreatureBehavior
    {
        public CarnivoreState State = CarnivoreState.Searching;
        public GameObject RadiusSprite;
        private GameObject _victim;

        public override void Start()
        {
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
            switch (State)
            {
                case CarnivoreState.Searching: SearchForEnemy(); break;
                case CarnivoreState.Chasing: Chase(); break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Herbivore"))
            {
                Destroy(collision.gameObject);
                ChangeState(CarnivoreState.Searching);
            }
        }

        private GameObject FindClosestVictim() => FindClosest("Herbivore");

        private void SearchForEnemy()
        {
            Search();

            _victim = FindClosestVictim();

            if (_victim != null) ChangeState(CarnivoreState.Chasing);
        }

        private void Chase()
        {
            if (Vector2.Distance(transform.position, _victim.transform.position) > VisionRadius)
            {
                _victim = null;
                ChangeState(CarnivoreState.Searching);
                return;
            }

            MoveTo(_victim.transform);
        }

        private void ChangeState(CarnivoreState state) => State = state;
    }
}
