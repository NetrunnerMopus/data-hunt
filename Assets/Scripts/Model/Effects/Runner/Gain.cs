using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.effects.runner
{
    public class Gain : IEffect
    {
        private int credits;
        IEnumerable<string> IEffect.Graphics => new string[] { "Images/UI/credit" };

        public Gain(int credits)
        {
            this.credits = credits;
        }

        async Task IEffect.Resolve(Game game)
        {
            game.runner.credits.Gain(credits);
            await Task.CompletedTask;
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}
