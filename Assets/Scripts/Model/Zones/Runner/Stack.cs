using model.cards;
using System.Collections.Generic;

namespace model.zones.runner
{
    public class Stack
    {
        private Deck deck;
        private HashSet<IStackCountObserver> counts = new HashSet<IStackCountObserver>();
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

        public ICard RemoveTop()
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
                observer.NotifyCardCount(cards);
            }
            return card;
        }

        public void ObserveCount(IStackCountObserver observer)
        {
            counts.Add(observer);
            observer.NotifyCardCount(deck.Size());
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