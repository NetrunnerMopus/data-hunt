using System.Threading.Tasks;
using model.timing;
using model.zones.corp;

namespace model.run {
    public class RunStructure {
        private IServer server;
        private readonly Game game;
        private int position;

        public RunStructure(IServer server, Game game) {
            this.server = server;
            this.game = game;
        }

        async public Task AwaitEnd() {
            var ice = server.Ice;
            position = ice.Height;
            // TODO
            if (position == 0) {
                await ApproachServer(); // 6.9.5
            } else {
                // TODO
            }
            await End(); // 6.9.6
        }

        async private Task ApproachServer() {
            await TriggerServerApproached(); // 6.9.5.a
            await game.Timing.DefinePaidWindow(rezzing: false, scoring: false).Open(); // 6.9.5.b
            var jackedOut = await OfferJackOut(); // 6.9.5.c
            if (jackedOut) {
                return;
            }
            await game.Timing.DefinePaidWindow(rezzing: true, scoring: false).Open(); // 6.9.5.d
            await MakeRunSuccessful(); // 6.9.5.e
            await game.Timing.Checkpoint(); // 6.9.5.f
            await Access(); // 6.9.5.g
            await game.Timing.Checkpoint(); // 6.9.5.h
        }

        async private Task TriggerServerApproached() {
            await Task.CompletedTask; // TODO
        }



        async private Task<bool> OfferJackOut() {
            return await Task.FromResult(false); // TODO
        }

        async private Task MakeRunSuccessful() {
            await Task.CompletedTask; // TODO
        }

        async private Task Access() {
            await new AccessStructure(server, game).AwaitEnd();
        }

        async private Task End() {
            await LoseBadPublicity(); // 6.9.6.a
            // TODO 6.9.6.b
            await game.Checkpoint(); // 6.9.6.c
        }

        async private Task LoseBadPublicity() {
            await Task.CompletedTask; // TODO
        }

        public override string ToString() {
            return "Run(server=" + server + ")";
        }
    }
}
