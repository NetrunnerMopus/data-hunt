public class Grip
{
    public GripZone Zone { get; private set; }
    private CardPrinter printer;

    public Grip(GripZone zone, CardPrinter printer)
    {
        Zone = zone;
        this.printer = printer;
    }

    public void AddCard(ICard card)
    {
        var visual = printer.Print(card.GetName(), "Images/Cards/" + card.GetImageAsset(), Zone.transform);
        var cardInGrip = visual.AddComponent<CardInGrip>();
        cardInGrip.Card = card;
    }
}
