using model.cards;
using view;

namespace model
{
    public class Stack
    {
        private Deck deck;
        private StackPile stackPile;

        public Stack(Deck deck, StackPile stackPile)
        {
            this.deck = deck;
            this.stackPile = stackPile;
        }

        public void Shuffle()
        {
            deck.Shuffle();
        }

        public ICard RemoveTop()
        {
            var card = deck.RemoveTop();
            stackPile.UpdateCardsLeft(deck.Size());
            return card;
        }
    }
}