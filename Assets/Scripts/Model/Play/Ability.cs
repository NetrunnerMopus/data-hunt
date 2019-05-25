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
        public bool Usable => payable && impactful;

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
                observer.NotifyUsable(Usable, this);
            }
        }

        public override bool Equals(object obj)
        {
            var ability = obj as Ability;
            return ability != null &&
                   EqualityComparer<ICost>.Default.Equals(cost, ability.cost) &&
                   EqualityComparer<IEffect>.Default.Equals(effect, ability.effect);
        }

        public override int GetHashCode()
        {
            var hashCode = 1540118860;
            hashCode = hashCode * -1521134295 + EqualityComparer<ICost>.Default.GetHashCode(cost);
            hashCode = hashCode * -1521134295 + EqualityComparer<IEffect>.Default.GetHashCode(effect);
            return hashCode;
        }

        public override string ToString() => "Ability(cost=" + cost + ", effect=" + effect + ")";
    }

    public interface IUsabilityObserver
    {
        void NotifyUsable(bool usable, Ability ability);
    }

    public interface IResolutionObserver
    {
        void NotifyResolved();
    }
}