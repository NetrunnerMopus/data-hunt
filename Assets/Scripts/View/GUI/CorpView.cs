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
            var servers = GameObject.Find("Servers").AddComponent<ServerRow>();
            servers.CreateServer("R&D");
            var hq = servers.CreateServer("HQ").Printer.Print(new CustomBiotics());
            var archives = servers.CreateServer("Archives").gameObject;
            game.corp.zones.hq.ObserveCount(hq.AddComponent<HqCount>());
            game.corp.zones.archives.Observe(archives.AddComponent<ArchivesPile>());
        }
    }
}