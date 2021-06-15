using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.trash;
using model.costs;
using model.play;
using model.player;
using model.timing;
using model.zones;

namespace model.cards.corp {
    public class AdvancedAssemblyLines : Card {
        public AdvancedAssemblyLines(Game game) : base(game) { }
        override public string FaceupArt => "advanced-assembly-lines";
        override public string Name => "Advanced Assembly Lines";
        override public Faction Faction => Factions.HAAS_BIOROID;
        override public int InfluenceCost => 2;
        override public ICost PlayCost => game.corp.credits.PayingForPlaying(this, 1);
        override public IType Type => new Asset(game);
        override public IList<ITrashOption> TrashOptions() => new List<ITrashOption> {
            new Leave(),
            new PayToTrash(1, this, game)
        };

        async protected override Task Activate() {
            await game.corp.credits.Gaining(3).Resolve();
            game.Timing.PaidWindowDefined += DefineTrashAbility;
        }

        async protected override Task Deactivate() {
            game.Timing.PaidWindowDefined -= DefineTrashAbility;
        }

        private void DefineTrashAbility(PaidWindow paidWindow) {
            var archives = game.corp.zones.archives.Zone;
            var aalInstall = new AdvancedAssemblyLinesInstall(game.corp);
            var pop = new Ability(
                cost: new Conjunction(paidWindow.Permission(), new Trash(aal, archives), new Active(aal)),
                effect: aalInstall,
                aal
            ).BelongingTo(aal);
            paidWindow.Add(pop);
        }

        private class AdvancedAssemblyLinesInstall : IEffect, IDisposable {
            public bool Impactful => Installables().Count > 0;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new string[] { };
            private Corp corp;

            public AdvancedAssemblyLinesInstall(Corp corp) {
                this.corp = corp;
                corp.zones.hq.Zone.Changed += UpdateInstallables;
            }

            private IList<Card> Installables() => corp.zones.hq.Zone.Cards.Where(card => (card.Type.Installable && !(card.Type is Agenda))).ToList();

            async Task IEffect.Resolve(IPilot pilot) {
                var installable = await corp.pilot.ChooseACard().Declare("Which card to install?", Installables());
                await corp.Installing.InstallingCard(installable).Resolve(pilot);
            }

            private void UpdateInstallables(Zone hqZone) {
                ChangedImpact(this, Impactful);
            }

            public void Dispose() {
                corp.zones.hq.Zone.Changed -= UpdateInstallables;
            }
        }
    }
}
