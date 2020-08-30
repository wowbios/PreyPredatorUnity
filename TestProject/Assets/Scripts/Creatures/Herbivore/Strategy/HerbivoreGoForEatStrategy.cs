using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public class HerbivoreGoForEatStrategy : IStrategy<HerbivoreBehavior>
    {
        public void Apply(HerbivoreBehavior herbivore)
        {
            GameObject food = herbivore.FindClosest(EntityType.Food);
            if (food is null)
                herbivore.ChangeState(HerbivoreState.Searching);
            else
                herbivore.MoveTo(food);
        }
    }
}