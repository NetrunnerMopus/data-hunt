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
			var rd = servers.Box(zones.rd).Printer.PrintCorpFacedown("Top of R&D");
			var hq = servers.Box(zones.hq).Printer.Print(game.corp.identity);
            zones.archives.Observe(archives.AddComponent<ArchivesPile>());
			zones.hq.Zone.ObserveCount(hq.AddComponent<PileCount>());
			zones.rd.Zone.ObserveCount(rd.AddComponent<PileCount>());
			return new CorpView(servers);
        }
    }
}
