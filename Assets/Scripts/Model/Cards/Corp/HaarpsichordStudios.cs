using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.steal;
using model.timing;

namespace model.cards.corp
{
    public class HaarpsichordStudios : Card
    {
        override public string FaceupArt => "haarpsichord-studios";
        override public string Name => "Haarpsichord Studios";
        override public Faction Faction => Factions.NBN;
        override public int InfluenceCost { get { throw new System.Exception("Identities don't have an influence cost"); } }
        override public ICost PlayCost { get { throw new System.Exception("Identities don't have a play cost"); } }
        override public IEffect Activation => new HaarpsichordEffect();
        override public IType Type => new Identity();

        private class HaarpsichordEffect : IEffect
        {
            IEnumerable<string> IEffect.Graphics => new string[] { };
            void IEffect.Observe(IImpactObserver observer, Game game) => observer.NotifyImpact(true, this);

            async Task IEffect.Resolve(Game game)
            {
                var memory = new HaarpsichordMemory();
                game.corp.turn.Started += memory.Reset;
                game.runner.turn.Started += memory.Reset;
                var mod = new HaarpsichordModifier(memory);
                game.runner.ModifyStealing(mod);
                await Task.CompletedTask;
            }
        }

        private class HaarpsichordMemory
        {
            public bool AgendaStolenThisTurn = false;

            public void Reset(object sender, ITurn turn)
            {
                AgendaStolenThisTurn = false;
            }
        }

        private class HaarpsichordModifier : IStealModifier
        {
            private HaarpsichordMemory memory;

            public HaarpsichordModifier(HaarpsichordMemory memory)
            {
                this.memory = memory;
            }

            IStealOption IStealModifier.Modify(IStealOption option)
            {
                return new StealingWhileHaarpsichordWatches(memory, option);
            }
        }

        private class StealingWhileHaarpsichordWatches : IStealOption
        {
            private HaarpsichordMemory memory;
            private IStealOption original;
            public string Art => original.Art;

            public StealingWhileHaarpsichordWatches(HaarpsichordMemory memory, IStealOption original)
            {
                this.memory = memory;
                this.original = original;
            }

            public bool IsLegal(Game game)
            {
                return !memory.AgendaStolenThisTurn && original.IsLegal(game);
            }

            async public Task<bool> Perform(Game game)
            {
                if (memory.AgendaStolenThisTurn)
                {
                    throw new System.Exception("Illegal steal from Haarpsichord");
                }
                var stolen = await original.Perform(game);
                if (stolen)
                {
                    memory.AgendaStolenThisTurn = true;
                }
                return stolen;
            }
        }
    }
}
