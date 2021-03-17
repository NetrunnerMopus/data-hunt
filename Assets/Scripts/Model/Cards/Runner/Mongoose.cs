using model.cards.types;

namespace model.cards.runner
{
    public class Mongoose : Card
    {
        public Mongoose(Game game) : base(game) { }
        override public string FaceupArt { get { return "mongoose"; } }
        override public string Name { get { return "Mongoose"; } }
        override public Faction Faction { get { return Factions.CRIMINAL; } }
        override public int InfluenceCost { get { return 2; } }
        override public ICost PlayCost => game.Costs.InstallProgram(this, 3);
        override public IEffect Activation => new effects.Nothing();
        override public IType Type { get { return new Program(); } }
    }
}
