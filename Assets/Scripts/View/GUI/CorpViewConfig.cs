using model;
using UnityEngine;

namespace view.gui
{
    public class CorpViewConfig
    {
        public CorpView Display(Game game, BoardParts parts)
        {
            var zones = game.corp.zones;
            game.corp.credits.Observe(GameObject.Find("Corp/Credits/Credits text").AddComponent<CreditPoolText>());
            var servers = new ServerRow(GameObject.Find("Servers"), parts, zones);
            var archivesBox = servers.Box(zones.archives);
            archivesBox.Printer.Sideways = true;
            var rd = servers.Box(zones.rd).Printer.PrintCorpFacedown("Top of R&D");
            var hq = servers.Box(zones.hq).Printer.Print(game.corp.identity);
            zones.archives.Zone.ObserveCount(archivesBox.gameObject.AddComponent<PileCount>()); // TODO renders under the pile due to children-order
            zones.archives.Zone.ObserveAdditions(archivesBox);
            zones.archives.Zone.ObserveRemovals(archivesBox);
            zones.hq.Zone.ObserveCount(hq.AddComponent<PileCount>());
            zones.rd.Zone.ObserveCount(rd.AddComponent<PileCount>());
            return new CorpView(servers);
        }
    }
}
