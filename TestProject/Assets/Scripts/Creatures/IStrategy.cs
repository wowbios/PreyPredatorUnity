namespace Assets.Scripts.Creatures
{
    public interface IStrategy<in T>
        where T : CreatureBehavior
    {
        void Apply(T creature);
    }
}