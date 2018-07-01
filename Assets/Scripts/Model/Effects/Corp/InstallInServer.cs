using model.cards;
using model.play.corp;
using model.zones.corp;

namespace model.effects.corp
{
    public class InstallInServer : IEffect
    {
        private readonly Card card;
        private Remote remote;

        public InstallInServer(Card card, Remote remote)
        {
            this.card = card;
            this.remote = remote;
        }

        void IEffect.Resolve(Game game)
        {
            remote.InstallWithin(card);
            if (card.Type.Rezzable)
            {
                var rezzable = new Rezzable(card, game);
                game.flow.corpTurn.rezWindow.Add(rezzable);
            }
            game.corp.zones.hq.Remove(card);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}