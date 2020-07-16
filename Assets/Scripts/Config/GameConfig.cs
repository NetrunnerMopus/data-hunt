﻿using UnityEngine;
using model;
using view.gui;
using model.ai;
using view.log;
using model.player;
using model.zones;
using view;
using view.gui.timecross;

public class GameConfig : MonoBehaviour
{
    void Start()
    {
        var board = GameObject.Find("/Board");
        var perception = new RunnerPerception();
        var zoom = new CardZoom(board, perception);
        var parts = new BoardParts(board, perception, zoom);
        var corpPlayer = new Player(
            deck: new Decks().DemoCorp(),
            pilot: new CorpAi(new System.Random(1234))
        );
        var runnerPlayer = new Player(
            deck: new Decks().DemoRunner(),
            pilot: new CardPickingPilot(
                new CardChoiceScreen(parts),
                new TrashingPilot(
                    new TrashChoiceScreen(parts),
                    new AutoPaidWindowPilot(
                        new SingleChoiceMaker(
                            new NoPilot()
                        )
                    )
                )
            )
        );
        var game = new Game(corpPlayer, runnerPlayer, new Shuffling(10006));
        var flowView = new GameFlowView();
        var flowLog = new GameFlowLog();
        flowView.Display(board, game);
        flowLog.Display(game);
        var corpView = new CorpViewConfig().Display(game, parts);
        new RunnerViewConfig().Display(game, flowView, corpView, parts);
        game.Start();
    }
}
