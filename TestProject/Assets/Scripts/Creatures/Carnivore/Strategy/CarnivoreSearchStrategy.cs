using System;

namespace Assets.Scripts.Creatures
{
    [Serializable]
    public class CarnivoreSearchStrategy : SearchStrategy
    {
        public CarnivoreSearchStrategy(float changeInterval) : base(changeInterval)
        {

        }

        public override void Apply(CreatureBehavior creature)
        {
            if (creature is CarnivoreBehavior carnivore)
            {
                if (carnivore.FindClosest(EntityType.Herbivore) != null)
                {
                    carnivore.ChangeState(CarnivoreState.Chasing);
                    return;
                }
            }
            base.Apply(creature);
        }
    }
}