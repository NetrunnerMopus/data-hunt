using model.timing.corp;
using model.zones.corp;
using System.Threading.Tasks;

namespace model.ai
{
    public class CorpAi : ICorpActionObserver, IHqDiscardObserver
    {
        private Game game;

        public CorpAi(Game game)
        {
            this.game = game;
        }

        public void Play()
        {
            game.flow.corpTurn.ObserveActions(this);
            game.corp.zones.hq.ObserveDiscarding(this);
        }

        async void ICorpActionObserver.NotifyActionTaking()
        {
            await Task.Delay(600);
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