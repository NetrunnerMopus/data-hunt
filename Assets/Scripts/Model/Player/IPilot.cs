using model.cards;
using model.choices;
using model.choices.trash;
using model.zones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.player
{
    public interface IPilot
    {
        void Play(Game game);
        Task<IEffect> TriggerFromSimultaneous(List<IEffect> effects);
        IChoice<string, Card> ChooseACard();
        // IChoice<Card> ChooseAZone(); TODO for central access
        IChoice<string, IInstallDestination> ChooseAnInstallDestination();
        IChoice<Card, ITrashOption> ChooseTrashing();
    }
}