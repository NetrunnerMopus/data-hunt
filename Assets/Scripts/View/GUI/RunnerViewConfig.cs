using controller;
using model;
using UnityEngine;

namespace view.gui
{
    public class RunnerViewConfig
    {
        public void Display(Game game, CorpView corpView)
        {
            GripFan grip = Object.FindObjectOfType<GripFan>();
            StackPile stackPile = Object.FindObjectOfType<StackPile>();
            RigGrid rig = Object.FindObjectOfType<RigGrid>();
            var playZone = GameObject.Find("Trigger").AddComponent<DropZone>();
            var rigZone = GameObject.Find("Rig").AddComponent<DropZone>();
            var heapZone = GameObject.Find("Heap").AddComponent<DropZone>();
            var gripZone = GameObject.Find("Grip").AddComponent<DropZone>();
            grip.Construct(game, playZone, rigZone, heapZone);
            stackPile.Construct(game, gripZone);
            rig.Construct(game, playZone);
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
            zones.stack.ObserveCount(stackPile.gameObject.AddComponent<PileCount>());
            zones.stack.ObservePopping(stackPile);
            zones.grip.ObserveAdditions(grip);
            zones.grip.ObserveRemovals(grip);
            zones.heap.Observe(Object.FindObjectOfType<HeapPile>());
        }
    }
}