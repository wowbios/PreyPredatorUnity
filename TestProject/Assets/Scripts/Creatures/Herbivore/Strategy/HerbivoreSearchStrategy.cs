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
                if (herbivore.FindClosest(EntityType.Carnivore) != null)
                {
                    herbivore.ChangeState(HerbivoreState.Running);
                    return;
                }

                if (herbivore.FindClosest(EntityType.Food) != null)
                {
                    herbivore.ChangeState(HerbivoreState.GoForEat);
                    return;
                }
            }

            base.Apply(creature);
        }
    }
}