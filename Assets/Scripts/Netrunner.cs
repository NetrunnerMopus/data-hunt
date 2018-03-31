using UnityEngine;
using model;
using view;
using controller;

public class Netrunner : MonoBehaviour
{
    public static Game game;

    public GripZone gripZone;
    public GripFan gripFan;
    public StackPile stackPile;
    public GameObject heapZone;
    public PlayZone playZone;
    public CardPrinter serversZone;
    public CreditPoolView creditPoolView;

    private Deck runnerDeck = new Decks().DemoRunner();

    void Start()
    {
        var corp = SetupCorporation();
        var runner = SetupRunner();
        game = new Game(corp, runner);
        game.Start();
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
        var heap = new Heap(heapZone);
        var creditPool = new CreditPool(creditPoolView);
        var runner = new Runner(stack, heap, creditPool);
        return runner;
    }
}
