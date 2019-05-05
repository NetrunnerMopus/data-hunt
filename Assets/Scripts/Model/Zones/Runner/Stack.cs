using model.cards;
using model.zones.corp;
using System.Collections.Generic;

namespace model.zones.runner
{
    public class Stack
    {
        private Deck deck;
        private HashSet<IZoneCountObserver> counts = new HashSet<IZoneCountObserver>();
        private HashSet<IStackPopObserver> pops = new HashSet<IStackPopObserver>();

        public Stack(Deck deck)
        {
            this.deck = deck;
        }

        public void Shuffle()
        {
            deck.Shuffle();
        }

        public bool HasCards() => deck.Size() > 0;

        public void Draw(int cards, Grip grip)
        {
            for (int i = 0; i < cards; i++)
            {
                if (HasCards())
                {
                    grip.Add(RemoveTop());
                }
            }
        }

        public Card RemoveTop()
        {
            var card = deck.RemoveTop();
            var cards = deck.Size();
            var empty = cards == 0;
            foreach (var observer in pops)
            {
                observer.NotifyCardPopped(empty);
            }
            foreach (var observer in counts)
            {
                observer.NotifyCount(cards);
            }
            return card;
        }

        public void ObserveCount(IZoneCountObserver observer)
        {
            counts.Add(observer);
            observer.NotifyCount(deck.Size());
        }

        public void ObservePopping(IStackPopObserver observer)
        {
            pops.Add(observer);
        }
    }

    public interface IStackCountObserver
    {
        void NotifyCardCount(int cards);
    }

    public interface IStackPopObserver
    {
        void NotifyCardPopped(bool empty);
    }
}