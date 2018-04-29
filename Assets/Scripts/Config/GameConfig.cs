using UnityEngine;
using model;
using view;
using controller;
using view.gui;
using view.log;
using model.play.runner;
using view.memory;

public class GameConfig : MonoBehaviour
{
    public static Game game;

    public ActionCardConfig actionCardConfig;
    public GripFan gripFan;
    public StackPile stackPile;
    public HeapPile heapPile;
    public RigGrid rigGrid;
    public PlayZone playZone;
    public CardPrinter serversZone;
    public ClickPoolRow clickPoolRow;
    public CreditPoolText creditPoolText;

    private Deck runnerDeck = new Decks().DemoRunner();

    void Awake()
    {
        game = new Game();
        game.runner = SetupRunner(game);
        game.corp = SetupCorporation();
    }

    void Start()
    {
        game.AttachView(new RunnerView(actionCardConfig.View()));
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
        var grip = new Grip(gripFan);
        var stack = new Stack(runnerDeck, stackPile);
        var heap = new Heap(heapPile);
        var rig = new Rig(rigGrid);
        var clicks = new ClickPool(clickPoolRow);
        var credits = new CreditPool(creditPoolText);
        var runner = new Runner(game, actionCard, grip, stack, heap, rig, clicks, credits);
        return runner;
    }
}
