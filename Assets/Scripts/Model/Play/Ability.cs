using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.play
{
    public class Ability : IPayabilityObserver, IImpactObserver
    {
        public readonly ICost cost;
        public readonly IEffect effect;
        public event Action<Ability, bool> UsabilityChanged = delegate {};
        public event Action<Ability> Resolved = delegate {};
        private bool payable = false;
        private bool impactful = false;
        public bool Usable => payable && impactful;

        public Ability(ICost cost, IEffect effect)
        {
            this.cost = cost;
            this.effect = effect;
        }

        async public Task Trigger(Game game)
        {
            await cost.Pay(game);
            await effect.Resolve(game);
            Resolved(this);
        }

        public void ObserveUsability(IUsabilityObserver observer, Game game)
        {
            usabilities.Add(observer);
            cost.Observe(this, game);
            effect.Observe(this, game);
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
            UsabilityChanged(this, Usable);
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
}
