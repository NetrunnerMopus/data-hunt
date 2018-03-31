using model.effects.runner;
using model.costs;
using model.effects;

namespace model.cards
{
    public class ProcessAutomation : ICard
    {
        string ICard.FaceupArt { get { return "process-automation"; } }

        string ICard.Name { get { return "Process Automation"; } }

        bool ICard.Faceup { get { return false; } }

        Faction ICard.Faction { get { return Factions.MASK; } }

        int ICard.InfluenceCost { get { return 1; } }

        ICost ICard.PlayCost { get { return new RunnerCreditCost(0); } }

        IEffect ICard.PlayEffect { get { return new Sequence(new Gain(2), new Draw(1), new SelfTrash(this)); } }
    }
}