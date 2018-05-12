using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.play
{
    public class Ability : IPayabilityObserver
    {
        public readonly ICost cost;
        public readonly IEffect effect;
        private HashSet<IUsabilityObserver> usabilities = new HashSet<IUsabilityObserver>();
        private HashSet<IResolutionObserver> resolutions = new HashSet<IResolutionObserver>();

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
            foreach (var observer in usabilities)
            {
                observer.NotifyUsable(payable);
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