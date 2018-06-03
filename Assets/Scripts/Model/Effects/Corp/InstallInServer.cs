using model.cards;
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
            game.corp.zones.hq.Remove(card);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}