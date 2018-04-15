using model.cards;
using view;

namespace model
{
    public class Stack
    {
        private Deck deck;
        private IStackView view;

        public Stack(Deck deck, IStackView view)
        {
            this.deck = deck;
            this.view = view;
        }

        public void Shuffle()
        {
            deck.Shuffle();
        }

        public ICard RemoveTop()
        {
            var card = deck.RemoveTop();
            view.UpdateCardsLeft(deck.Size());
            return card;
        }
    }
}