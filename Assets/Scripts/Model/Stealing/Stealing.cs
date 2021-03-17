using System.Collections.Generic;
using System.Linq;
using model.cards;

namespace model.stealing
{
    public class Stealing
    {
        private readonly IList<IStealModifier> stealMods = new List<IStealModifier>();

        internal void ModifyStealing(IStealModifier mod)
        {
            stealMods.Add(mod);
        }

        public IStealOption MustSteal(Card card, int agendaPoints)
        {
            IStealOption mustSteal = new MustSteal(card, agendaPoints);
            return stealMods.Aggregate(mustSteal, (option, mod) => mod.Modify(option));
        }
    }
}
