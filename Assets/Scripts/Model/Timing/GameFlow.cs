using System;
using System.Threading.Tasks;

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
            try
            {
                while (!game.ended)
                {
                    await corpTurn.Start();
                    await runnerTurn.Start();
                }
            }
            catch (Exception e)
            {
                if (game.ended)
                {
                    UnityEngine.Debug.Log("The game is over! " + e.Message);
                }
            }
        }
    }
}