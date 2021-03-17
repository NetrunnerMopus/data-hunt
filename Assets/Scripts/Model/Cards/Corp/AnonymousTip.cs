using model.cards.types;
using model.costs;
using model.effects.corp;

namespace model.cards.corp
{
    public class AnonymousTip : Card
    {
        public AnonymousTip(Game game) : base(game) { }
        override public string FaceupArt => "anonymous-tip";
        override public string Name => "Anonymous Tip";
        override public Faction Faction => Factions.NBN;
        override public int InfluenceCost => 1;
        override public ICost PlayCost => new Nothing();
        override public IEffect Activation => new Draw(3);
        override public IType Type => new Operation();
    }
}
