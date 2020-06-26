﻿using controller;
using model;
using UnityEngine;

namespace view.gui
{
    public class RunnerViewConfig
    {
        public void Display(Game game, GameFlowView flowView, CorpView corpView, BoardParts parts)
        {
            var playZone = GameObject.Find("Choice").AddComponent<DropZone>();
            RigGrid rigGrid = new RigGrid(GameObject.Find("Rig"), game, flowView.PaidWindow.Sink, parts);
            HeapPile heapPile = new HeapPile(GameObject.Find("Heap"), game, parts);
            GripFan gripFan = new GripFan(GameObject.Find("Grip"), game, playZone, rigGrid.DropZone, heapPile.DropZone, parts);
            StackPile stackPile = new StackPile(GameObject.Find("Stack"), game, gripFan.DropZone, parts);
            new ZoneBox(GameObject.Find("Runner/Left hand/Score"), parts).Represent(game.runner.zones.score.zone);
            parts.Print(GameObject.Find("Runner/Right hand/Core/Identity")).Print(game.runner.identity);
            new RunInitiation(
                gameObject: GameObject.Find("Run"),
                serverRow: corpView.serverRow,
                game: game
            );
            var credits = GameObject.Find("Runner/Right hand/Credits");
            GameObject.Find("Bank/Credit")
                .AddComponent<Droppable>()
                .Represent(
                    new InteractiveAbility(game.runner.actionCard.credit, game),
                    credits.AddComponent<DropZone>()
                );
            game.runner.credits.Observe(credits.AddComponent<CreditSpiral>());
            game.runner.credits.Observe(credits.AddComponent<PileCount>());
        }
    }
}
