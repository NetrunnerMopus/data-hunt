using model.cards;
using model.choices;
using model.choices.trash;

namespace model.player
{
    public class TrashingPilot : DelegatingPilot
    {
        private readonly IChoice<Card, ITrashOption> trashing;

        public TrashingPilot(IChoice<Card, ITrashOption> trashing, IPilot basic) : base(basic)
        {
            this.trashing = trashing;
        }
        override public IChoice<Card, ITrashOption> ChooseTrashing() => trashing;
    }
}