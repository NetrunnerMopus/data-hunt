using model.cards;
using model.player;
using model.zones;
using System.Threading.Tasks;

namespace model.timing
{
    public class AccessCard
    {
        private readonly Card card;
        private readonly Game game;

        public AccessCard(Card card, Game game)
        {
            this.card = card;
            this.game = game;
        }

        async public Task AwaitEnd()
        {
            var preAccessInfo = Look();
            var preAccessZone = card.Zone;
            await TriggerAccessingCard(); // 7.8.1
            await game.Checkpoint(); // 7.8.2
            await Trash(); // 7.8.3
            await Steal(); // 7.8.4
            await TriggerAfterAccessingCard(); // 7.8.5
            await game.Checkpoint(); // 7.8.6
            StopLooking(preAccessInfo, preAccessZone);
        }

        private Information Look()
        {
            var info = card.Information;
            card.UpdateInfo(Information.OPEN); // TODO actually, Corp shouldn't see this in R&D
            return info;
        }

        async private Task TriggerAccessingCard()
        {
            await Task.CompletedTask; // TODO
        }

        async private Task<bool> Trash()
        {
            var options = card.TrashOptions(game);
            var decision = game.runner.pilot.ChooseTrashing();
            var trashing = await decision.Declare(card, options, game);
            return await trashing.Perform(game);
        }

        async private Task<bool> Steal()
        {
            var options = card.StealOptions(game);
            var decision = game.runner.pilot.ChooseStealing();
            var stealing = await decision.Declare(card, options, game);
            return await stealing.Perform(game);
        }

        async private Task TriggerAfterAccessingCard()
        {
            await Task.CompletedTask; // TODO
        }

        private void StopLooking(Information preAccessInfo, Zone preAccessZone)
        {
            if (card.Zone == preAccessZone)
            {
                card.UpdateInfo(preAccessInfo);
            }
        }

        public override string ToString()
        {
            return "AccessCard(card=" + card + ")";
        }
    }
}
