using System.Collections.Generic;

public class Decks
{
    public Deck DemoRunner()
    {
        var cards = new List<ICard>();
        for (int i = 0; i < 7; i++)
        {
            cards.Add(new Diesel());
            cards.Add(new SureGamble());
        }
        return new Deck(cards);
    }
}
