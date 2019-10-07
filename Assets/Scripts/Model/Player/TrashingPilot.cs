using model.cards;
using model.choices;
using model.choices.trash;

namespace model.player
{
    public class TrashingPilot : DelegatingPilot
    {
        private readonly IDecision<Card, ITrashOption> trashing;

        public TrashingPilot(IDecision<Card, ITrashOption> trashing, IPilot basic) : base(basic)
        {
            this.trashing = trashing;
        }
        override public IDecision<Card, ITrashOption> ChooseTrashing() => trashing;
    }
}