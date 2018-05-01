using controller;
using model;
using UnityEngine;

namespace view.gui
{
    public class RunnerView : MonoBehaviour, IRunnerView
    {
        public ActionCardView ActionCard { get; set; }

        public void Display(Game game)
        {
            game.runner.clicks.Observe(FindObjectOfType<ClickPoolRow>());
            game.runner.credits.Observe(FindObjectOfType<CreditPoolText>());
            game.runner.actionCard.credit.Observe(new AbilityHighlight(FindObjectOfType<BankCredit>().gameObject.AddComponent<Highlight>()) , game);
        }
    }
}