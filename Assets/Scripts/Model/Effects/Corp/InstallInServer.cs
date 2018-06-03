using model.cards;
using model.zones.corp;

namespace model.effects.corp
{
    public class InstallInServer : IEffect
    {
        private readonly ICard card;
        private Remote remote;

        internal InstallInServer(ICard card, Remote remote)
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