using model;
using UnityEngine;

namespace view.gui
{
    public class CorpViewConfig
    {
        public CorpView Display(Game game)
        {
            var zones = game.corp.zones;
            game.corp.credits.Observe(GameObject.Find("Corp/Credits/Credits text").AddComponent<CreditPoolText>());
            var servers = GameObject.Find("Servers").AddComponent<ServerRow>();
            servers.Represent(zones);
            var archives = servers.Box(zones.archives).gameObject;
            var hq = servers.Box(zones.hq).Printer.Print(game.corp.identity);
			var hqCount = new GameObject("HQ card count");
			hqCount.AttachTo(hq);
            servers.Box(zones.rd);
            zones.hq.ObserveCount(hqCount.AddComponent<PileCount>());
            zones.archives.Observe(archives.AddComponent<ArchivesPile>());
            return new CorpView(servers);
        }
    }
}