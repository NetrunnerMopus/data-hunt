using model;
using model.ai;
using model.player;
using model.zones;
using UnityEngine;
using view;
using view.gui;
using view.log;

public class GameConfig : MonoBehaviour
{

    private GameMenu gameMenu;

    void Start()
    {
        var board = GameObject.Find("/Board");
        gameMenu = board.GetComponentInChildren<GameMenu>();
        gameMenu.Resume();
        var perception = new RunnerPerception();
        var zoom = new CardZoom(board, perception);
        var parts = new BoardParts(board, perception, zoom);
        var corpPilot = new CorpAi(new System.Random(1234));
        var runnerPilot = new CardPickingPilot(
            new CardChoiceScreen(parts),
            new TrashingPilot(
                new TrashChoiceScreen(parts),
                new StealingPilot(
                    new StealChoiceScreen(parts),
                    new AutoPaidWindowPilot(
                        new SingleChoiceMaker(
                            new NoPilot()
                        )
                    )
                )
            )
        );
        var game = new Game(new Shuffling(10006));
        var corpDeck = new Decks().DemoCorp(game);
        var runnerDeck = new Decks().DemoRunner(game);
        var flowView = new GameFlowView();
        var flowLog = new GameFlowLog();
        flowView.Display(board, game);
        flowLog.Display(game);
        var corpView = new CorpViewConfig().Display(game, parts);
        new RunnerViewConfig().Display(game, flowView, corpView, parts);
        game.Start(corpDeck, runnerDeck);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gameMenu.Open();
        }
    }
}
