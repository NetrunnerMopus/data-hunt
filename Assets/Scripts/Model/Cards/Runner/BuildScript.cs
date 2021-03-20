using model.cards.types;
using model.effects;

namespace model.cards.runner
{
    public class BuildScript : Card
    {
        public BuildScript(Game game) : base(game) { }
        override public string FaceupArt { get { return "build-script"; } }
        override public string Name { get { return "Build Script"; } }
        override public Faction Faction { get { return Factions.MASQUE; } }
        override public int InfluenceCost { get { return 1; } }
        override public ICost PlayCost => game.runner.credits.PayingForPlaying(this, 0);
        override public IEffect Activation => new Sequence(game.runner.credits.Gaining(1), game.runner.zones.Drawing(2));
        override public IType Type { get { return new Event(); } }
    }
}
