using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards;
// "Ability(cost=Conjunction(costs=model.costs.CorpClickCost, model.costs.ActionPermission, model.costs.InZone), effect=model.effects.GenericInstall)"
namespace model.zones.corp
{
    public class IceStack : IInstallDestination
    {
        public int Height { get; private set; } = 0;
        private Game game;
        private IList<Card> ice = new List<Card>();

        public IceStack(Game game)
        {
            this.game = game;
        }

        void IInstallDestination.Host(Card card)
        {
            ice.Add(card);
        }

        Task IInstallDestination.TrashAlike(Card card)
        {
            // CR: 8.2.5.c
            return Task.CompletedTask;
        }

        Task IInstallDestination.PayInstallCost(Card card)
        {
            // CR: 8.2.11.a    
            game.corp.credits.Pay(Height);
            return Task.CompletedTask;
        }
    }
}
