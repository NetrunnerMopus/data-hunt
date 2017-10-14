using UnityEngine;

public class Netrunner : MonoBehaviour
{
    public GameObject gripZone;
    public GameObject stackZone;
    public GameObject heapZone;
    public GameObject serversZone;


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
        runnerDeck.Shuffle();
        var grip = new Grip(gripZone, printer);
        var stack = new Stack(stackZone, runnerDeck, grip, printer);
        stack.PrepareTop();
        
        for (int i = 0; i < 5; i++)
        {
            stack.Draw();
        }
    }
}
