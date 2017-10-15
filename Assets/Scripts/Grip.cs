using UnityEngine;

public class Grip
{
    private GameObject zone;
    private CardPrinter printer;

    public Grip(GameObject zone, CardPrinter printer)
    {
        this.zone = zone;
        this.printer = printer;
    }

    public void AddCard(ICard card)
    {
        var visual = printer.Print(card.GetName(), "Images/Cards/" + card.GetImageAsset(), zone.transform);
        var cardInGrip = visual.AddComponent<CardInGrip>();
        cardInGrip.Card = card;
    }
}
