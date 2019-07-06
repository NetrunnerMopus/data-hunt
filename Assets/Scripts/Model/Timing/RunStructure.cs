using model.zones.corp;
using System.Threading.Tasks;

namespace model.timing
{
    public class RunStructure
    {
        private IServer server;
        private readonly Game game;
        private int position;

        public RunStructure(IServer server, Game game)
        {
            this.server = server;
            this.game = game;
        }

        async public Task AwaitEnd()
        {
            var ice = server.Ice;
            position = ice.Height;
            // TODO
            if (position == 0)
            {
                await ApproachServer(); // 6.9.5
            }
            else
            {
                // TODO
            }
            await End(); // 6.9.6
        }

        async private Task ApproachServer()
        {
            await TriggerServerApproached(); // 6.9.5.a
            await OpenPreCommitmentWindows(); // 6.9.5.b
            var jackedOut = await OfferJackOut(); // 6.9.5.c
            if (jackedOut)
            {
                return;
            }
            await OpenPostCommitmentWindows(); // 6.9.5.d
            await MakeRunSuccessful(); // 6.9.5.e
            await Checkpoint(); // 6.9.5.f
            await Access(); // 6.9.5.g
            await Checkpoint(); // 6.9.5.h
        }

        async private Task TriggerServerApproached()
        {
            await Task.CompletedTask; // TODO
        }

        async private Task OpenPreCommitmentWindows()
        {
            await game.OpenPaidWindow(game.runner.paidWindow, game.corp.paidWindow);  // TODO An Offer You Can't Refuse
        }

        async private Task<bool> OfferJackOut()
        {
            return await Task.FromResult(false); // TODO
        }

        async private Task OpenPostCommitmentWindows()
        {
            var paid = game.OpenPaidWindow(game.runner.paidWindow, game.corp.paidWindow);  // TODO An Offer You Can't Refuse
            var rez = game.corp.turn.rezWindow.Open();
            await paid;
            await rez;
        }

        async private Task MakeRunSuccessful()
        {
            await Task.CompletedTask; // TODO
        }

        async private Task Checkpoint()
        {
            await Task.CompletedTask; // TODO
        }

        async private Task Access()
        {
            UnityEngine.Debug.Log("Accessing " + server);
            await Task.CompletedTask; // TODO
        }

        async private Task End()
        {
            await LoseBadPublicity(); // 6.9.6.a
            // TODO 6.9.6.b
            await Checkpoint(); // 6.9.6.c
        }

        async private Task LoseBadPublicity()
        {
            await Task.CompletedTask; // TODO
        }

        public override string ToString()
        {
            return "Run(server=" + server + ")";
        }
    }
}