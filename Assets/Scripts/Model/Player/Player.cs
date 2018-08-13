namespace model.player
{
    public class Player
    {
        public readonly Deck deck;
        public readonly IPilot pilot;

        public Player(Deck deck, IPilot pilot)
        {
            this.deck = deck;
            this.pilot = pilot;
        }
    }
}