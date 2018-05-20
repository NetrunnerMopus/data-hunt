using UnityEngine;
using model;
using view.gui;
using model.ai;

public class GameConfig : MonoBehaviour
{
    public RunnerView runnerView;
    public CorpView corpView;
    private Game game;

    void Awake()
    {
        game = new Game(new Decks().DemoCorp(), new Decks().DemoRunner());
    }

    void Start()
    {
        runnerView.Display(game);
        corpView.Display(game);
        var ai = new CorpAi(game);
        ai.Play();
        game.Start();
    }
}
