using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards;
using model.player;
using model.zones;

namespace model.effects
{
    public class GenericInstall : IEffect, IPayabilityObserver
    {
        private readonly Card card;
        private readonly IPilot pilot;
        private readonly HashSet<IImpactObserver> impactObservers = new HashSet<IImpactObserver>();

        public GenericInstall(Card card, IPilot pilot)
        {
            this.card = card;
            this.pilot = pilot;
        }

        // CR: 8.3
        async public Task Resolve(Game game)
        {
            PutOut(game);
            var destination = await ChooseDestination(game);
            await TrashLikeCards(destination);
            await PayInstallCost(destination);
            await Place(destination, game);
            await TriggerPostInstall();
        }

        // CR: 8.3.1
        private void PutOut(Game game)
        {
            card.MoveTo(game.PlayArea);
            card.FlipPreInstall();
        }

        // CR: 8.3.2
        async private Task<IInstallDestination> ChooseDestination(Game game)
        {
            var destinations = card.FindInstallDestinations(game);
            return await pilot.ChooseAnInstallDestination().Declare("Choose installation destination", destinations, game);
        }

        // CR: 8.3.3
        // CR: 8.2.5
        async private Task TrashLikeCards(IInstallDestination destination)
        {
            await destination.TrashAlike(card);
        }

        // CR: 8.3.4
        // CR: 8.2.11
        async private Task PayInstallCost(IInstallDestination destination)
        {
            await destination.PayInstallCost(card);
        }

        // CR: 8.3.5
        async private Task Place(IInstallDestination destination, Game game)
        {
            destination.Host(card);
            if (card.Faction.Side == Side.RUNNER)
            {
                // CR: 8.2.3
                card.FlipFaceUp();
                await card.Activate(game);
            }
        }

        // CR: 8.3.6
        async private Task TriggerPostInstall()
        {
            await Task.FromResult("TODO: Implement TriggerPostInstall");
        }

        public void Observe(IImpactObserver observer, Game game)
        {
            if (card.Faction.Side == Side.RUNNER)
            {
                impactObservers.Add(observer);
                card.PlayCost.Observe(this, game);
            }

            if (card.Faction.Side == Side.CORP)
            {
                observer.NotifyImpact(true, this);
            }
        }

        void IPayabilityObserver.NotifyPayable(bool payable, ICost source)
        {
            foreach (var observer in impactObservers)
            {
                observer.NotifyImpact(payable, this);
            }
        }
    }
}