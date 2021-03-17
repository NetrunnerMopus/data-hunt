namespace model.zones.runner
{
    public class Stack
    {
        public readonly Zone zone = new Zone("Stack");
        private Shuffling shuffling;

        public Stack(Shuffling shuffling)
        {
            this.shuffling = shuffling;
        }

        public void AddDeck(Deck deck)
        {
            foreach (var card in deck.cards)
            {
                card.MoveTo(zone);
            }
            Shuffle();
        }

        public void Shuffle()
        {
            shuffling.Shuffle(zone.Cards);
        }

        public bool HasCards() => zone.Cards.Count > 0;

        public void Draw(int cards, Grip grip)
        {
            for (int i = 0; i < cards; i++)
            {
                if (HasCards())
                {
                    zone.Cards[0].MoveTo(grip.zone);
                }
            }
        }
    }
}
