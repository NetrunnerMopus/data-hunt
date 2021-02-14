using System.Collections.Generic;
using model.choices.steal;
using model.zones;

namespace model.cards.types
{
    public class Agenda : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => false;
        IList<IInstallDestination> IType.FindInstallDestinations(Game game) => game.corp.zones.RemoteInstalls();
        IList<IStealOption> IType.DefaultStealing(Card card, Game game) => new List<IStealOption> { game.MustSteal(card, printedAgendaPoints) };
        private int printedAgendaPoints;

        public Agenda(int printedAgendaPoints)
        {
            this.printedAgendaPoints = printedAgendaPoints;
        }
    }
}
