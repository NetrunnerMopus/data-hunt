using UnityEngine;
using model;
using view.gui;
using model.ai;
using view.log;
using model.player;

public class GameConfig : MonoBehaviour
{
    public GameObject board;
    public RunnerView runnerView;
    public CorpView corpView;
    private Game game;

    void Awake()
    {
        var corpPlayer = new Player(
            deck: new Decks().DemoCorp(),
            pilot: new CorpAi(new System.Random(1234))
        );
        var runnerPlayer = new Player(
            deck: new Decks().DemoRunner(),
            pilot: new NoPilot()
        );
        game = new Game(corpPlayer, runnerPlayer);
    }

    void Start()
    {
        var flowView = new GameFlowView();
        var flowLog = new GameFlowLog();
        flowView.Display(board, game);
        flowLog.Display(game);
        runnerView.Display(game);
        corpView.Display(game);
        game.Start();
    }
}
