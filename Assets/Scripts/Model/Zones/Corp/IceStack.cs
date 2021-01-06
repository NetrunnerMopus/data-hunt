using System.Threading.Tasks;
using model.cards;

namespace model.zones.corp
{
    public class IceStack : IInstallDestination
    {
        public Zone Zone { get; } = new Zone("ICE");
        public int Height => Zone.Count;
        private Game game;


        public IceStack(Game game)
        {
            this.game = game;
        }

        void IInstallDestination.Host(Card card)
        {
            Zone.Add(card);
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
