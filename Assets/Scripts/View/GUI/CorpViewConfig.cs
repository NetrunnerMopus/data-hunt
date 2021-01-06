using model;
using UnityEngine;

namespace view.gui
{
    public class CorpViewConfig
    {
        public CorpView Display(Game game, BoardParts parts)
        {
            var zones = game.corp.zones;
            var credits = GameObject.Find("Corp/Credits");
            game.corp.credits.Observe(credits.AddComponent<CreditSpiral>());
            game.corp.credits.Observe(credits.AddComponent<PileCount>());
            var servers = new ServerRow(GameObject.Find("Servers"), parts, zones);
            var archivesBox = servers.Box(zones.archives);
            archivesBox.Printer.Sideways = true;
            var rd = servers.Box(zones.rd).Printer.PrintCorpFacedown("Top of R&D");
            var hq = servers.Box(zones.hq).Printer.Print(game.corp.identity);
            zones.archives.Zone.ObserveCount(archivesBox.box.AddComponent<PileCount>()); // TODO renders under the pile due to children-order
            zones.archives.Zone.ObserveAdditions(archivesBox);
            zones.archives.Zone.ObserveRemovals(archivesBox);
            zones.hq.Zone.ObserveCount(hq.AddComponent<PileCount>());
            zones.rd.Zone.ObserveCount(rd.AddComponent<PileCount>());
            return new CorpView(servers);
        }
    }
}
