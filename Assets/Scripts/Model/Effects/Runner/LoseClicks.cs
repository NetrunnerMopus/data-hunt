using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.effects.runner
{
    public class LoseClicks : IEffect
    {
        public bool Impactful { get; private set; }
        public event System.Action<IEffect, bool> ChangedImpact = delegate { };
        private ClickPool pool;
        private int clicksToLose;
        IEnumerable<string> IEffect.Graphics => new string[] { "" };

        public LoseClicks(ClickPool pool, int clicks)
        {
            this.pool = pool;
            this.clicksToLose = clicks;
            pool.Changed += CountClicks;
        }

        private void CountClicks(ClickPool pool)
        {
            Impactful = pool.Remaining > clicksToLose;
            ChangedImpact(this, Impactful);
        }

        async Task IEffect.Resolve(Game game)
        {
            game.runner.clicks.Lose(clicksToLose);
            await Task.CompletedTask;
        }
    }
}
