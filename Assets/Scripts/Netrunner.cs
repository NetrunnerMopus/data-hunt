using UnityEngine;
using model;
using view;
using controller;

public class Netrunner : MonoBehaviour
{
    public static Game game;

    private bool started = false;
    public GripZone gripZone;
    public GripFan gripFan;
    public StackPile stackPile;
    public HeapPile heapPile;
    public PlayZone playZone;
    public CardPrinter serversZone;
    public ClickPoolView clickPoolView;
    public CreditPoolView creditPoolView;

    private Deck runnerDeck = new Decks().DemoRunner();

    void Update()
    {
        if (!started)
        {
            started = true;
            var corp = SetupCorporation();
            var runner = SetupRunner();
            game = new Game(corp, runner);
            game.Start();
        }
    }

    private Corp SetupCorporation()
    {
        var printer = serversZone.GetComponent<CardPrinter>();
        printer.PrintCorpFacedown("Archives");
        printer.PrintCorpFacedown("R&D");
        printer.PrintCorpFacedown("HQ");
        printer.PrintCorpFacedown("Remote");
        return new Corp();
    }

    private Runner SetupRunner()
    {
        var stack = new Stack(runnerDeck, stackPile, gripFan);
        var heap = new Heap(heapPile);
        var clicks = new ClickPool(clickPoolView);
        var credits = new CreditPool(creditPoolView);
        var runner = new Runner(stack, heap, clicks, credits);
        return runner;
    }
}
