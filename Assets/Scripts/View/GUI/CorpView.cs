using model;
using model.cards.corp;
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
            var hq = printer.Print(new CustomBiotics());
            game.corp.zones.hq.ObserveCount(hq.AddComponent<HqCount>());
            printer.PrintCorpFacedown("Remote");
        }
    }
}