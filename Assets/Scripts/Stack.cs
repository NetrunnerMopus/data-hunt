using UnityEngine;

public class Stack
{
    private GameObject zone;
    private Deck deck;
    private CardPrinter printer;
    private Grip grip;
    private GameObject topFacedown;

    public Stack(GameObject zone, Deck deck, Grip grip, CardPrinter printer)
    {
        this.zone = zone;
        this.deck = deck;
        this.grip = grip;
        this.printer = printer;
    }

    public void Draw()
    {
        Object.Destroy(topFacedown);
        var card = deck.Draw();
        grip.AddCard(card);
        ActivateTop();
    }

    private void ActivateTop()
    {
        if (deck.HasCards())
        {
            topFacedown = printer.PrintRunnerFacedown("Top of stack", zone.transform);
            topFacedown.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
            var top = topFacedown.AddComponent<TopOfTheStack>();
            top.stack = this;
        }
    }

    public void Shuffle()
    {
        deck.Shuffle();
    }
}
