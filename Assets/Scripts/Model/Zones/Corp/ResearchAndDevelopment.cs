using System.Collections.Generic;
using model.cards;

namespace model.zones.corp
{
    public class ResearchAndDevelopment : IServer
    {
        string IServer.Name => "R&D";
        IceColumn IServer.Ice => new IceColumn();
        private Deck deck;
		private HashSet<IZoneCountObserver> counts = new HashSet<IZoneCountObserver>();

		public ResearchAndDevelopment(Deck deck)
        {
            this.deck = deck;
        }

        public void Shuffle()
        {
            deck.Shuffle();
        }

        public bool HasCards() => deck.Size() > 0;

        public void Draw(int cards, Headquarters hq)
        {
            for (int i = 0; i < cards; i++)
            {
                if (HasCards())
                {
                    hq.Add(RemoveTop());
                }
            }
        }

        public Card RemoveTop()
        {
            var top = deck.RemoveTop();
			NotifyCount();
			return top;
        }

		private void NotifyCount() {
			foreach (var observer in counts) {
				observer.NotifyCount(deck.Size());
			}
		}

		public void ObserveCount(IZoneCountObserver observer) {
			counts.Add(observer);
		}
	}
}