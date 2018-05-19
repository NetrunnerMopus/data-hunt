using model.timing.corp;
using System.Threading.Tasks;

namespace model.ai
{
    public class CorpAi : IActionStepObserver
    {
        private Game game;

        public CorpAi(Game game)
        {
            this.game = game;
        }

        public void Play()
        {
            game.corp.turn.ObserveActionStep(this);
        }

        async Task IActionStepObserver.NotifyActionStep()
        {
            await Task.Delay(1000);
            game.corp.actionCard.credit.Trigger(game);
        }
    }
}