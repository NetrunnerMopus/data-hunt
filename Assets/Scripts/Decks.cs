using System.Collections.Generic;

public class Decks
{
    private CardPool pool = new CardPool();

    public Deck DemoRunner()
    {
        var cards = new List<Card>
        {
            pool.abagnale,
            pool.abagnale,
            pool.abagnale,
            pool.paperclip,
            pool.paperclip,
            pool.citadelSanctuary,
            pool.mongoose,
            pool.mongoose,
            pool.mongoose,
            pool.securityTesting,
            pool.securityTesting,
            pool.securityTesting,
            pool.hqInterface,
            pool.hqInterface,
            pool.sneakdoorBeta,
            pool.spyCamera,
            pool.spyCamera,
            pool.spyCamera,
            pool.spyCamera,
            pool.spyCamera,
            pool.spyCamera
        };
        return new Deck(cards);
    }
}
