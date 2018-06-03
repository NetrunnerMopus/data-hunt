using model.cards.corp;
using model.play.corp;
using model.timing.corp;
using model.zones.corp;
using System.Threading.Tasks;

namespace model.ai
{
    public class CorpAi : ICorpActionObserver, IHqDiscardObserver
    {
        private Game game;
        private Zones zones;
        private ActionCard actionCard;

        public CorpAi(Game game)
        {
            this.game = game;
            zones = game.corp.zones;
            actionCard = game.corp.actionCard;
        }

        public void Play()
        {
            game.flow.corpTurn.ObserveActions(this);
            zones.hq.ObserveDiscarding(this);
        }

        async void ICorpActionObserver.NotifyActionTaking()
        {
            await Task.Delay(600);
            var pad = zones.hq.Find<PadCampaign>();
            if (pad != null)
            {
                var remote = zones.CreateRemote();
                actionCard.InstallInServer(pad, remote).Trigger(game);
            }
            else
            {
                actionCard.credit.Trigger(game);
            }
        }

        void IHqDiscardObserver.NotifyDiscarding(bool discarding)
        {
            if (discarding)
            {
                zones.hq.Discard(zones.hq.Random(), zones.archives);
            }
        }
    }
}