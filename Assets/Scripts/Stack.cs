using UnityEngine;
using UnityEngine.UI;
using controller;
using model;

public class Stack
{
    private GameObject zone;
    private Deck deck;
    private CardPrinter printer;
    private Grip grip;
    private GameObject top;

    public Stack(GameObject zone, Deck deck, Grip grip, CardPrinter printer)
    {
        this.zone = zone;
        this.deck = deck;
        this.grip = grip;
        this.printer = printer;
    }

    public void Draw()
    {
        if (deck.HasCards())
        {
            Object.Destroy(top);
            var card = deck.Draw();
            grip.AddCard(card);
            ActivateTop();
            var text = zone.GetComponentInChildren<Text>();
            text.text = deck.Size() + " cards";
        }
    }

    private void ActivateTop()
    {
        if (deck.HasCards())
        {
            top = printer.PrintRunnerFacedown("Top of stack", zone.transform);
            top.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
            var rect = top.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
            var topOfTheStack = top.AddComponent<TopOfTheStack>();
            topOfTheStack.gripZone = grip.Zone;
        }
    }

    public void Shuffle()
    {
        deck.Shuffle();
        ActivateTop();
    }
}
