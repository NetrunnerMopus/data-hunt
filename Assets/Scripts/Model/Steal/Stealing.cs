using System.Collections.Generic;
using System.Linq;
using model.cards;

namespace model.steal
{
    public class Stealing
    {
        private Runner runner;
        private readonly IList<IStealModifier> stealMods = new List<IStealModifier>();

        public Stealing(Runner runner) => this.runner = runner;

        internal void ModifyStealing(IStealModifier mod)
        {
            stealMods.Add(mod);
        }

        public IStealOption MustSteal(Card card, int agendaPoints)
        {
            IStealOption mustSteal = new MustSteal(card, agendaPoints, runner.zones);
            return stealMods.Aggregate(mustSteal, (option, mod) => mod.Modify(option));
        }
    }
}
