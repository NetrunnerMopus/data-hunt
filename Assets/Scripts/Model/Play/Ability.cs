using System.Collections.Generic;

namespace model.play
{
    public class Ability : IAvailabilityObserver<ICost>
    {
        public readonly ICost cost;
        public readonly IEffect effect;
        private HashSet<IAvailabilityObserver<Ability>> observers = new HashSet<IAvailabilityObserver<Ability>>();

        public Ability(ICost cost, IEffect effect)
        {
            this.cost = cost;
            this.effect = effect;
        }

        public void Trigger(Game game)
        {
            if (cost.CanPay(game))
            {
                cost.Pay(game);
                effect.Resolve(game);
            }
        }

        public void Observe(IAvailabilityObserver<Ability> observer, Game game)
        {
            observers.Add(observer);
            cost.Observe(this, game);
        }

        public void Notify(bool available, ICost resource)
        {
            foreach (IAvailabilityObserver<Ability> observer in observers)
            {
                observer.Notify(available, this);
            }
        }
    }
}