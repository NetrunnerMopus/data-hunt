using model;
using UnityEngine;

namespace view.gui
{
    public class CorpView : MonoBehaviour
    {
        public void Display(Game game)
        {
            var zones = game.corp.zones;
            game.corp.credits.Observe(GameObject.Find("Corp/Credits/Credits text").AddComponent<CreditPoolText>());
            var servers = GameObject.Find("Servers").AddComponent<ServerRow>();
            servers.Represent(zones);
            var archives = servers.CreateServer("Archives").gameObject;
            var hq = servers.CreateServer("HQ").Printer.Print(game.corp.identity);
            servers.CreateServer("R&D");
            zones.hq.ObserveCount(hq.AddComponent<HqCount>());
            zones.archives.Observe(archives.AddComponent<ArchivesPile>());
        }
    }
}