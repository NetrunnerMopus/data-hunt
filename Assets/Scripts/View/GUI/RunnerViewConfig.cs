using controller;
using model;
using UnityEngine;

namespace view.gui
{
    public class RunnerViewConfig
    {
        public void Display(Game game, CorpView corpView)
        {
            var grip = GameObject.Find("Grip");
            var stack = GameObject.Find("Stack");
            var rig = GameObject.Find("Rig");
            var playZone = GameObject.Find("Trigger").AddComponent<DropZone>();
            var rigZone = rig.AddComponent<DropZone>();
            var heapZone = GameObject.Find("Heap").AddComponent<DropZone>();
            var gripZone = grip.AddComponent<DropZone>();
            var gripFan = new GripFan(grip, game, playZone, rigZone, heapZone);
            var stackPile = new StackPile(stack, game, gripZone);
            new RigGrid(rig, game, playZone);
            GameObject.Find("Runner/Right hand/Core/Identity").AddComponent<CardPrinter>().Print(game.runner.identity);
            new RunInitiation(
                gameObject: GameObject.Find("Runner/Activation/Run"),
                serverRow: corpView.serverRow,
                game: game
            );
            GameObject.Find("Bank/Credit")
                .AddComponent<DroppableAbility>()
                .Represent(
                    game.runner.actionCard.credit,
                    game,
                    GameObject.Find("Runner/Right hand/Credits").AddComponent<DropZone>()
                );

            game.runner.credits.Observe(GameObject.Find("Runner/Right hand/Credits/Credits text").AddComponent<CreditPoolText>());
            var zones = game.runner.zones;
            zones.stack.ObserveCount(stackPile);
            zones.stack.ObservePopping(stackPile);
            zones.grip.ObserveAdditions(gripFan);
            zones.grip.ObserveRemovals(gripFan);
            zones.heap.Observe(Object.FindObjectOfType<HeapPile>());
        }
    }
}