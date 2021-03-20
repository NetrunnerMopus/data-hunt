using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.trash;
using model.costs;
using model.play;
using model.zones;

namespace model.cards.corp
{
    public class AdvancedAssemblyLines : Card
    {
        public AdvancedAssemblyLines(Game game) : base(game) { }
        override public string FaceupArt => "advanced-assembly-lines";
        override public string Name => "Advanced Assembly Lines";
        override public Faction Faction => Factions.HAAS_BIOROID;
        override public int InfluenceCost => 2;
        override public ICost PlayCost => game.corp.credits.PayingForPlaying(this, 1);
        override public IEffect Activation => new AdvancedAssemblyLinesActivation(this, game.corp);
        override public IType Type => new Asset(game);
        override public IList<ITrashOption> TrashOptions() => new List<ITrashOption> {
            new Leave(),
            new PayToTrash(1, this, game)
        };

        private class AdvancedAssemblyLinesActivation : IEffect
        {
            public bool Impactful => true;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            private readonly Card aal;
            private readonly Corp corp;
            IEnumerable<string> IEffect.Graphics => new string[] { };

            public AdvancedAssemblyLinesActivation(Card aal, Corp corp)
            {
                this.aal = aal;
                this.corp = corp;
            }

            async Task IEffect.Resolve()
            {
                await corp.credits.Gaining(3).Resolve();
                var paidWindow = corp.paidWindow;
                var archives = corp.zones.archives.Zone;
                var pop = new Ability(
                    cost: new Conjunction(paidWindow.Permission(), new Trash(aal, archives), new Active(aal)),
                    effect: new AdvancedAssemblyLinesInstall(corp)
                );
                paidWindow.Add(pop, aal);
                aal.Moved += (card, source, target) => paidWindow.Remove(pop);
            }
        }

        private class AdvancedAssemblyLinesInstall : IEffect
        {
            public bool Impactful => installables.Count > 0;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new string[] { };
            private List<Card> installables = new List<Card>();
            private Corp corp;

            public AdvancedAssemblyLinesInstall(Corp corp)
            {
                this.corp = corp;
                corp.zones.hq.Zone.Changed += UpdateInstallables;
            }

            async Task IEffect.Resolve()
            {
                var installable = await corp.pilot.ChooseACard().Declare("Which card to install?", installables);
                await corp.Installing.InstallingCard(installable).Resolve();
            }

            private void UpdateInstallables(Zone hqZone)
            {
                installables = corp.zones.hq.Zone.Cards.Where(card => (card.Type.Installable && !(card.Type is Agenda))).ToList();
                ChangedImpact(this, Impactful);
            }
        }
    }
}
