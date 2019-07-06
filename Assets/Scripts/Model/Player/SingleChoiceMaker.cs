using model.cards;
using model.choices;
using model.play;
using model.zones;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.player
{
    public class SingleChoiceMaker : DelegatingPilot
    {
        public SingleChoiceMaker(IPilot basic) : base(basic) { }

        async override public Task<IEffect> TriggerFromSimultaneous(List<IEffect> effects)
        {
            if (effects.Count == 1)
            {
                return effects.Single();
            }
            else
            {
                return await base.TriggerFromSimultaneous(effects);
            }
        }

        override public IChoice<Card> ChooseACard() => new TheOnlyChoice<Card>(base.ChooseACard());
        override public IChoice<IInstallDestination> ChooseAnInstallDestination() => new TheOnlyChoice<IInstallDestination>(base.ChooseAnInstallDestination());
    }
}