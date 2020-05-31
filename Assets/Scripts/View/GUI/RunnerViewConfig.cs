using controller;
using model;
using UnityEngine;

namespace view.gui
{
    public class RunnerViewConfig
    {
        public void Display(Game game, CorpView corpView, BoardParts parts)
        {
            var playZone = GameObject.Find("Trigger").AddComponent<DropZone>();
            RigGrid rigGrid = new RigGrid(GameObject.Find("Rig"), game, playZone, parts);
            HeapPile heapPile = new HeapPile(GameObject.Find("Heap"), game, parts);
            GripFan gripFan = new GripFan(GameObject.Find("Grip"), game, playZone, rigGrid.DropZone, heapPile.DropZone, parts);
            StackPile stackPile = new StackPile(GameObject.Find("Stack"), game, gripFan.DropZone, parts);
            new ZoneBox(GameObject.Find("Runner/Left hand/Score"), parts).Represent(game.runner.zones.score.zone);
            parts.Print(GameObject.Find("Runner/Right hand/Core/Identity")).Print(game.runner.identity);
            new RunInitiation(
                gameObject: GameObject.Find("Runner/Activation/Run"),
                serverRow: corpView.serverRow,
                game: game
            );
            var credits = GameObject.Find("Runner/Right hand/Credits");
            GameObject.Find("Bank/Credit")
                .AddComponent<DroppableAbility>()
                .Represent(
                    game.runner.actionCard.credit,
                    game,
                    credits.AddComponent<DropZone>()
                );
            game.runner.credits.Observe(credits.AddComponent<CreditSpiral>());
            game.runner.credits.Observe(credits.AddComponent<PileCount>());
        }
    }
}