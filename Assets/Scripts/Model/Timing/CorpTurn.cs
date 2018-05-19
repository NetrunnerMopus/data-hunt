using System.Threading.Tasks;

namespace model.timing
{
    public class CorpTurn
    {
        private Game game;

        public CorpTurn(Game game)
        {
            this.game = game;
        }

        async public Task Start()
        {
            game.corp.clicks.Gain(3);
            await TakeActions();
        }

        async private Task TakeActions()
        {
            UnityEngine.Debug.Log("Corp taking actions");
            while (game.corp.clicks.Remaining() > 0)
            {
                await game.corp.actionCard.TakeAction();
                UnityEngine.Debug.Log("Corp action taken");
            }
        }
    }
}