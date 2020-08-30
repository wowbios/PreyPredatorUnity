using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public class HerbivoreSearchStrategy : SearchStrategy
    {
        public HerbivoreSearchStrategy(float changeInterval) : base(changeInterval)
        {
        }

        public override void Apply(CreatureBehavior creature)
        {
            if (creature is HerbivoreBehavior herbivore)
            {
                if (HerbivoreRunStrategy.FindClosestEnemy(herbivore) != null)
                {
                    herbivore.ChangeState(HerbivoreState.Running);
                }
                else if (FindClosestFood(herbivore) != null)
                {
                    herbivore.ChangeState(HerbivoreState.GoForEat);
                }
            }

            base.Apply(creature);
        }

        public static GameObject FindClosestFood(HerbivoreBehavior herbivore)
            => herbivore.FindClosest("Food");
    }
}