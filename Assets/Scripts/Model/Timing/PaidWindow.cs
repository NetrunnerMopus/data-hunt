using System.Linq;
using System.Threading.Tasks;
using model.player;

namespace model.timing
{
    public class PaidWindow : PriorityWindow
    {
        private bool rezzing;
        private bool scoring;
        private IPilot acting;
        private IPilot reacting;

        public PaidWindow(bool rezzing, bool scoring, IPilot acting, IPilot reacting, string name) : base(name)
        {
            this.rezzing = rezzing;
            this.scoring = scoring;
            this.acting = acting;
            this.reacting = reacting;
        }

        async override public Task Open()
        {
            pass = new TaskCompletionSource<bool>();
            Opened(this);

            var bothPlayersCouldAct = false;
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
            if (abilities.Count > 0)
            {
                await new SimultaneousTriggers(abilities.Copy()).AllTriggered(game.corp.pilot);
            }
            await pass.Task;
            Closed(this);
        }

        async private Task<bool> AwaitPass(IPilot pilot)
        {
            var options = abilities
                .Where(it => it.Ability.Active)
                .Where(it => it.Ability.controller == pilot);
            CardAbility pass = new PassOption();
            var option = await pilot.TriggerFromSimultaneous(options);
            await option.Ability.Trigger();
            return pass.Used;
        }
    }
}
