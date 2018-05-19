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
            game.runner.stack.ObserveCount(stackPile);
            game.runner.stack.ObservePopping(stackPile);
            game.runner.grip.ObserveAdditions(grip);
            game.runner.grip.ObserveRemovals(grip);
            game.runner.heap.Observe(FindObjectOfType<HeapPile>());
            game.runner.rig.ObserveInstallations(FindObjectOfType<RigGrid>());
        }
    }
}