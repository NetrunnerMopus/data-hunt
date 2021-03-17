using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.effects.corp
{
    public class Gain : IEffect
    {
        private int credits;
        public bool Impactful => true;
        public event Action<IEffect, bool> ChangedImpact;

        IEnumerable<string> IEffect.Graphics => new string[] { "Images/UI/credit" };
        public Gain(int credits)
        {
            this.credits = credits;
        }

        async Task IEffect.Resolve(Game game)
        {
            game.corp.credits.Gain(credits);
            await Task.CompletedTask;
        }
    }
}
