using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public class CarnivoreHuntStrategy : IStrategy<CarnivoreBehavior>
    {
        public void Apply(CarnivoreBehavior creature)
        {
            GameObject victim = FindClosestHerbivore(creature);
            if (victim is null)
                creature.ChangeState(CarnivoreState.Searching);
            else
                creature.MoveTo(victim);
        }

        public static GameObject FindClosestHerbivore(CarnivoreBehavior carnivore)
        {
            GameObject victim = carnivore.FindClosest("Herbivore");
            return Vector2.Distance(
                    carnivore.transform.position,
                    victim.transform.position)
                <= carnivore.VisionRadius
                    ? victim
                    : null;
        }
    }
}