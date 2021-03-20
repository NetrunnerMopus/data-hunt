using model.cards;
using model.choices;
using model.steal;

namespace model.player
{
    public class StealingPilot : DelegatingPilot
    {
        private readonly IDecision<Card, IStealOption> stealing;

        public StealingPilot(IDecision<Card, IStealOption> stealing, IPilot basic) : base(basic)
        {
            this.stealing = stealing;
        }
        override public IDecision<Card, IStealOption> ChooseStealing() => stealing;
    }
}
