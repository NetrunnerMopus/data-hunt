using model.cards.types;

namespace model.cards.runner
{
    public class QualityTime : Card
    {
        public QualityTime(Game game) : base(game) { }
        override public string FaceupArt { get { return "quality-time"; } }
        override public string Name { get { return "Quality Time"; } }
        override public Faction Faction { get { return Factions.SHAPER; } }
        override public int InfluenceCost { get { return 1; } }
        override public ICost PlayCost => game.runner.credits.PayingForPlaying(this, 3);
        override public IEffect Activation => game.runner.zones.Drawing(5);
        override public IType Type { get { return new Event(); } }
    }
}
