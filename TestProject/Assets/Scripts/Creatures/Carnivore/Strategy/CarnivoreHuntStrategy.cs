using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public class CarnivoreHuntStrategy : IStrategy<CarnivoreBehavior>
    {
        public void Apply(CarnivoreBehavior creature)
        {
            GameObject victim = creature.FindClosest(EntityType.Herbivore);
            if (victim is null)
                creature.ChangeState(CarnivoreState.Searching);
            else
                creature.MoveTo(victim);
        }


    }
}