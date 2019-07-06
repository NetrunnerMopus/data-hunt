using model.cards;
using model.zones;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.player
{
    public class NoPilot : IPilot
    {
        void IPilot.Play(Game game) { }

        Task<IEffect> IPilot.TriggerFromSimultaneous(List<IEffect> effects)
        {
            return Task.FromResult(effects.First());
        }

        IChoice<Card> IPilot.ChooseACard() => new FailingChoice<Card>();
        IChoice<IInstallDestination> IPilot.ChooseAnInstallDestination() => new FailingChoice<IInstallDestination>();

        private class FailingChoice<T> : IChoice<T>
        {
            T IChoice<T>.Declare(IEnumerable<T> items)
            {
                throw new System.Exception("Don't know how to choose from " + items);
            }
        }
    }
}