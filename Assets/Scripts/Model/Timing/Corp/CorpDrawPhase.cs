using System.Threading.Tasks;
using model.play;

namespace model.timing.corp
{
    public class CorpDrawPhase : ITimingStructure<CorpDrawPhase>
    {
        private Corp corp;
        private Timing timing;
        public PriorityWindow TurnBegins = new PriorityWindow("Corp turn begins");
        public string Name => "Corp draw phase";
        public event AsyncAction<CorpDrawPhase> Opened;
        public event AsyncAction<CorpDrawPhase> Closed;

        internal CorpDrawPhase(Corp corp, Timing timing)
        {
            this.corp = corp;
            this.timing = timing;
        }

        public async Task Open()
        {
            corp.clicks.Replenish(); // CR: 5.6.1.a
            await timing.OpenPaidWindow(rezzing: true, scoring: true); // CR: 5.6.1.b
            RefillRecurringCredits(); // CR: 5.6.1.c
            await TurnBegins.Open(); // CR: 5.6.1.d
            await timing.Checkpoint();// CR: 5.6.1.e
            await MandatoryDraw(); // CR: 5.6.1.f
            await timing.Checkpoint(); // CR: 5.6.1.g
        }

        private void RefillRecurringCredits()
        {
        }

        async private Task TriggerTurnBeginning()
        {
            await TurnBegins.Open();
        }

        async private Task MandatoryDraw()
        {
            await corp.zones.Drawing(1).Resolve();
        }
    }
}
