using System.Collections.Generic;
using model.zones;

namespace model.effects.runner
{
    public class Draw : IEffect, IZoneCountObserver
    {
        private int cards;
        private HashSet<IImpactObserver> observers = new HashSet<IImpactObserver>();

        public Draw(int cards)
        {
            this.cards = cards;
        }

        void IEffect.Resolve(Game game)
        {
            var stack = game.runner.zones.stack;
            var grip = game.runner.zones.grip;
            stack.Draw(cards, grip);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observers.Add(observer);
            game.runner.zones.stack.Zone.ObserveCount(this);
        }

        void IZoneCountObserver.NotifyCount(int count)
        {
            var impactful = count > 0;
            foreach (var observer in observers)
            {
                observer.NotifyImpact(impactful, this);
            }
        }
    }
}