using model.timing.corp;
using model.zones.corp;
using System.Threading.Tasks;

namespace model.ai
{
    public class CorpAi : IActionStepObserver, IHqDiscardObserver
    {
        private Game game;

        public CorpAi(Game game)
        {
            this.game = game;
        }

        public void Play()
        {
            game.corp.turn.ObserveActionStep(this);
            game.corp.zones.hq.ObserveDiscarding(this);
        }

        async Task IActionStepObserver.NotifyActionStep()
        {
            await Task.Delay(1000);
            game.corp.actionCard.credit.Trigger(game);
        }

        void IHqDiscardObserver.NotifyDiscarding(bool discarding)
        {
            if (discarding)
            {
                game.corp.zones.hq.Discard(game.corp.zones.hq.Random(), game.corp.zones.archives);
            }
        }
    }
}