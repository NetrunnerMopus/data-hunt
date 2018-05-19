using System.Threading.Tasks;
using model.timing.corp;
using model.timing.runner;

namespace model.timing
{
    public class GameFlow
    {
        private Game game;
        private corp.Turn corpTurn;
        private runner.Turn runnerTurn;

        public GameFlow(Game game, corp.Turn corpTurn, runner.Turn runnerTurn)
        {
            this.game = game;
            this.corpTurn = corpTurn;
            this.runnerTurn = runnerTurn;
        }

        async public Task Start()
        {
            while (!game.ended)
            {
                await corpTurn.Start();
                await runnerTurn.Start();
            }
        }
    }
}