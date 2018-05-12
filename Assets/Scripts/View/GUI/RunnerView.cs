using controller;
using model;
using UnityEngine;

namespace view.gui
{
    public class RunnerView : MonoBehaviour, IRunnerView
    {
        public void Display(Game game)
        {
            GripFan grip = FindObjectOfType<GripFan>();
            BankCredit bankCredit = FindObjectOfType<BankCredit>();
            StackPile stackPile = FindObjectOfType<StackPile>();
            grip.Game = game;
            bankCredit.Game = game;
            stackPile.Game = game;
            game.runner.clicks.Observe(FindObjectOfType<ClickPoolRow>());
            game.runner.credits.Observe(FindObjectOfType<CreditPoolText>());
            game.runner.actionCard.credit.Observe(new AbilityHighlight(bankCredit.gameObject.AddComponent<Highlight>()) , game);
            game.runner.actionCard.draw.Observe(new AbilityHighlight(stackPile.gameObject.AddComponent<Highlight>()), game);
            game.runner.stack.ObserveCount(stackPile);
            game.runner.stack.ObservePopping(stackPile);
            game.runner.grip.ObserveAdditions(grip);
            game.runner.grip.ObserveRemovals(grip);
            game.runner.heap.Observe(FindObjectOfType<HeapPile>());
        }

        public IRigView Rig { get { return FindObjectOfType<RigGrid>(); } }
    }
}