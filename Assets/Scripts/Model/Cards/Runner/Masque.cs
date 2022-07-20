using model.cards.types;

namespace model.cards.runner
{
    public class TheMasque : Card
    {
        public TheMasque(Game game) : base(game) { }
        override public string FaceupArt => "the-masque";
        override public string Name => "The Masque";
        override public Faction Faction => Factions.MASQUE;
        override public int InfluenceCost { get { throw new System.Exception("Identities don't have an influence cost"); } }
        override public ICost PlayCost { get { throw new System.Exception("Identities don't have a play cost"); } }
        override public IEffect Activation => new effects.Nothing();
        override public IType Type => new RunnerIdentity();
    }
}
