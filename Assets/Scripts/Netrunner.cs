using UnityEngine;

public class Netrunner : MonoBehaviour
{
    public static Game game;

    public GripZone gripZone;
    public GameObject stackZone;
    public GameObject heapZone;
    public GameObject serversZone;
    public GameObject creditsZone;

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
        var grip = new Grip(gripZone, printer);
        var stack = new Stack(stackZone, runnerDeck, grip, printer);
        var creditPool = new CreditPool(creditsZone);
        var runner = new Runner(grip, stack, creditPool);
        return runner;
    }
}
