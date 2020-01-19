using System.Threading.Tasks;
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

        async public Task Resolve(Game game)
        {
            destination.Host(card);
            if (card.Type.Rezzable)
            {
                var rezzable = new Rezzable(card, game);
                game.corp.turn.rezWindow.Add(rezzable);
            }
            await Task.CompletedTask;
        }

        public void Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}