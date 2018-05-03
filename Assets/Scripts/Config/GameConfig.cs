using UnityEngine;
using model;
using view;
using view.gui;

public class GameConfig : MonoBehaviour
{
    public RunnerView runnerView;
    public CardPrinter serversZone;

    private Game game;
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
}
