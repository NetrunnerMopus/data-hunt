namespace model.effects
{
    public class Nothing : IEffect
    {
        void IEffect.Resolve(Game game)
        {
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(false, this);
        }
    }
}