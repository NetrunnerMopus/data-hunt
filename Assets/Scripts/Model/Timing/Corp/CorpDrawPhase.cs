using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;

namespace model.timing.corp
{
    public class CorpDrawPhase : ITimingStructure
    {
        private Corp corp;
        private Timing timing;
        private IList<IEffect> turnBeginningTriggers = new List<IEffect>();
        public string Name => "Corp draw phase";
        public event AsyncAction<ITurn> Opened;
        public event AsyncAction<ITurn> Closed;

        public CorpDrawPhase(Corp corp, Timing timing)
        {
            this.corp = corp;
            this.timing = timing;
        }

        public async Task Open()
        {
            corp.clicks.Replenish(); // CR: 5.6.1.a
            await timing.OpenPaidWindow(rezzing: true, scoring: true); // CR: 5.6.1.b
            RefillRecurringCredits(); // CR: 5.6.1.c
            await TriggerTurnBeginning(); // CR: 5.6.1.d
            await timing.Checkpoint();// CR: 5.6.1.e
            await MandatoryDraw(); // CR: 5.6.1.f
            await timing.Checkpoint(); // CR: 5.6.1.g
        }

        private void RefillRecurringCredits()
        {
        }

        async private Task TriggerTurnBeginning()
        {
            if (turnBeginningTriggers.Count > 0)
            {
                await new SimultaneousTriggers(turnBeginningTriggers.Copy()).AllTriggered(game.corp.pilot);
            }
        }

        async private Task MandatoryDraw()
        {
            await corp.zones.Drawing(1).Resolve();
        }

        public void WhenBegins(IEffect effect)
        {
            turnBeginningTriggers.Add(effect);
        }
    }
}
