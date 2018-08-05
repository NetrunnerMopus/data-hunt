using UnityEngine;
using model;
using view.gui;
using model.ai;
using view.log;

public class GameConfig : MonoBehaviour
{
    public GameObject board;
    public RunnerView runnerView;
    public CorpView corpView;
    private Game game;

    void Awake()
    {
        game = new Game(new Decks().DemoCorp(), new Decks().DemoRunner());
    }

    void Start()
    {
        var flowView = new GameFlowView();
        var flowLog = new GameFlowLog();
        flowView.Display(board, game);
        flowLog.Display(game);
        runnerView.Display(game);
        corpView.Display(game);
        var ai = new CorpAi(game, new System.Random(1234));
        ai.Play();
        game.Start();
    }
}
