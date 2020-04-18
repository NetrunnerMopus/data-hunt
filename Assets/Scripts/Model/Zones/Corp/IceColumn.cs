using System.Threading.Tasks;
using model.cards;

namespace model.zones.corp
{
    public class IceColumn : IInstallDestination
    {
        public int Height { get; private set; } = 0;
        private Game game;

        public IceColumn(Game game)
        {
            this.game = game;
        }

        void IInstallDestination.Host(Card card)
        {
            throw new System.NotImplementedException();
        }

        Task IInstallDestination.TrashAlike(Card card)
        {
            // CR: 8.2.5.c
            throw new System.NotImplementedException();
        }

        Task IInstallDestination.PayInstallCost(Card card)
        {
            // CR: 8.2.11.a    
            game.corp.credits.Pay(Height);
            return Task.CompletedTask;
        }
    }
}
