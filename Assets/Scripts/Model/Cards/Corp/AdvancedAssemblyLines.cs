using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.trash;
using model.costs;
using model.effects;
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
        override public ICost PlayCost => game.Costs.Rez(this, 1);
        override public IEffect Activation => new AdvancedAssemblyLinesActivation(this, game);
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
            private readonly Game game;
            IEnumerable<string> IEffect.Graphics => new string[] { };

            public AdvancedAssemblyLinesActivation(Card aal, Game game)
            {
                this.aal = aal;
                this.game = game;
            }

            async Task IEffect.Resolve()
            {
                await game.corp.credits.Gaining(3).Resolve();
                var paidWindow = game.corp.paidWindow;
                var archives = game.corp.zones.archives.Zone;
                var pop = new Ability(
                    cost: new Conjunction(paidWindow.Permission(), new Trash(aal, archives), new Active(aal)),
                    effect: new AdvancedAssemblyLinesInstall(game)
                );
                paidWindow.Add(pop, aal);
                aal.Moved += delegate (Card card, Zone source, Zone target) { paidWindow.Remove(pop); };
            }
        }

        private class AdvancedAssemblyLinesInstall : IEffect
        {
            public bool Impactful => installables.Count > 0;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new string[] { };
            private List<Card> installables = new List<Card>();
            private Game game;

            public AdvancedAssemblyLinesInstall(Game game)
            {
                this.game = game;
                game.corp.zones.hq.Zone.Changed += UpdateInstallables;
            }

            async Task IEffect.Resolve()
            {
                var pilot = game.corp.pilot;
                var installable = await pilot.ChooseACard().Declare("Which card to install?", installables);
                var install = new GenericInstall(installable, game.corp.pilot, game);
                await install.Resolve();
            }

            private void UpdateInstallables(Zone hqZone)
            {
                installables = game.corp.zones.hq.Zone.Cards.Where(card => (card.Type.Installable && !(card.Type is Agenda))).ToList();
                ChangedImpact(this, Impactful);
            }
        }
    }
}
