using model.cards;
using model.choices;
using model.choices.trash;
using model.stealing;
using model.zones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.player
{
    public interface IPilot
    {
        void Play(Game game);
        Task<IEffect> TriggerFromSimultaneous(IList<IEffect> effects);
        IDecision<string, Card> ChooseACard();
        // IDecision<Card> ChooseAZone(); TODO for central access
        IDecision<string, IInstallDestination> ChooseAnInstallDestination();
        IDecision<Card, ITrashOption> ChooseTrashing();
        IDecision<Card, IStealOption> ChooseStealing();
    }
}
