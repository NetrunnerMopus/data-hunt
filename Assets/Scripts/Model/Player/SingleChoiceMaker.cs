using model.cards;
using model.play;
using model.zones;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.player
{
    public class SingleChoiceMaker : IPilot
    {
        private IPilot basic;
        private HashSet<Ability> paidAbilities = new HashSet<Ability>();
        private Game game;

        public SingleChoiceMaker(IPilot basic)
        {
            this.basic = basic;
        }

        void IPilot.Play(Game game)
        {
            basic.Play(game);
        }

        async Task<IEffect> IPilot.TriggerFromSimultaneous(List<IEffect> effects)
        {
            if (effects.Count == 1)
            {
                return effects.Single();
            }
            else
            {
                return await basic.TriggerFromSimultaneous(effects);
            }
        }

        IChoice<Card> IPilot.ChooseACard() => new TheOnlyChoice<Card>(basic.ChooseACard());
        IChoice<IInstallDestination> IPilot.ChooseAnInstallDestination() => new TheOnlyChoice<IInstallDestination>(basic.ChooseAnInstallDestination());
    }
}