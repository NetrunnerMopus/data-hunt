using System.Collections.Generic;

namespace model.play
{
    public class Ability : IPayabilityObserver, IImpactObserver
    {
        public readonly ICost cost;
        public readonly IEffect effect;
        private HashSet<IUsabilityObserver> usabilities = new HashSet<IUsabilityObserver>();
        private HashSet<IResolutionObserver> resolutions = new HashSet<IResolutionObserver>();
        private bool payable = false;
        private bool impactful = false;
        private bool Usable => payable && impactful;

        public Ability(ICost cost, IEffect effect)
        {
            this.cost = cost;
            this.effect = effect;
        }

        public void Trigger(Game game)
        {
            cost.Pay(game);
            effect.Resolve(game);
            foreach (var observer in resolutions)
            {
                observer.NotifyResolved();
            }
        }

        public void ObserveUsability(IUsabilityObserver observer, Game game)
        {
            usabilities.Add(observer);
            cost.Observe(this, game);
            effect.Observe(this, game);
        }

        public void ObserveResolution(IResolutionObserver observer)
        {
            resolutions.Add(observer);
        }

        public void Unobserve(IUsabilityObserver observer)
        {
            usabilities.Remove(observer);
        }

        void IPayabilityObserver.NotifyPayable(bool payable, ICost source)
        {
            this.payable = payable;
            Update();
        }

        void IImpactObserver.NotifyImpact(bool impactful, IEffect source)
        {
            this.impactful = impactful;
            Update();
        }

        private void Update()
        {
            foreach (var observer in usabilities)
            {
                observer.NotifyUsable(Usable);
            }
        }
    }

    public interface IUsabilityObserver
    {
        void NotifyUsable(bool usable);
    }

    public interface IResolutionObserver
    {
        void NotifyResolved();
    }
}