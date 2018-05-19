using controller;
using model;
using UnityEngine;

namespace view.gui
{
    public class RunnerView : MonoBehaviour
    {
        public void Display(Game game)
        {
            GripFan grip = FindObjectOfType<GripFan>();
            StackPile stackPile = FindObjectOfType<StackPile>();
            grip.Game = game;
            stackPile.Game = game;
            GameObject.Find("Bank/Credit")
                .AddComponent<Droppable>()
                .Represent(
                    game.runner.actionCard.credit,
                    game,
                    GameObject.Find("Runner/Left panel/Credits").AddComponent<DropZone>()
                );
            game.runner.clicks.Observe(FindObjectOfType<ClickPoolRow>());
            game.runner.credits.Observe(GameObject.Find("Runner/Left panel/Credits/Credits text").AddComponent<CreditPoolText>());
            var zones = game.runner.zones;
            zones.stack.ObserveCount(stackPile);
            zones.stack.ObservePopping(stackPile);
            zones.grip.ObserveAdditions(grip);
            zones.grip.ObserveRemovals(grip);
            zones.heap.Observe(FindObjectOfType<HeapPile>());
            zones.rig.ObserveInstallations(FindObjectOfType<RigGrid>());
        }
    }
}