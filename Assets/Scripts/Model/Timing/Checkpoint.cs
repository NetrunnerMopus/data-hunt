using System.Linq;
using System.Threading.Tasks;

namespace model.timing
{

    // CR: 10.3
    public class Checkpoint
    {
        private Game game;

        public Checkpoint(Game game)
        {
            this.game = game;
        }

        // CR: 10.3.1
        async internal Task Check()
        {
            CheckConditionalAbilities();
            CheckExpiredAbilities();
            CheckPlayerScore();
            CheckUniqueCards();
            await FixBrokenRestrictions();
            TrashScoreAreaLeftovers();
            CheckMissingHosts();
            PruneEmptyRemotes();
            UnconvertDiscardedCards();
            ReturnDiscardedCounters();
        }

        // CR: 10.3.1.a
        private void CheckConditionalAbilities()
        {
            // TODO impl
        }

        // CR: 10.3.1.b
        private void CheckExpiredAbilities()
        {
            // TODO impl
        }

        // CR: 10.3.1.c
        private void CheckPlayerScore()
        {
            // TODO impl
        }

        // CR: 10.3.1.d
        private void CheckUniqueCards()
        {
            // TODO impl
        }

        // CR: 10.3.1.e
        private Task FixBrokenRestrictions()
        {
            return Task.CompletedTask; // TODO impl
        }

        // CR: 10.3.1.f
        private void TrashScoreAreaLeftovers()
        {
            // TODO impl
        }

        // CR: 10.3.1.g
        private void CheckMissingHosts()
        {
            // TODO impl
        }

        // CR: 10.3.1.h
        private void PruneEmptyRemotes()
        {
            var zones = game.corp.zones;
            zones
                .remotes
                .Where(it => it.IsEmpty())
                .ToList()
                .ForEach(it => zones.RemoveRemote(it));
        }

        // CR: 10.3.1.i
        private void UnconvertDiscardedCards()
        {
            // TODO impl
        }

        // CR: 10.3.1.j
        private void ReturnDiscardedCounters()
        {
            // TODO impl
        }
    }
}