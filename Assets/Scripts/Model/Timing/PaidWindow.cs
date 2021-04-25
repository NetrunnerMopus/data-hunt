using System;
using System.Threading.Tasks;
using model.play;
using model.player;

namespace model.timing
{
    public class PaidWindow : PriorityWindow
    {
        private bool rezzing;
        private bool scoring;
        private IPilot acting;
        private IPilot reacting;
        public event Action<Priority> PriorityGiven = delegate { };

        public PaidWindow(bool rezzing, bool scoring, IPilot acting, IPilot reacting, string name) : base(name)
        {
            this.rezzing = rezzing;
            this.scoring = scoring;
            this.acting = acting;
            this.reacting = reacting;
        }

        internal void GiveOption(IPilot pilot, IPlayOption pop)
        {
            throw new System.NotImplementedException();
        }

        async override public Task Open()
        {
            Opened(this); // TODO inherited events don't work
            priority.Add(new Rez(PadCampaign));
            priority.Add(new Aal());
            var bothPlayersCouldAct = false; // CR: 9.2.7.a
            while (true)
            {
                var actingDeclined = await AwaitPass(acting);
                if (actingDeclined && bothPlayersCouldAct)
                {
                    break;
                }
                var reactingDeclined = await AwaitPass(reacting);
                bothPlayersCouldAct = true;
                if (reactingDeclined && bothPlayersCouldAct)
                {
                    break;
                }
            }
            Closed(this);
        }

        async private Task<bool> AwaitPass(IPilot pilot)
        {
            var priority = new Priority(pilot, canPass: true); // CR: 9.2.4.b
            PriorityGiven(priority);
            var declined = false;
            while (!priority.Passed) // CR: 9.2.4.c
            {
                await priority.Choose(); // CR: 9.2.7.f
            }
            return declined;
        }
    }
}
