using model.cards;
using model.player;
using model.timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.zones.corp
{
    public class Headquarters : IServer
    {
        public Zone Zone { get; } = new Zone("HQ");
        public IceColumn Ice { get; }
        private IList<IHqDiscardObserver> discards = new List<IHqDiscardObserver>();
        private TaskCompletionSource<bool> discarding;
        private Random random;

        public Headquarters(Random random, CreditPool credits)
        {
            this.random = random;
            Ice = new IceColumn(this, credits);
        }

        async public Task Discard()
        {
            discarding = new TaskCompletionSource<bool>();
            foreach (var observer in discards)
            {
                observer.NotifyDiscarding(true);
            }
            await discarding.Task;
        }

        public void Discard(Card card, Archives archives)
        {
            card.MoveTo(archives.Zone);
            foreach (var observer in discards)
            {
                observer.NotifyDiscarding(false);
            }
            discarding.SetResult(true);
        }

        public Card Random() => Zone.Cards[random.Next(0, Zone.Count)];

        public void ObserveDiscarding(IHqDiscardObserver observer)
        {
            discards.Add(observer);
        }

        async Task IServer.Access(int accessCount, IPilot pilot, Game game)
        {
            if (Zone.Cards.Count == 0)
            {
                return;
            }
            var unaccessed = new List<Card>(Zone.Cards);
            for (var accessesLeft = accessCount; accessesLeft > 0; accessesLeft--)
            {
                var randomCard = unaccessed
                    .OrderBy(it => random.Next())
                    .First();
                var cardToAccess = await pilot.ChooseACard().Declare("Which card to access now?", new List<Card> { randomCard }); 
                unaccessed.Remove(cardToAccess); // TODO : draw already accessed cards on the side
                await new AccessCard(cardToAccess, game).AwaitEnd();
            }
        }
    }
    public interface IHqDiscardObserver
    {
        void NotifyDiscarding(bool discarding);
    }
}
