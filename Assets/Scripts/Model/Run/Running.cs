using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.timing;
using model.zones.corp;

namespace model.run
{
    public class Running
    {
        private Game game;

        public Running(Game game)
        {
            this.game = game;
        }

        public IEffect RunningOn(IServer server)
        {
            return new Run(server, game);
        }
    }
}
