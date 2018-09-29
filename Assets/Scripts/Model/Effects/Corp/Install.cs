using model.cards;
using model.play.corp;
using model.zones;

namespace model.effects.corp
{
    public class Install : IEffect
    {
        private readonly Card card;
        private IInstallDestination destination;

        public Install(Card card, IInstallDestination destination)
        {
            this.card = card;
            this.destination = destination;
        }

        public void Resolve(Game game)
        {
            destination.Host(card);
            if (card.Type.Rezzable)
            {
                var rezzable = new Rezzable(card, game);
                game.flow.corpTurn.rezWindow.Add(rezzable);
            }
            game.corp.zones.hq.Remove(card);
        }

        public void Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}