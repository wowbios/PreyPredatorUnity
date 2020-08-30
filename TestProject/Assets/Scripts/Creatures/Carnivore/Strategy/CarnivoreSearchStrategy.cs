namespace Assets.Scripts.Creatures
{
    public class CarnivoreSearchStrategy : SearchStrategy
    {
        public CarnivoreSearchStrategy(float changeInterval) : base(changeInterval)
        {

        }

        public override void Apply(CreatureBehavior creature)
        {
            if (creature is CarnivoreBehavior carnivore)
            {
                if (CarnivoreHuntStrategy.FindClosestHerbivore(carnivore) != null)
                {
                    carnivore.ChangeState(CarnivoreState.Chasing);
                    return;
                }
            }
            base.Apply(creature);
        }
    }
}