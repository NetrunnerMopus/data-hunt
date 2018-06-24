using model.cards.corp;
using model.play.corp;
using model.timing.corp;
using model.zones.corp;
using System.Threading.Tasks;

namespace model.ai
{
    public class CorpAi : ICorpActionObserver, IHqDiscardObserver, IRezWindowObserver
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
            game.flow.corpTurn.rezWindow.ObserveWindow(this);
            zones.hq.ObserveDiscarding(this);
        }

        async void ICorpActionObserver.NotifyActionTaking()
        {
            await Task.Delay(600);
            var pad = zones.hq.Find<PadCampaign>();
            var hedge = zones.hq.Find<HedgeFund>();
            if (pad != null)
            {
                var remote = zones.CreateRemote();
                actionCard.InstallInServer(pad, remote).Trigger(game);
            }
            else if (hedge != null)
            {
                actionCard.Play(hedge).Trigger(game);
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

        void IRezWindowObserver.NotifyRezWindowClosed()
        {
        }

        void IRezWindowObserver.NotifyRezWindowOpened()
        {
            game.flow.corpTurn.rezWindow.Pass();
        }
    }
}