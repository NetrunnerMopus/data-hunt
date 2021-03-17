using model.cards.types;
using model.effects;

namespace model.cards.runner
{
    public class ProcessAutomation : Card
    {
        public ProcessAutomation(Game game) : base(game) { }
        override public string FaceupArt { get { return "process-automation"; } }
        override public string Name { get { return "Process Automation"; } }
        override public Faction Faction { get { return Factions.MASQUE; } }
        override public int InfluenceCost { get { return 1; } }
        override public ICost PlayCost => game.Costs.PlayEvent(this, 0);
        override public IEffect Activation => new Sequence(game.runner.credits.Gaining(1), game.runner.zones.Drawing(2));
        override public IType Type { get { return new Event(); } }
    }
}
