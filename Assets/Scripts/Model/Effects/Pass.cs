namespace model.effects
{
    public class Pass : IEffect
    {
        void IEffect.Resolve(Game game) { }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}