using model;
using UnityEngine;

namespace view.gui
{
    public class CorpView : MonoBehaviour
    {
        public void Display(Game game)
        {
            game.corp.credits.Observe(GameObject.Find("Corp/Credits/Credits text").AddComponent<CreditPoolText>());
            var serverZone = GameObject.Find("Servers");
            var printer = serverZone.AddComponent<CardPrinter>();
            printer.PrintCorpFacedown("Archives");
            printer.PrintCorpFacedown("R&D");
            printer.PrintCorpFacedown("HQ");
            printer.PrintCorpFacedown("Remote");
        }
    }
}