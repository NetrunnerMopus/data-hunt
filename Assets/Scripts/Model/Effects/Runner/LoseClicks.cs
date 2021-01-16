using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.effects.runner
{
    public class LoseClicks : IEffect, IClickObserver
    {
        private int clicksToLose;
        private HashSet<IImpactObserver> observers = new HashSet<IImpactObserver>();
        IEnumerable<string> IEffect.Graphics => new string[] { "" };

        public LoseClicks(int clicks)
        {
            this.clicksToLose = clicks;
        }

        async Task IEffect.Resolve(Game game)
        {
            game.runner.clicks.Lose(clicksToLose);
            await Task.CompletedTask;
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observers.Add(observer);
            game.runner.clicks.Observe(this);
        }

        void IClickObserver.NotifyClicks(int spent, int remaining)
        {
            var impactful = remaining > clicksToLose;
            foreach (var observer in observers)
            {
                observer.NotifyImpact(impactful, this);
            }
        }
    }
}
