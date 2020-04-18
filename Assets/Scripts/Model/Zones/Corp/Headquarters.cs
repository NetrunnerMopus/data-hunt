using model.cards;
using model.player;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.zones.corp
{
    public class Headquarters : IServer
    {
        public Zone Zone { get; } = new Zone("HQ");
        public IceColumn Ice { get; }
        private HashSet<IHqDiscardObserver> discards = new HashSet<IHqDiscardObserver>();
        private TaskCompletionSource<bool> discarding;
        private Random random;
        private Game game;

        public Headquarters(Game game, Random random)
        {
            this.random = random;
            this.game = game;
            Ice = new IceColumn(game);
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

        Task IServer.Access(int accessCount, IPilot pilot, Game game)
        {
            throw new NotImplementedException();
        }
    }
    public interface IHqDiscardObserver
    {
        void NotifyDiscarding(bool discarding);
    }
}
