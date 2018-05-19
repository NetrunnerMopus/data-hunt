using System.Threading.Tasks;

namespace model.timing
{
    public class GameFlow
    {
        private Game game;

        public GameFlow(Game game)
        {
            this.game = game;
        }

        async public Task Start()
        {
            while (!game.ended)
            {
                await new CorpTurn(game).Start();
                await new RunnerTurn(game).Start();
            }
        }
    }
}