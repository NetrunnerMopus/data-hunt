using UnityEngine;
using model;
using view;
using view.gui;
using model.play.runner;

public class GameConfig : MonoBehaviour
{
    public static Game game;

    public RunnerView runnerView;
    public CardPrinter serversZone;

    private Deck runnerDeck = new Decks().DemoRunner();

    void Awake()
    {
        game = new Game(runnerDeck, runnerView)
        {
            corp = SetupCorporation()
        };
    }

    void Start()
    {
        runnerView.Display(game);
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

    private Runner SetupRunner(Game game)
    {
        var actionCard = new ActionCard();
        var grip = new Grip(runnerView.Grip);
        var stack = new Stack(runnerDeck, runnerView.Stack);
        var heap = new Heap();
        var rig = new Rig(runnerView.Rig);
        var clicks = new ClickPool();
        var credits = new CreditPool();
        var runner = new Runner(game, actionCard, grip, stack, heap, rig, clicks, credits);
        return runner;
    }
}
