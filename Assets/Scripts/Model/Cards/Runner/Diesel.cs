using model.cards.types;

namespace model.cards.runner
{
    public class Diesel : Card
    {
        public Diesel(Game game) : base(game) { }
        override public string FaceupArt { get { return "diesel"; } }
        override public string Name { get { return "Diesel"; } }
        override public Faction Faction { get { return Factions.SHAPER; } }
        override public int InfluenceCost { get { return 2; } }
        override public ICost PlayCost => game.Costs.PlayEvent(this, 0);
        override public IEffect Activation => game.runner.zones.Drawing(3);
        override public IType Type { get { return new Event(); } }
    }
}
