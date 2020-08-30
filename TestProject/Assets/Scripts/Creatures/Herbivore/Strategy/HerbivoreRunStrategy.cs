using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public class HerbivoreRunStrategy : IStrategy<HerbivoreBehavior>
    {
        public void Apply(HerbivoreBehavior creature)
        {
            throw new System.NotImplementedException();
        }

        public static GameObject FindClosestEnemy(HerbivoreBehavior herbivore)
        {

        }
    }
}