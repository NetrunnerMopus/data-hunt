using UnityEngine;

public class Netrunner : MonoBehaviour
{
    public GameObject gripZone;
    public GameObject stackZone;
    public GameObject heapZone;
    public GameObject serversZone;
    public GameObject creditsZone;

    private Deck runnerDeck = new Decks().DemoRunner();
    private CardPrinter printer = new CardPrinter();

    void Start()
    {
        SetupCorporation();
        SetupRunner();
    }

    private void SetupCorporation()
    {
        var serverZoneTransform = serversZone.transform;
        printer.PrintCorpFacedown("Archives", serverZoneTransform);
        printer.PrintCorpFacedown("R&D", serverZoneTransform);
        printer.PrintCorpFacedown("HQ", serverZoneTransform);
        printer.PrintCorpFacedown("Remote", serverZoneTransform);
    }

    private void SetupRunner()
    {
        var grip = new Grip(gripZone, printer);
        var stack = new Stack(stackZone, runnerDeck, grip, printer);
        var creditPool = new CreditPool(creditsZone);
        var runner = new Runner(grip, stack, creditPool);
        runner.StartGame();
    }
}
