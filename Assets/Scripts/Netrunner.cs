using UnityEngine;
using UnityEngine.UI;

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
        SetupStack();
        runnerDeck.Shuffle();
        for (int i = 0; i < 5; i++)
        {
            RunnerDraw();
        }
    }

    private void SetupStack()
    {
        var runnerCardBack = "Images/UI/runner-card-back";
        var stack = printer.PrintRunnerFacedown("Top of the stack", stackZone.transform);
        stack.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
        stack.AddComponent<FaceupCard>();
    }

    private void RunnerDraw()
    {
        var card = runnerDeck.Draw();
        AddToGrip(card);
    }

    private void AddToGrip(Card card)
    {
        var gameObject = printer.Print(card.name, "Images/Cards/" + card.id, gripZone.gameObject.transform);
        gameObject.AddComponent<FaceupCard>();
    }
}
