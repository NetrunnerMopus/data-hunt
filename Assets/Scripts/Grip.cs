public class Grip
{
    public GripZone Zone { get; private set; }
    private PlayZone playZone;
    private CardPrinter printer;

    public Grip(GripZone zone, PlayZone playZone, CardPrinter printer)
    {
        Zone = zone;
        this.printer = printer;
        this.playZone = playZone;
    }

    public void AddCard(ICard card)
    {
        var visual = printer.Print(card.Name, "Images/Cards/" + card.FaceupArt, Zone.transform);
        var cardInGrip = visual.AddComponent<CardInGrip>();
        cardInGrip.Card = card;
        cardInGrip.playZone = playZone;
    }
}
