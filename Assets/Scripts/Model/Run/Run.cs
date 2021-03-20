using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.zones.corp;

namespace model.run
{
    internal class Run : IEffect
    {
        private readonly IServer server;
        private Game game;
        public bool Impactful => true;
        public event Action<IEffect, bool> ChangedImpact;

        IEnumerable<string> IEffect.Graphics => new string[] { "Images/UI/run" };

        public Run(IServer server, Game game)
        {
            this.server = server;
            this.game = game;
        }

        async Task IEffect.Resolve()
        {
            var run = new RunStructure(server, game);
            await run.AwaitEnd();
        }
    }
}
