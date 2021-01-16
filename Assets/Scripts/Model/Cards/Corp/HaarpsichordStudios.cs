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
                var mod = new HaarpsichordModifier();
                game.corp.turn.Started += mod.Reset;
                game.runner.turn.Started += mod.Reset;
                game.runner.ModifyStealing(mod);
                await Task.CompletedTask;
            }
        }

        private class HaarpsichordModifier : IStealModifier
        {
            public bool AgendaStolenThisTurn = false;

            public void Reset(object sender, ITurn turn)
            {
                AgendaStolenThisTurn = false;
            }

            IStealOption IStealModifier.Modify(IStealOption option)
            {
                if (AgendaStolenThisTurn)
                {
                    return new CannotSteal();
                }
                else
                {
                    return new StealingWhileHaarpsichordWatches(option, this);
                }
            }
        }

        private class StealingWhileHaarpsichordWatches : IStealOption
        {
            private HaarpsichordModifier modifier;
            private IStealOption original;
            public string Art => original.Art;

            public StealingWhileHaarpsichordWatches(IStealOption original, HaarpsichordModifier modifier)
            {
                this.original = original;
                this.modifier = modifier;
            }

            public bool IsLegal(Game game)
            {
                return original.IsLegal(game);
            }

            async public Task<bool> Perform(Game game)
            {
                var stolen = await original.Perform(game);
                if (stolen)
                {
                    modifier.AgendaStolenThisTurn = true;
                }
                return stolen;
            }
        }
    }
}
