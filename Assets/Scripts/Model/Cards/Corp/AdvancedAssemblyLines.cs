using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.trash;
using model.costs;
using model.effects;
using model.effects.corp;
using model.play;
using model.zones;
using model.zones.corp;

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
        override public IEffect Activation => new AdvancedAssemblyLinesActivation(this);
        override public IType Type => new Asset();
        override public IList<ITrashOption> TrashOptions(Game game) => new List<ITrashOption> {
            new Leave(),
            new PayToTrash(game.Costs.Trash(this, 1), this)
        };

        private class AdvancedAssemblyLinesActivation : IEffect
        {
            public bool Impactful => true;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            private readonly Card card;
            IEnumerable<string> IEffect.Graphics => new string[] { };

            public AdvancedAssemblyLinesActivation(Card card)
            {
                this.card = card;
            }

            async Task IEffect.Resolve(Game game)
            {
                IEffect gain = new Gain(3);
                await gain.Resolve(game);
                var paidWindow = game.corp.paidWindow;
                var archives = game.corp.zones.archives.Zone;
                var pop = new Ability(
                    cost: new Conjunction(paidWindow.Permission(), new Trash(card, archives), new Active(card)),
                    effect: new AdvancedAssemblyLinesInstall(game.corp.zones.hq)
                );
                paidWindow.Add(pop, card);
                card.Moved += delegate (Card card, Zone source, Zone target) { paidWindow.Remove(pop); };
            }
        }

        private class AdvancedAssemblyLinesInstall : IEffect
        {
            public bool Impactful => installables.Count > 0;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new string[] { };
            private List<Card> installables = new List<Card>();
            private Headquarters hq;

            public AdvancedAssemblyLinesInstall(Headquarters hq)
            {
                this.hq = hq;
                hq.Zone.Changed += UpdateInstallables;
            }

            async Task IEffect.Resolve(Game game)
            {
                var pilot = game.corp.pilot;
                var installable = await pilot.ChooseACard().Declare("Which card to install?", installables, game);
                var install = new GenericInstall(installable, game.corp.pilot);
                await install.Resolve(game);
            }

            private void UpdateInstallables(Zone hqZone)
            {
                installables = hq.Zone.Cards.Where(card => (card.Type.Installable && !(card.Type is Agenda))).ToList();
                ChangedImpact(this, Impactful);
            }
        }
    }
}
