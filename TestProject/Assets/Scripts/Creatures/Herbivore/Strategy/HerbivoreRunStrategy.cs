using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public class HerbivoreRunStrategy : IStrategy<HerbivoreBehavior>
    {
        public void Apply(HerbivoreBehavior herbivore)
        {
            GameObject enemy = herbivore.FindClosest(EntityType.Carnivore);
            if (enemy is null)
                herbivore.ChangeState(HerbivoreState.Searching);
            else
                herbivore.MoveFrom(enemy);
        }
    }
}