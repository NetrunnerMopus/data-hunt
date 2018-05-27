namespace model
{
    public interface IEffect
    {
        void Resolve(Game game);
        void Observe(IImpactObserver observer, Game game);
    }

    public interface IImpactObserver
    {
        void NotifyImpact(bool impactful, IEffect source);
    }
}