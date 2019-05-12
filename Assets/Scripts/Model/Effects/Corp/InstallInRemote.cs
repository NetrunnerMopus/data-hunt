using model.cards;
using model.choices;
using model.play.corp;

namespace model.effects.corp
{
    public class InstallInRemote : IEffect
    {
        private readonly Card card;
        private IRemoteInstallationChoice remoteChoice;

        public InstallInRemote(Card card, IRemoteInstallationChoice remoteChoice)
        {
            this.card = card;
            this.remoteChoice = remoteChoice;
        }

        void IEffect.Resolve(Game game)
        {
            var remote = remoteChoice.Choose();
            remote.InstallWithin(card);
            if (card.Type.Rezzable)
            {
                var rezzable = new Rezzable(card, game);
                game.corp.turn.rezWindow.Add(rezzable);
            }
            game.corp.zones.hq.Remove(card);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}