using controller;
using model;
using UnityEngine;

namespace view.gui
{
    public class RunnerView : MonoBehaviour, IRunnerView
    {
        public void Display(Game game)
        {
            game.runner.clicks.Observe(FindObjectOfType<ClickPoolRow>());
            game.runner.credits.Observe(FindObjectOfType<CreditPoolText>());
            game.runner.actionCard.credit.Observe(new AbilityHighlight(FindObjectOfType<BankCredit>().gameObject.AddComponent<Highlight>()) , game);
            game.runner.actionCard.draw.Observe(new AbilityHighlight(FindObjectOfType<StackPile>().gameObject.AddComponent<Highlight>()), game);
            game.runner.heap.Observe(FindObjectOfType<HeapPile>());
        }

        public IGripView Grip { get { return FindObjectOfType<GripFan>(); } }
        public IStackView Stack { get { return FindObjectOfType<StackPile>(); } }
        public IRigView Rig { get { return FindObjectOfType<RigGrid>(); } }
    }
}