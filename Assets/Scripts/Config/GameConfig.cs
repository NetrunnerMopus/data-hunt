using UnityEngine;
using model;
using view.gui;

public class GameConfig : MonoBehaviour
{
    public RunnerView runnerView;
    public CorpView corpView;

    private Game game;
    private Deck runnerDeck = new Decks().DemoRunner();

    void Awake()
    {
        game = new Game(runnerDeck);
    }

    void Start()
    {
        runnerView.Display(game);
        corpView.Display(game);
        game.Start();
    }
}
