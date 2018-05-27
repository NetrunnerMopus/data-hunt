using model.effects.runner;
using model.costs;
using model.effects;
using model.cards.types;

namespace model.cards.runner
{
    public class ProcessAutomation : ICard
    {
        string ICard.FaceupArt { get { return "process-automation"; } }

        string ICard.Name { get { return "Process Automation"; } }

        bool ICard.Faceup { get { return false; } }

        Faction ICard.Faction { get { return Factions.MASK; } }

        int ICard.InfluenceCost { get { return 1; } }

        ICost ICard.PlayCost { get { return new RunnerCreditCost(0); } }

        IEffect ICard.Activation => new Sequence(new Gain(2), new Draw(1));

        IType ICard.Type { get { return new Event(); } }
    }
}