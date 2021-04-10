﻿using System.Threading.Tasks;

namespace model.timing.corp
{
    public class CorpTurn : ITimingStructure<CorpTurn>
    {
        public CorpDrawPhase drawPhase { get; }
        public CorpActionPhase actionPhase { get; }
        public CorpDiscardPhase discardPhase { get; }
        private Corp corp;
        private Timing timing;
        public ClickPool Clicks => corp.clicks;
        public Side Side => Side.CORP;
        public string Name { get; }
        public event AsyncAction<CorpTurn> Opened;
        public event AsyncAction<CorpTurn> Closed;

        public CorpTurn(Corp corp, Timing timing, int number)
        {
            this.corp = corp;
            this.timing = timing;
            Name = "Corp turn " + number;
            drawPhase = new CorpDrawPhase(corp, timing);
            actionPhase = new CorpActionPhase(corp, timing);
            discardPhase = new CorpDiscardPhase();
        }

        public CorpTurn(Corp corp, Timing timing)
        {
            this.corp = corp;
            this.timing = timing;
        }

        public async Task Open()
        {
            await Opened?.Invoke(this);
            await drawPhase.Open();
            await actionPhase.Open();
            await discardPhase.Open();
            await Closed?.Invoke(this);
        }

        async private Task DiscardPhase()
        {
            await Discard(); // CR: 5.6.3.a
            var rez = OpenRezWindow();  // CR: 5.6.3.b
            var paid = OpenPaidWindow();  // CR: 5.6.3.b
            await rez;
            await paid;
            corp.clicks.Reset(); // CR: 5.6.3.c
            TriggerTurnEnding(); // CR: 5.6.3.d
            await timing.Checkpoint(); // CR: 5.6.3.e
        }

        async private Task Discard()
        {
            var hq = corp.zones.hq;
            while (hq.Zone.Count > 5)
            {
                await hq.Discard();
            }
        }

        private void TriggerTurnEnding()
        {
        }
    }
}
