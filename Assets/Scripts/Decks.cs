using System.Collections.Generic;

public class Decks
{
    public Deck DemoRunner()
    {
        var cards = new List<ICard>();
        for (int i = 0; i < 5; i++)
        {
            cards.Add(new Diesel());
            cards.Add(new SureGamble());
            cards.Add(new BuildScript());
            cards.Add(new ProcessAutomation());
        }
        return new Deck(cards);
    }
}
