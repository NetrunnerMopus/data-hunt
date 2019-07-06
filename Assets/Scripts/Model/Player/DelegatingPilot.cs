using model.cards;
using model.choices;
using model.choices.trash;
using model.zones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.player
{
    public class DelegatingPilot : IPilot
    {
        private IPilot basic;

        public DelegatingPilot(IPilot basic)
        {
            this.basic = basic;
        }

        public virtual void Play(Game game)
        {
            basic.Play(game);
        }

        public virtual Task<IEffect> TriggerFromSimultaneous(List<IEffect> effects)
        {
            return basic.TriggerFromSimultaneous(effects);
        }

        public virtual IChoice<Card> ChooseACard()
        {
            return basic.ChooseACard();
        }

        public virtual IChoice<IInstallDestination> ChooseAnInstallDestination()
        {
            return basic.ChooseAnInstallDestination();
        }

        public virtual IChoice<ITrashOption> ChooseTrashing()
        {
            return basic.ChooseTrashing();
        }
    }
}