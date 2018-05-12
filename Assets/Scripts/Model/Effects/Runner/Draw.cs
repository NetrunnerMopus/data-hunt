using System.Collections.Generic;

namespace model.effects.runner
{
    public class Draw : IEffect, IStackCountObserver
    {
        private int cards;
        private HashSet<IImpactObserver> observers = new HashSet<IImpactObserver>();

        public Draw(int cards)
        {
            this.cards = cards;
        }

        void IEffect.Resolve(Game game)
        {
            for (int i = 0; i < cards; i++)
            {
                if (game.runner.stack.HasCards())
                {
                    var drawn = game.runner.stack.RemoveTop();
                    game.runner.grip.Add(drawn);
                }
            }
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observers.Add(observer);
            game.runner.stack.ObserveCount(this);
        }

        void IStackCountObserver.NotifyCardCount(int cards)
        {
            var impactful = cards > 0;
            foreach (var observer in observers)
            {
                observer.NotifyImpact(impactful, this);
            }
        }
    }
}