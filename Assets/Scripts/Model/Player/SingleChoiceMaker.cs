using model.cards;
using model.choices;
using model.choices.trash;
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

        override public IChoice<string, Card> ChooseACard() => new TheOnlyChoice<string, Card>(base.ChooseACard());
        override public IChoice<string, IInstallDestination> ChooseAnInstallDestination() => new TheOnlyChoice<string, IInstallDestination>(base.ChooseAnInstallDestination());
        override public IChoice<Card, ITrashOption> ChooseTrashing() => new TheOnlyChoice<Card, ITrashOption>(base.ChooseTrashing());
    }
}