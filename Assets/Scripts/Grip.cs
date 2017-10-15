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

    public void AddCard(Card card)
    {
        var cardInGrip = printer.Print(card.name, "Images/Cards/" + card.id, zone.transform);
        cardInGrip.AddComponent<CardInGrip>();
    }
}
