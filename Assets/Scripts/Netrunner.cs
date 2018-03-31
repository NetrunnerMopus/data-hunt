using UnityEngine;
using model;
using view;

public class Netrunner : MonoBehaviour
{
    public static Game game;

    public GripZone gripZone;
    public GameObject stackZone;
    public GameObject heapZone;
    public PlayZone playZone;
    public GameObject serversZone;
    public CreditPoolView creditPoolView;

    private Deck runnerDeck = new Decks().DemoRunner();
    private CardPrinter printer = new CardPrinter();

    void Start()
    {
        var corp = SetupCorporation();
        var runner = SetupRunner();
        game = new Game(corp, runner);
        game.Start();
    }

    private Corp SetupCorporation()
    {
        var serverZoneTransform = serversZone.transform;
        printer.PrintCorpFacedown("Archives", serverZoneTransform);
        printer.PrintCorpFacedown("R&D", serverZoneTransform);
        printer.PrintCorpFacedown("HQ", serverZoneTransform);
        printer.PrintCorpFacedown("Remote", serverZoneTransform);
        return new Corp();
    }

    private Runner SetupRunner()
    {
        var grip = new Grip(gripZone, playZone, printer);
        var stack = new Stack(stackZone, runnerDeck, grip, printer);
        var heap = new Heap(heapZone);
        var creditPool = new CreditPool(creditPoolView);
        var runner = new Runner(grip, stack, heap, creditPool);
        return runner;
    }
}
