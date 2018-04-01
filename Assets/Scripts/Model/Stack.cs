using view;

namespace model
{
    public class Stack
    {
        private Deck deck;
        private StackPile stackPile;
        private GripFan gripFan;

        public Stack(Deck deck, StackPile stackPile, GripFan gripFan)
        {
            this.deck = deck;
            this.stackPile = stackPile;
            this.gripFan = gripFan;
        }

        public void Shuffle()
        {
            deck.Shuffle();
        }

        public void Draw()
        {
            var card = deck.Draw();
            stackPile.UpdateCardsLeft(deck.Size());
            gripFan.AddCard(card);
        }
    }
}