using model.cards;
using model.zones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.player
{
    public interface IPilot
    {
        void Play(Game game);
        Task<IEffect> TriggerFromSimultaneous(List<IEffect> effects);
        IChoice<Card> ChooseACard();
        IChoice<IInstallDestination> ChooseAnInstallDestination();
    }

    public interface IChoice<T>
    {
        T Declare(IEnumerable<T> items);
    }
}