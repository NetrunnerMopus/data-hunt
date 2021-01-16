using model.cards.types;
using model.effects;

namespace model.cards.runner
{
    public class OmarKeung : Card
    {
        override public string FaceupArt => "omar-keung";
        override public string Name => "Omar Keung";
        override public Faction Faction => Factions.ANARCH;
        override public int InfluenceCost { get { throw new System.Exception("Identities don't have an influence cost"); } }
        override public ICost PlayCost { get { throw new System.Exception("Identities don't have a play cost"); } }
        override public IEffect Activation => new Nothing(); // TODO impl
        override public IType Type => new Identity();
    }
}
